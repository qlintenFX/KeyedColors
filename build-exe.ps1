#!/usr/bin/env pwsh

# Script to build an executable for KeyedColors application
Write-Host "Building KeyedColors executable..." -ForegroundColor Green

# Define paths
$projectName = "KeyedColors"
$publishDir = ".\publish-output"
$csprojPath = ".\$projectName.csproj"

try {
    # Check if the publish directory exists and delete it
    if (Test-Path $publishDir) {
        Write-Host "Removing previous publication..." -ForegroundColor Yellow
        Remove-Item -Path $publishDir -Recurse -Force
    }

    # Build the application for Windows x64
    Write-Host "Publishing application as self-contained executable..." -ForegroundColor Cyan
    dotnet publish $csprojPath -c Release -r win-x64 --self-contained true -o $publishDir
    
    # Check if build was successful
    if ($LASTEXITCODE -eq 0) {
        # List files in publish directory
        Write-Host "Files in publish directory:" -ForegroundColor Yellow
        Get-ChildItem -Path $publishDir | ForEach-Object { Write-Host "  - $($_.Name)" }
        
        $exePath = Join-Path -Path $publishDir -ChildPath "$projectName.exe"
        
        if (Test-Path $exePath) {
            Write-Host "`nBuild successful!" -ForegroundColor Green
            Write-Host "Executable location: $exePath" -ForegroundColor Green
            Write-Host "Size: $([math]::Round((Get-Item $exePath).Length / 1MB, 2)) MB" -ForegroundColor Green
        } else {
            Write-Host "`nBuild output does not contain expected executable: $exePath" -ForegroundColor Red
            Write-Host "Please check the publish directory for the correct executable name." -ForegroundColor Yellow
        }
    } else {
        Write-Host "`nBuild failed." -ForegroundColor Red
    }
} catch {
    Write-Host "`nError occurred: $_" -ForegroundColor Red
    Write-Host "Exception details: $($_.Exception.Message)" -ForegroundColor Red
} 