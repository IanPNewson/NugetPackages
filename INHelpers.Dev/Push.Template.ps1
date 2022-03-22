$APIKey = "{APIKey}"

$pkgs = (New-Object System.IO.DirectoryInfo($PSScriptRoot + "\bin\Debug")).GetFiles("*.nupkg")
$pkgs = $pkgs | Sort-Object -Descending LastWriteTime

Write-Host ($pkgs[0].LastWriteTime.ToString() + $pkgs[0].Name)

dotnet nuget push ($pkgs[0].FullName) -k $APIKey -s https://api.nuget.org/v3/index.json