echo Excluir dados antigos de cobertura:
del /s /q .\Coverage\*.*

echo Gerar dados da cobertura dos testes:
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings --results-directory="./Coverage/CoverageData"

echo Gerar relatorio visual com os dados obtidos no comando anterior:
reportgenerator -reports:.\Coverage\CoverageData\*\coverage.opencover.xml -targetdir:.\Coverage\CoverageReport -reporttypes:Html

echo Abrir relatorio de cobertura no navegador:
.\Coverage\CoverageReport\index.html

pause