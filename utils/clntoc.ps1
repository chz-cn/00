
Remove-Item -Path src/_data -Force -ErrorAction SilentlyContinue
Get-ChildItem -Path src -Directory |
Remove-Item -Path { $_.FullName + "\toc.yml" } -ErrorAction SilentlyContinue
