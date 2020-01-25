# Rice
Remote code execution for .Net!

# To Dev
git clone --recurse-submodules https://github.com/zerotwooneone/Rice

## To Update a submodule (executed from Repo root)
git submodule update --remote Rice.Core

## To Build (executed from from Repo root)
~~dotnet build .\Rice.Core\Source\Rice.Core\Rice.Core.csproj --output .\Dependencies\Rice\Rice.Core~~
dotnet build .\Source\Rice.sln