[CmdletBinding()]
param()

$repositoryRoot = Split-Path -Parent $PSScriptRoot
$gitDirectory = Join-Path -Path $repositoryRoot -ChildPath '.git'

if (-not (Test-Path -LiteralPath $gitDirectory -PathType Container)) {
    throw "Git-Verzeichnis wurde nicht gefunden: $gitDirectory"
}

$measuredSize = Get-ChildItem -LiteralPath $gitDirectory -File -Recurse -Force -ErrorAction Stop |
    Measure-Object -Property Length -Sum
$sizeInBytes = [long]0
if ($null -ne $measuredSize.Sum) {
    $sizeInBytes = [long]$measuredSize.Sum
}

$outputDirectory = 'C:\tmp'
if (-not (Test-Path -LiteralPath $outputDirectory -PathType Container)) {
    New-Item -ItemType Directory -Path $outputDirectory -Force -ErrorAction Stop | Out-Null
}

$outputFile = Join-Path -Path $outputDirectory -ChildPath 'git-size.txt'
$timestamp = Get-Date -Format 'dd.MM.yyyy HH:mm'
$record = "$timestamp`t$sizeInBytes"

if (Test-Path -LiteralPath $outputFile -PathType Leaf) {
    Add-Content -LiteralPath $outputFile -Value $record -Encoding utf8 -ErrorAction Stop
}
else {
    Set-Content -LiteralPath $outputFile -Value @("Timestamp`tSizeBytes", $record) -Encoding utf8 -ErrorAction Stop
}

Write-Output "Git-Verzeichnis: $sizeInBytes Bytes ($outputFile)"