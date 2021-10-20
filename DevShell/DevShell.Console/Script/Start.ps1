Import-Module .\Module\DevShell-Function.psm1 -DisableNameChecking 

.\Script\ImportModules
.\Script\EnvironmentVariables.ps1
.\Script\Alias.ps1
.\Script\SetPrompt.ps1

pause

DS-Boot