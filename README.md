# Rice
Remote code execution for .Net!

# To Dev
git clone --recurse-submodules https://github.com/zerotwooneone/Rice

## To Update a submodule
git submodule update --remote Rice.Core

## To Build
dotnet build .\Rice.Core\Source\Rice.Core\Rice.Core.csproj --output .\Dependencies\Rice\Rice.Core
dotnet build .\Rice.sln