function Get-InvocationScriptName {
    $invocationScriptName = Get-PSCallStack | Where-Object ScriptName -ne $null | Select-Object -Last 1 | Select-Object ScriptName -ExpandProperty ScriptName

    return $invocationScriptName
}

Get-PSCallStack | Where-Object ScriptName -ne $null | Select-Object -Last 1 | Select-Object ScriptName -ExpandProperty ScriptName

function Invoke-ElevateAsAdminPowershell {
    
    $scriptFilename = Get-InvocationScriptName

    if (-Not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] 'Administrator'))
    {
        if ([int](Get-CimInstance -Class Win32_OperatingSystem | Select-Object -ExpandProperty BuildNumber) -ge 6000)
        {
            Start-Process powershell -Verb RunAs -ArgumentList "-ExecutionPolicy Bypass -File `"$($scriptFilename)`""
            Exit
        }
    }
}

Invoke-ElevateAsAdminPowershell

$ProgressPreference = 'SilentlyContinue'

# Install Winget
Write-Information("Installing Winget")
$downloadsFolder = Join-Path $env:USERPROFILE -ChildPath "Downloads"
Invoke-WebRequest -Uri "https://aka.ms/Microsoft.VCLibs.x64.14.00.Desktop.appx" -OutFile (New-Item -Path "$downloadsFolder\Microsoft.VCLibs.x64.14.00.Desktop.appx" -Force)
Add-AppPackage "$downloadsFolder\Microsoft.VCLibs.x64.14.00.Desktop.appx"
Invoke-WebRequest -Uri "https://github.com/microsoft/winget-cli/releases/download/v1.1.12653/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle" -OutFile (New-Item -Path "$downloadsFolder\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.appxbundle")
Add-AppPackage "$downloadsFolder\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.appxbundle"
Write-Information("Winget installed")

# Install Windows App Runtime 1.0.3
Write-Information("Installing Windows App Runtime 1.0.3")
Invoke-WebRequest -Uri "https://aka.ms/windowsappsdk/1.0/1.0.3/windowsappruntimeinstall-1.0.3-x64.exe" -OutFile (New-Item -Path "$downloadsFolder\windowsappruntimeinstall-1.0.3-x64.exe")
Start-Process "$downloadsFolder\windowsappruntimeinstall-1.0.3-x64.exe"
Write-Information("Windows App Runtime 1.0.3 installed")

$ProgressPreference = 'Continue'

# Install software using Winget
winget install Microsoft.dotnetRuntime.6-x64 --exact
winget install Google.Chrome --exact
winget install Mozilla.Firefox --exact
winget install Opera.Opera --exact

# Generate chrome profiles
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="Personal"
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="School"
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="Work"
