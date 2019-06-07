# Builds NZXTSharp and the TestApp, runs TestApp

dotnet build ..\NZXTSharp\NZXTSharp.csproj
Write-Host "-----SDK BUILD DONE-----"
dotnet build ..\TestApp\TestApp.csproj
Write-Host "-----TestApp BUILD DONE-----"
Write-Host "---STARTING---"
start ..\TestApp\bin\Debug\TestApp.exe