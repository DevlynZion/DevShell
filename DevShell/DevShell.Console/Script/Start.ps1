Write-Host "DevShell"

Import-Module .\Module\DevShell-Function.psm1 -DisableNameChecking 

.\Script\ImportModules
.\Script\Alias.ps1
.\Script\SetPrompt.ps1
#.\Script\FixConnection.ps1
.\Script\TestFix.ps1