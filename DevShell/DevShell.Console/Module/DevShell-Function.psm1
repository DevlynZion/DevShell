
function DevShell-Add-Alias
{
	Param($alias, $value)

	Write-Host $alias " - " $value
	Set-Alias -Scope Global -Name $alias -Value $value
}