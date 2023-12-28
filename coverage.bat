@echo off

setlocal

call ..\Certification\set_tokens.bat

set PROJECTNAME=XamlConverterLibrary
set TESTPROJECTNAME=%PROJECTNAME%.Test
set RESULTFILENAME=Coverage-%PROJECTNAME%.xml
set RESULTFILEPATH=".\Test\%TESTPROJECTNAME%\obj\x64\%RESULTFILENAME%"

set OPENCOVER_VERSION=4.7.1221
set OPENCOVER=OpenCover.%OPENCOVER_VERSION%
set OPENCOVER_EXE=".\packages\%OPENCOVER%\tools\OpenCover.Console.exe"

set REPORTGENERATOR_VERSION=5.2.0
set REPORTGENERATOR=ReportGenerator.%REPORTGENERATOR_VERSION%
set REPORTGENERATOR_EXE=".\packages\%REPORTGENERATOR%\tools\net8.0\ReportGenerator.exe"

nuget install OpenCover -Version %OPENCOVER_VERSION% -OutputDirectory packages
nuget install ReportGenerator -Version %REPORTGENERATOR_VERSION% -OutputDirectory packages

if not exist %OPENCOVER_EXE% goto error_console1
if not exist %REPORTGENERATOR_EXE% goto error_console2

if exist %RESULTFILEPATH% del %RESULTFILEPATH%
call runtests.bat Debug net481
call runtests.bat Debug net8.0-windows7.0
call runtests.bat Release net481
call runtests.bat Release net8.0-windows7.0

%REPORTGENERATOR_EXE% -reports:%RESULTFILEPATH% -targetdir:.\CoverageReports "-assemblyfilters:-Microsoft*;+%PROJECTNAME%" "-filefilters:-*.g.cs"

goto end

:error_console1
echo ERROR: OpenCover.Console not found.
goto end

:error_console2
echo ERROR: ReportGenerator not found.
goto end

:end
