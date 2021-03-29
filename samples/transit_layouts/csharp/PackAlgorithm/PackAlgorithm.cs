using RtdApiCodeSamples;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    private const string S6X2Aisi316PartNumber = "S006000000221";
    private const string LeveloutPackAlgorithm = "levelout";    
    private const string LeveloutByCategoryPackAlgorithm = "leveloutbycategory";    
    
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
                    Frame = new Frame
                    {
                        PartNumber = S6X2Aisi316PartNumber
                    },
                    Drawing = new Drawing
                    {
                        Revision = "A"
                    },

                    // The cables have two different categories. With Levelout pack algorithm,
                    // they will be packed in the same frame opening. With LeveloutByCategory,
                    // they will be packed in different openings.
                    Cables = new[]
                    {
                        new Cable {Diameter = 20, Id = "a", Category = "CatA"},
                        new Cable {Diameter = 20, Id = "b", Category = "CatA"},
                        new Cable {Diameter = 20, Id = "c", Category = "CatB"},
                        new Cable {Diameter = 20, Id = "d", Category = "CatB"}
                    },
                    
                    PackingParameters = new PackingParameters
                    {
                        Algorithm = LeveloutPackAlgorithm
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

        Console.WriteLine("");
        Console.WriteLine($"The transit was successfully created! It has transit ID {resultDocument.Data.Id}");

        Console.WriteLine("");
        Console.WriteLine("The complete server response was:");
        Console.WriteLine(JsonConvert.SerializeObject(resultDocument, Formatting.Indented));
    }
}
