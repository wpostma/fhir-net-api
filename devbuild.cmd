powershell build/runbuild.ps1 Package
if "%LOCALNUGET%" NEQ "" call nuget init ./working/ %LOCALNUGET% 
