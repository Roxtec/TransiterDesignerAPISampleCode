param(
    [Parameter(Mandatory=$true)]
    [string]$name,

    [Parameter(Mandatory=$true)]
    [string]$projectId,

    [Parameter(Mandatory=$true)]
    [string]$projectApiKey,

    [parameter(Mandatory=$false)]
    [string[]]$transitIds
)

dotnet run -p "${name}.csproj" $projectId $projectApiKey @transitIds
