using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);
        var transitId = options.TransitIds.Length > 0
            ? options.TransitIds[0]
            : Common.AskTransitId();

        var client = Common.CreateClient(options);

        var resultDocument = await client.GetTransitLayoutAsync(options.ProjectId, transitId);

        Console.WriteLine("The complete server response was:");
        Console.WriteLine(JsonConvert.SerializeObject(resultDocument, Formatting.Indented));
    }
}
