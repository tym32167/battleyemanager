cd ..\src\BattlEyeManager.Spa
rmdir /s /q wwwroot
cd ..\BattlEyeManager.React
npm run build
xcopy /s "build" "..\BattlEyeManager.Spa\wwwroot\"
