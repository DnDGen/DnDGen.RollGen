param(
    [Parameter(Mandatory)]
    [string]$Version,

    [Parameter(Mandatory)]
    [AllowEmptyCollection()]
    [string[]]$ReleasedVersions
)

$ErrorActionPreference = 'Stop'

$candidate = [version]$Version
$released = @($ReleasedVersions | ForEach-Object { [version]($_ -replace '^v', '') })

if ($released.Count -eq 0) {
    Write-Host "Version $candidate is the first release."
    return
}

$highest = $released | Sort-Object -Descending | Select-Object -First 1

if ($candidate -le $highest) {
    throw "Version $candidate must be greater than the highest released version, $highest."
}

Write-Host "Version $candidate is greater than the highest released version, $highest."
