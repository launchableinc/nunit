# Launchable NUnit integration
See https://www.launchableinc.com/docs/features/predictive-test-selection/requesting-and-running-a-subset-of-tests/subsetting-with-the-launchable-cli/zero-input-subsetting/#dotnet-test--nunit for the user documentation

## Development instruction
* `dotnet build` to build
* `dotnet test` to run unit tests. `dotnet test -l:nunit` to produce TestResults.xml

## Release instruction
* Update the version number in `Launchable.NUnit/*.csproj`
* `dotnet pack -c Release` to produce a release mode package. See `ls **/*.nupkg`
* Upload this to https://www.nuget.org/packages/Launchable.NUnit with a browser.
  (TODO: automate this via https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli)