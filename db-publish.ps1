param(
	[Parameter(Position=0, Mandatory=$false)]
	[string]$Environment = 'Local'
)

# Map environment to profile file (expects files like Local.publish.xml, etc.)
$profileFile = "$Environment.publish.xml"

Write-Host "Building database project..."

dotnet build src\Asm.MooAuth.Database\Asm.MooAuth.Database.sqlproj -c Release -v m

Write-Host "Using environment: $Environment"
Write-Host "Using publish profile: $profileFile"

sqlpackage /Action:Publish /Profile:src\Asm.MooAuth.Database\$profileFile /SourceFile:src\Asm.MooAuth.Database\bin\Release\Asm.MooAuth.Database.dacpac
