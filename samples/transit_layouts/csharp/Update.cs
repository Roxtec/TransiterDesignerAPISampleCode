using System;
using RtdApiCodeSamples;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = Common.ParseCommandLine(args);
        var transitId = options.TransitIds.Length > 0
            ? options.TransitIds[0]
            : Common.AskTransitId();

        var client = Common.CreateClient(options);

        var documentBefore = await client.GetTransitLayoutAsync(options.ProjectId, transitId);

        // Take existing cables, then add 4 new ones
        var cables = documentBefore.Data.Attributes.Cables;
        var newCables = cables.Concat(GenerateAdditionalCables(cables, 4));
        
        // Build a document with transit update data.
        // Attributes only need to contain the data to update, in this case cables.
        var createDocument = new SingleTransitLayoutCreateUpdateDocument
        {
            Data = new TransitLayoutCreateUpdateResource
            {
                Id = transitId.ToString(), // needed for update
                Type = Common.TransitLayoutsType,
                Attributes = new Attributes
                {
                    Name = documentBefore.Data.Attributes.Name, // TODO remove
                    Cables = newCables.ToArray()
                },
                Relationships = new Relationships
                {
                    Project = Common.CreateProjectRelationship(options.ProjectId)
                }
            }
        };

        var resultDocument = await client.UpdateTransitLayoutAsync(options.ProjectId, transitId,
            createDocument);
        
        Console.WriteLine("The transit was successfully updated!");
        Console.WriteLine($"Fill rate before : {fillRatePct(documentBefore)}");
        Console.WriteLine($"Fill rate after  : {fillRatePct(resultDocument)}");

        static string fillRatePct(SingleTransitLayoutDocument document)
        {
            var rate = document.Data.Attributes.PackingResult.FillRate;
            return $"{rate:F2} %";
        }
    }

    private static IEnumerable<Cable> GenerateAdditionalCables(ICollection<Cable> cables, int count)
    {
        var existingIds = new HashSet<string>(cables.Select(cable => cable.Id));
        return Enumerable.Repeat(0, 4).Select(_ =>
        {
            var id = generateId();
            return new Cable {Id = id, Diameter = 20};
        });

        string generateId()
        {
            var number = 0;
            string newId;
            do
            {
                number++;
                newId = $"a{number}";
            } while (existingIds.Contains(newId));

            existingIds.Add(newId);
            return newId;
        }
    }
}
