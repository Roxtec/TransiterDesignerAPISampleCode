using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using RtdApiCodeSamples;

public class ProjectOptions
{
    public Guid ProjectId { get; set; }
    public string ProjectApiKey { get; set; }
    public Guid[] TransitIds { get; set; }
}

public static class Common
{
    public const string TransitLayoutsType = "transit_layouts";
    public const string ProjectsType = "projects";
    public const string TransitDocumentsType = "transit_documents_jobs";

    public static readonly HttpClient HttpClient = new HttpClient();
    
    public static Client CreateClient(ProjectOptions options)
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ProjectApiKey);

        return new Client(HttpClient);
    }
    
    public static ProjectOptions ParseCommandLine(string[] args)
    {
        if (args.Length < 2) throw new ArgumentException("Please include project ID and API key as arguments.");

        var projectIdStr = args[0];
        var projectApiKey = args[1];

        if (!Guid.TryParse(projectIdStr, out var projectId))
        {
            throw new ArgumentException($"Not a valid project ID: {projectIdStr}");
        }

        var transitIds = new Guid[0];
        if (args.Length > 2)
        {
            transitIds = ParseTransitIds(args.Skip(2)).ToArray();
        }

        return new ProjectOptions
        {
            ProjectId = projectId,
            ProjectApiKey = projectApiKey,
            TransitIds = transitIds
        };
    }

    private static IEnumerable<Guid> ParseTransitIds(IEnumerable<string> args)
    {
        foreach (var arg in args)
        {
            yield return ParseTransitId(arg);
        }
    }

    public static ResourceLinkageRelationship CreateProjectRelationship(Guid projectId)
    {
        return new ResourceLinkageRelationship
        {
            Data = new ResourceLinkageRelationshipData
            {
                Id = projectId.ToString(),
                Type = ProjectsType
            }
        };
    }

    public static Guid AskTransitId()
    {
        Console.WriteLine("Please enter a transit ID (GUID):");
        var str = Console.ReadLine();
        return ParseTransitId(str);
    }
    
    public static IEnumerable<Guid> AskTransitIds()
    {
        Console.WriteLine("Please enter one or more transit IDs (GUID), separate with commas or spaces:");
        var str = Console.ReadLine();
        var parts = str.Split(',', ' ');
        var ids = ParseTransitIds(parts).ToList();
        if (ids.Count == 0)
        {
            Console.WriteLine("Please specify at least one transit ID");
            return AskTransitIds();
        }

        return ids;
    }

    private static Guid ParseTransitId(string id)
    {
        return Guid.TryParse(id, out var transitId)
            ? transitId
            : throw new ArgumentException($"Not a valid transit ID: {id}");
    }
}