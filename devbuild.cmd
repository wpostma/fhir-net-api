powershell build/runbuild.ps1 Package
if "%LOCALNUGET%" NEQ "" call nuget install ./working/ %LOCALNUGET% 
