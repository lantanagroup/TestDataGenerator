param(
    [string]$version = "3.1.0.0"
)

$files = Get-ChildItem -Include AssemblyInfo.cs -Recurse
foreach ($file in $files) {
    Write-Host "Updating $file"
    $content = Get-Content $file -Encoding UTF8
    $content -replace "\d+\.\d+\.(\d+|\*)(\.(\d+|\*))?", $version | Set-Content $file -Encoding UTF8
}
