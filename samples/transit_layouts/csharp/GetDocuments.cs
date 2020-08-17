using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using RtdApiCodeSamples;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);
        var transitIds = options.TransitIds.Length > 0
            ? options.TransitIds
            : Common.AskTransitIds();

        var transitIdStrings = transitIds.Select(id => id.ToString()).ToList();
        
        // Create a client and set to authenticate using the API key
        var client = Common.CreateClient(options);
        
        // Build a document that contains options for creating transit documents.
        var createJobDocument = new CreateTransitDocumentsJobDocument
        {
            Data = new CreateTransitDocumentsJobResource
            {
                Type = Common.TransitDocumentsType,
                Attributes = new Attributes3
                {
                    TransitIds = transitIdStrings,
                    DocumentTypes = new DocumentTypes
                    {
                        BillOfMaterial = new BillOfMaterial2 {Xlsx = true},
                        Certificate = true,
                        Drawing = new Drawing2 {Dxf = true, Pdf = true},
                        FrameModel = new FrameModel {Revit = true, Step = true},
                        InstallationInstruction = true,
                        // SpecificationDocument = true // not available for all project categories
                    }
                },
                Relationships = new Relationships3
                {
                    Project = Common.CreateProjectRelationship(options.ProjectId)
                }
            }
        };

        try
        {
            var jobDocument = await client.CreateTransitDocumentsJobAsync(options.ProjectId, createJobDocument);
            var jobId = Guid.Parse(jobDocument.Data.Id);

            Console.WriteLine($"Documents job successfully created, got job ID {jobId}");

            // Query job status periodically, not too often
            bool isComplete;
            string downloadUrl;
            do
            {
                // Wait 1.5 seconds before checking status.
                await Task.Delay(1500);

                var statusDocument = await client.GetTransitDocumentsJobAsync(options.ProjectId, jobId);
                var pctComplete = statusDocument.Data.Attributes.PercentageComplete;

                Console.WriteLine($"% complete : {pctComplete:F2}");

                isComplete = pctComplete >= 100d;

                // Save the download URL for later use, as the NSwag-generated DownloadTransitDocumentsAsync
                // method isn't helpful (see https://github.com/RicoSuter/NSwag/issues/2842).
                downloadUrl = statusDocument.Data.Relationships.Download.Links.Related;
            } while (!isComplete);

            var response = await Common.HttpClient.GetAsync(downloadUrl);
            var filename = GetFilenameFromResponse(response);

            await using var outputStream = File.OpenWrite(filename);
            await response.Content.CopyToAsync(outputStream);

            Console.WriteLine($"Download complete; the documents were saved in {filename}");
        }
        catch (ApiException<ErrorList> ex)
        {
            foreach (var err in ex.Result.Errors)
            {
                Console.WriteLine($"{err.Title}: {err.Detail}");
            }
        }
    }

    private static string GetFilenameFromResponse(HttpResponseMessage response)
    {
        var contentDispositionHeader = response.Content.Headers.ContentDisposition;
        if (contentDispositionHeader == null) return "downloaded.zip";
        return contentDispositionHeader.FileNameStar ?? contentDispositionHeader.FileName;
    }
}
