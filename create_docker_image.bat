cd src
cd BattlEyeManager.Spa
dotnet publish -c release
cd ..
cd BattlEyeManager.React
npm run build
cd ..
cd ..

docker build -t tym32167/battleyemanager .

:: docker run -d -p 8080:80 --name BattlEyeManager.Spa -e "ConnectionStrings__DefaultConnection=..." tym32167/battleyemanager
:: docker save tym32167/battleyemanager -o battleyemanager.tar

:: docker push tym32167/battleyemanager

