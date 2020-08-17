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

    public static Client CreateClient(ProjectOptions options)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.ProjectApiKey);

        return new Client(httpClient);
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
            if (!Guid.TryParse(arg, out var transitId))
            {
                throw new ArgumentException($"Not a valid transit ID: {arg}");
            }
            yield return transitId;
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
        return Guid.TryParse(str, out var id)
            ? id
            : throw new ArgumentException($"Not a valid transit ID: {str}");
    }
}