using RtdApiCodeSamples;
using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);
        
        Console.WriteLine($"Project ID: {options.ProjectId}");
        Console.WriteLine($"Project API key: {options.ProjectApiKey}");

        // Create a client and set to authenticate using the API key
        var client = Common.CreateClient(options);

        // Build a document with settings for the new transit
        var createDocument = new SingleTransitLayoutCreateUpdateDocument
        {
            Data = new TransitLayoutCreateUpdateResource
            {
                Type = Common.TransitLayoutsType,
                Attributes = new Attributes
                {
                    Name = "TestError",
                    Cables = new[]
                    {
                        new Cable {Id = "id-a", Diameter = 20},
                        new Cable {Id = "id-a", Diameter = 20},
                    },
                    Frame = new Frame
                    {
                        PartNumber = "unknown"
                    }
                },
                Relationships = new Relationships
                {
                    Project = Common.CreateProjectRelationship(options.ProjectId)
                }
            }
        };

        try
        {
            // Send the transit create request to the Transit Designer server
            _ = await client.CreateTransitLayoutAsync(options.ProjectId, createDocument, default);
        }
        catch (ApiException<ErrorList> ex)
        {
            Console.WriteLine("Errors");
            Console.WriteLine("------");
            foreach (var error in ex.Result.Errors)
            {
                Console.WriteLine($"* {error.Title}: {error.Detail}");
            }
        }
    }
}
