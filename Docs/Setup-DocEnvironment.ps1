# Install Winget
$downloadsFolder = Join-Path $env:USERPROFILE -ChildPath "Downloads"
Invoke-WebRequest -Uri "https://aka.ms/Microsoft.VCLibs.x64.14.00.Desktop.appx" -OutFile (New-Item -Path "$downloadsFolder\Microsoft.VCLibs.x64.14.00.Desktop.appx" -Force)
Add-AppPackage "$downloadsFolder\Microsoft.VCLibs.x64.14.00.Desktop.appx"
Invoke-WebRequest -Uri "https://github.com/microsoft/winget-cli/releases/download/v1.1.12653/Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.msixbundle" -OutFile (New-Item -Path "$downloadsFolder\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.appxbundle")
Add-AppPackage "$downloadsFolder\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe.appxbundle"

# Install Windows App Runtime 1.0
Invoke-WebRequest -Uri "https://aka.ms/windowsappsdk/1.0-stable/msix-installer" -OutFile (New-Item -Path "$downloadsFolder\Microsoft.WindowsAppRuntime.Redist.1.0.0.zip")
Expand-Archive "$downloadsFolder\Microsoft.WindowsAppRuntime.Redist.1.0.0.zip" -DestinationPath "$downloadsFolder\Microsoft.WindowsAppRuntime.Redist.1.0.0\"
Start-Process "$downloadsFolder\Microsoft.WindowsAppRuntime.Redist.1.0.0\WindowsAppSDK-Installer-x64\WindowsAppRuntimeInstall.exe"

# Install .Net 6 Runtime
Invoke-WebRequest -Uri "https://download.visualstudio.microsoft.com/download/pr/89f0ba2a-5879-417b-ba1d-debbb2bde208/b22a9e9e4d513e4d409d2222315d536b/dotnet-sdk-6.0.200-win-x64.exe" -OutFile (New-Item -Path "$downloadsFolder\dotnet-sdk-6.0.200-win-x64.exe")
Start-Process "$downloadsFolder\dotnet-sdk-6.0.200-win-x64.exe"

# Install software using Winget
winget install Microsoft.dotnetRuntime.6-x64 --exact
winget install Google.Chrome --exact
winget install Mozilla.Firefox --exact
winget install Opera.Opera --exact

# Generate chrome profiles
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="Personal"
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="School"
Start-Process "C:\Program Files\Google\Chrome\Application\chrome.exe" --profile-directory="Work"
