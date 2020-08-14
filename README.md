# Transit Designer API sample code

This repository contains code samples for [Roxtec Transit Designer<sup>TM</sup>](https://www.roxtec.com/en/service-support/design/roxtec-transit-designer/).

## Repository structure

The repository is structured as follows:

```
samples/
\--<api name>
   \--<language>
      |-- <sample file 1>
      |-- ...
      \-- <sample file N>
```

Currently, there is a single API: _transit_layouts_

Similarly, all code samples are in C#, under the _csharp_ language folder.

## How to use the samples

### .NET Core

The C# code makes use of .NET Core 3.1. It can be installed
from [here](https://dotnet.microsoft.com/download/dotnet-core/3.1).

### Swagger

The Swagger documentation for Transit Designer can be viewed [here](https://transitdesigner.roxtec.com/api/docs).
This link always refers to the latest API documentation.

The Swagger documentation exists in a machine-readable file (swagger.json) as well, which we will
use in the next section. 

This file is located at: https://transitdesigner.roxtec.com/api/docs/swagger.json

### Generate a Swagger client

To generate a Swagger client for C#, please complete the following steps.
For the samples to work, the current directory should be the root of this
Git repository.

Here we use [NSwag](https://github.com/RicoSuter/NSwag) to generate the client.
It can be done in many different ways (see the NSwag link). Here we do it with an npm
package called `nswag`. Note that this requires Node.js and the `npx` to be installed.

1. Run the following command from a PowerShell or command prompt:

        npx nswag swagger2csclient --core 3.1

2. The program first asks about namespace of the generated classes. You can choose whatever
you like; here we use _RtdApiCodeSamples_.

3. Next, the program asks about the path or URL to swagger.json. Enter: https://transitdesigner.roxtec.com/api/docs/swagger.json

The program now generates a file named `Full`. This file contains C# code
for the Swagger client.

The entire exchange should look like this (`>` is the prompt):

```
❯ npx nswag swagger2csclient --core 3.1
npx: installed 1 in 4.433s
NSwag NPM CLI
NSwag command line tool for .NET Core NetCore31, toolchain v13.7.0.0 (NJsonSchema v10.1.24.0 (Newtonsoft.Json v12.0.0.0))
Visit http://NSwag.org for more information.
NSwag bin directory: ...
The namespace of the generated classes.
Namespace: RtdApiCodeSamples
A file path or URL to the data or the JSON data itself.
Input: https://transitdesigner.roxtec.com/api/docs/swagger.json
Code has been successfully written to file.

Duration: 00:00:11.1543105
```

**The file `Full` needs to renamed to `Full.cs`**. Please use one of the following commands
depending on your shell (`>` is the prompt):

    > mv Full Full.cs
    > move Full Full.cs
    > rename Full Full.cs

### Project ID and API key

All samples must be run with a Transit Designer project ID. To obtain the ID,
open Transit Designer and navigate to the overview of a project (create one if
necessary). The URL should look as follows:

`https://transitdesigner.roxtec.com/app/projects/1d64040c-c5e2-4c8c-9be4-281bf96a144c/overview`

The project ID in this case is `1d64040c-c5e2-4c8c-9be4-281bf96a144c`.

Some samples operate one one or more transits. A transit ID can be extracted from the
URL as well. Navigate to the cable registration page of a transit. The URL should
look as follows:

`https://transitdesigner.roxtec.com/app/projects/1d64040c-c5e2-4c8c-9be4-281bf96a144c/transits/3502645f-1693-4729-bd15-069366b78144/cablereg`

The transit ID in this case is `3502645f-1693-4729-bd15-069366b78144`.

Furthermore, it is necessary to have an API key. Go to the project settings, then
choose the _API key_ tab. If this tab is not visible, you do not have access to
Transit Designer APIs. Please contact a Roxtec representative to request access.

On the _API key_ tab, follow the instructions to generate a project API key.
Please copy and store the key in a safe place; once it has been generated, it
cannot be obtained from Transit Designer again.

### Running a sample

The C# samples are made to be run using `dotnet run`. Each sample has its own
project file to facilitate this.

To run an individual sample, navigate to its directory and run:

    dotnet run <Sample> [arguments...]

For example, to create a transit:

    cd samples
    cd transit_layouts
    cd csharp
    dotnet run CreateTransit <project ID> <project API key>

Replace `<project ID>` and `<project API key>` with the project ID and API key,
respectively. 

All samples require project ID and API key. The sample program will ask for a transit ID if it needs one.