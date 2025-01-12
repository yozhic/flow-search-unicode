dotnet publish Flow.Launcher.Plugin.SearchUnicode.Identify -c Debug -r win-x64 --no-self-contained
dotnet publish Flow.Launcher.Plugin.SearchUnicode.Search -c Debug -r win-x64 --no-self-contained
dotnet publish Flow.Launcher.Plugin.SearchUnicode.Emoji -c Debug -r win-x64 --no-self-contained

$AppDataFolder = [Environment]::GetFolderPath("ApplicationData")
$flowLauncherExe = "$env:LOCALAPPDATA\FlowLauncher\Flow.Launcher.exe"

if (Test-Path $flowLauncherExe) {
    Stop-Process -Name "Flow.Launcher" -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2

    if (Test-Path "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Identify") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Identify"
    }
    if (Test-Path "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Search") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Search"
    }
    if (Test-Path "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Emoji") {
        Remove-Item -Recurse -Force "$AppDataFolder\FlowLauncher\Plugins\SearchUnicode.Emoji"
    }

    Copy-Item "Flow.Launcher.Plugin.SearchUnicode.Identify\bin\Debug\win-x64\publish" "$AppDataFolder\FlowLauncher\Plugins\" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\Plugins\publish" -NewName "SearchUnicode.Identify"
    Copy-Item "Flow.Launcher.Plugin.SearchUnicode.Search\bin\Debug\win-x64\publish" "$AppDataFolder\FlowLauncher\Plugins\" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\Plugins\publish" -NewName "SearchUnicode.Search"
    Copy-Item "Flow.Launcher.Plugin.SearchUnicode.Emoji\bin\Debug\win-x64\publish" "$AppDataFolder\FlowLauncher\Plugins\" -Recurse -Force
    Rename-Item -Path "$AppDataFolder\FlowLauncher\Plugins\publish" -NewName "SearchUnicode.Emoji"

    Start-Sleep -Seconds 2
    Start-Process $flowLauncherExe
} else {
    Write-Host "Flow.Launcher.exe not found. Please install Flow Launcher first"
}
