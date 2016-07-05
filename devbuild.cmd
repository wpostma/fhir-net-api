@echo off
del /s /q devbuild/ >nul 2>nul
powershell build/runbuild.ps1 Package
if "%LOCALNUGET%" NEQ "" call nuget init ./working/ %LOCALNUGET% 
