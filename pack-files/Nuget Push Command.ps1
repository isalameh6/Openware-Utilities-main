foreach($file in Get-ChildItem -Filter *.nupkg)
{
nuget.exe push $file.Name -ApiKey Openware@1919! -SkipDuplicate -Source https://dev.openware.com.kw/Openware/PackageServer/nuget
Remove-Item $file.FullName
}