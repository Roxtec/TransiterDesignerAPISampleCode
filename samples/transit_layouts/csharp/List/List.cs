using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);

        Console.WriteLine($"Listing transit layouts for project {options.ProjectId}");

        var client = Common.CreateClient(options);

        var document = await client.ListTransitLayoutsAsync(options.ProjectId, null, default);
        
        Console.WriteLine("");
        Console.WriteLine("The complete server response was:");
        Console.WriteLine(JsonConvert.SerializeObject(document, Formatting.Indented));

        Console.WriteLine($"Listing transit layouts for project {options.ProjectId}, name only.");

        var filteredDocument = await client.ListTransitLayoutsAsync(options.ProjectId, "name", default);

        Console.WriteLine("");
        Console.WriteLine("The complete server response was:");
        Console.WriteLine(JsonConvert.SerializeObject(filteredDocument, Formatting.Indented));
    }
}
