Write-Host "Lancement du script de test et de couverture..." -ForegroundColor Green

Write-Host "Vérification de l'outil ReportGenerator..."
dotnet tool install -g dotnet-reportgenerator-globaltool

Write-Host "Exécution des tests et collecte de la couverture de code..."
dotnet test --collect:"XPlat Code Coverage"

Write-Host "Recherche du fichier de résultats..."
$reportPath = Get-ChildItem -Path ".\StacktimApi.Tests" -Recurse -Filter "coverage.cobertura.xml" | Sort-Object LastWriteTime -Descending | Select-Object -First 1 -ExpandProperty FullName

Write-Host "Génération du rapport HTML..."
reportgenerator -reports:$reportPath -targetdir:"coveragereport" -reporttypes:Html

Write-Host "Ouverture du rapport dans le navigateur..."
Start-Process "coveragereport\index.html"

Write-Host "Script terminé avec succès !" -ForegroundColor Green