param(
    [Parameter(Mandatory=$true)]
    [string]$name
)

$projectFileName = "$name.csproj"
$sampleFileName = "$name.cs"

@"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Newtonsoft.Json" Version="12.0.3" />
  </ItemGroup>

  <ItemGroup>
    <Compile Remove="*.cs" />
    <Compile Include="$name.cs" />
    <Compile Include="Common.cs" />
    <Compile Include="..\..\..\Full.cs" />
  </ItemGroup>

</Project>
"@ | Out-File $projectFileName


@"
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
    }
}
"@ | Out-File $sampleFileName
