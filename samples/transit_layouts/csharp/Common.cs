using System;
using RtdApiCodeSamples;

public class ProjectOptions
{
    public Guid ProjectId { get; set; }
    public string ProjectApiKey { get; set; }
}

public static class Common
{
    public const string TransitLayoutsType = "transit_layouts";
    public const string ProjectsType = "projects";
    
    public static ProjectOptions ParseCommandLine(string[] args)
    {
        if (args.Length < 3) throw new ArgumentException("Please include project ID and API key as arguments.");
        var projectIdStr = args[1];
        var projectApiKey = args[2];

        if (!Guid.TryParse(projectIdStr, out var projectId))
        {
            throw new ArgumentException($"Not a valid project ID: {projectIdStr}");
        }

        return new ProjectOptions {ProjectId = projectId, ProjectApiKey = projectApiKey};
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
}