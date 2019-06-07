# Runs the TestApp, without building either
# NZXTSharp or the TestApp

dotnet build ..\TestApp\TestApp.csproj
start ..\TestApp\bin\Debug\TestApp.exe