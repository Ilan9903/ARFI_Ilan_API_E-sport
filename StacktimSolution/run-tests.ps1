dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test --collect:"XPlat Code Coverage"

$reportPath = Get-ChildItem -Path ".\StacktimApi.Tests" -Recurse -Filter "coverage.cobertura.xml" | Sort-Object LastWriteTime -Descending | Select-Object -First 1 -ExpandProperty FullName

reportgenerator -reports:$reportPath -targetdir:"coveragereport" -reporttypes:Html

Start-Process "coveragereport\index.html"

Write-Host "Rapport de couverture généré et ouvert dans le navigateur."