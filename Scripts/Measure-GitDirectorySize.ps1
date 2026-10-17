[CmdletBinding()]
param(
    [ValidateNotNullOrEmpty()]
    [string]$OutputDirectory = 'C:\tmp',
    [ValidateNotNullOrEmpty()]
    [string]$OutputFileName = 'git-size.txt',
    [ValidateNotNullOrEmpty()]
    [string]$TimestampFormat = 'dd.MM.yyyy HH:mm',
    [ValidateNotNullOrEmpty()]
    [string]$GitDirectory = '.git',
    [ValidateNotNullOrEmpty()]
    [string]$RepositoryRoot = $PSScriptRoot

)

$gitDirectory = Join-Path -Path $RepositoryRoot -ChildPath $gitDirectory

if (-not (Test-Path -LiteralPath $gitDirectory -PathType Container)) {
    throw "Git-Verzeichnis wurde nicht gefunden: $gitDirectory"
}

$measuredSize = Get-ChildItem -LiteralPath $gitDirectory -File -Recurse -Force -ErrorAction Stop |
    Measure-Object -Property Length -Sum
$sizeInBytes = [long]0
if ($null -ne $measuredSize.Sum) {
    $sizeInBytes = [long]$measuredSize.Sum
}

if (-not (Test-Path -LiteralPath $outputDirectory -PathType Container)) {
    New-Item -ItemType Directory -Path $outputDirectory -Force -ErrorAction Stop | Out-Null
}

$outputFile = Join-Path -Path $outputDirectory -ChildPath $outputFileName
$timestamp = Get-Date -Format $timestampFormat
$record = "$timestamp`t$sizeInBytes"

if (Test-Path -LiteralPath $outputFile -PathType Leaf) {
    Add-Content -LiteralPath $outputFile -Value $record -Encoding utf8 -ErrorAction Stop
}
else {
    Set-Content -LiteralPath $outputFile -Value @("Timestamp`tSizeBytes", $record) -Encoding utf8 -ErrorAction Stop
}

Write-Output "Git-Verzeichnis: $sizeInBytes Bytes ($outputFile)"