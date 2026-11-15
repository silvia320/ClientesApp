<#
  Uso:  .\scripts\migrations.ps1 -Name AddAlgo
  Crea una migracion nueva en Infrastructure/Persistence/Migrations.
  Equivalente en Visual Studio (Package Manager Console, proyecto por defecto = ClientesApp):
        Add-Migration AddAlgo -OutputDir Infrastructure/Persistence/Migrations
#>
param([Parameter(Mandatory = $true)][string]$Name)

$root = Split-Path $PSScriptRoot -Parent
$proj = Join-Path $root 'src\ClientesApp\ClientesApp.csproj'

dotnet ef migrations add $Name `
    --project $proj --startup-project $proj `
    --context AppDbContext `
    --output-dir Infrastructure/Persistence/Migrations