@echo off

setlocal

set CONFIGURATION=%1
set FRAMEWORK=%2
set TESTBINARYPATH=.\Test\%TESTPROJECTNAME%\bin\x64\%CONFIGURATION%\%FRAMEWORK%\%TESTPROJECTNAME%.dll

dotnet build ".\Test\%TESTPROJECTNAME%" -c %CONFIGURATION% -f %FRAMEWORK% /p:Platform=x64
if not exist "%TESTBINARYPATH%" goto error_not_built

echo Testing: %CONFIGURATION% %FRAMEWORK%

if exist ".\Test\%TESTPROJECTNAME%\*.log" del ".\Test\%TESTPROJECTNAME%\*.log"
%OPENCOVER_EXE% -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test %TESTBINARYPATH%" "-filter:+[*]* -[%TESTPROJECTNAME%*]*" -output:%RESULTFILEPATH% -mergeoutput
goto end

:error_not_built
echo ERROR: %TESTPROJECTNAME%.dll not built.
goto end

:end
