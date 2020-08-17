using RtdApiCodeSamples;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);
        
        Console.WriteLine("Please enter a transit name:");
        var transitName = Console.ReadLine();
        
        Console.WriteLine($"Project ID: {options.ProjectId}");
        Console.WriteLine($"Project API key: {options.ProjectApiKey}");
        Console.WriteLine($"Transit name: {transitName}");

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
                    Name = transitName,
                    Cables = new[]
                    {
                        new Cable {Diameter = 20, Id = "a"},
                        new Cable {Diameter = 20, Id = "b"},
                        new Cable {Diameter = 20, Id = "c"},
                        new Cable {Diameter = 20, Id = "d"}
                    },
                    Frame = new Frame
                    {
                        PartNumber = "S006000000121" // S 6x1 AISI316
                    },
                    Drawing = new Drawing
                    {
                        Revision = "A"
                    },
                    Modules = new ModuleSpecification
                    {
                        EmcSystem = new EmcSystem
                        {
                            Center = true,
                            Type = "PE"
                        }
                    }
                },
                Relationships = new Relationships
                {
                    Project = Common.CreateProjectRelationship(options.ProjectId)
                }
            }
        };
        
        // Send the transit create request to the Transit Designer server
        var resultDocument = await client.CreateTransitLayoutAsync(options.ProjectId, createDocument, default);

        Console.WriteLine($"The transit was successfully created! It has transit ID {resultDocument.Data.Id}");

        Console.WriteLine("The complete server response was:");
        Console.WriteLine(JsonConvert.SerializeObject(resultDocument, Formatting.Indented));
    }
}