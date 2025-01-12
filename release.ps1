# Build and package Search plugin
dotnet publish Flow.Launcher.Plugin.SearchUnicode.Search -c Release -r win-x64 --no-self-contained
$tempDir = "Flow.Launcher.Plugin.SearchUnicode.Search/bin/Release/win-x64/Flow.Launcher.Plugin.SearchUnicode.Search"
if (Test-Path $tempDir) { Remove-Item -Path $tempDir -Recurse -Force }
Copy-Item -Path "Flow.Launcher.Plugin.SearchUnicode.Search/bin/Release/win-x64/publish" -Destination $tempDir -Recurse
Compress-Archive -LiteralPath $tempDir -DestinationPath Flow.Launcher.Plugin.SearchUnicode.Search/bin/SearchUnicode.Search.zip -Force
Remove-Item -Path $tempDir -Recurse -Force

# Build and package Identify plugin
dotnet publish Flow.Launcher.Plugin.SearchUnicode.Identify -c Release -r win-x64 --no-self-contained
$tempDir = "Flow.Launcher.Plugin.SearchUnicode.Identify/bin/Release/win-x64/Flow.Launcher.Plugin.SearchUnicode.Identify"
if (Test-Path $tempDir) { Remove-Item -Path $tempDir -Recurse -Force }
Copy-Item -Path "Flow.Launcher.Plugin.SearchUnicode.Identify/bin/Release/win-x64/publish" -Destination $tempDir -Recurse
Compress-Archive -LiteralPath $tempDir -DestinationPath Flow.Launcher.Plugin.SearchUnicode.Identify/bin/SearchUnicode.Identify.zip -Force
Remove-Item -Path $tempDir -Recurse -Force

# Build and package Emoji plugin
dotnet publish Flow.Launcher.Plugin.SearchUnicode.Emoji -c Release -r win-x64 --no-self-contained
$tempDir = "Flow.Launcher.Plugin.SearchUnicode.Emoji/bin/Release/win-x64/Flow.Launcher.Plugin.SearchUnicode.Emoji"
if (Test-Path $tempDir) { Remove-Item -Path $tempDir -Recurse -Force }
Copy-Item -Path "Flow.Launcher.Plugin.SearchUnicode.Emoji/bin/Release/win-x64/publish" -Destination $tempDir -Recurse
Compress-Archive -LiteralPath $tempDir -DestinationPath Flow.Launcher.Plugin.SearchUnicode.Emoji/bin/SearchUnicode.Emoji.zip -Force
Remove-Item -Path $tempDir -Recurse -Force
