cd src
cd BattlEyeManager.Spa
dotnet publish -c release
cd ..
cd BattlEyeManager.React
npm run build
cd ..
cd ..
docker build -t battleyemanager .
docker run -d -p 8080:80 --name BattlEyeManager.Spa battleyemanager
docker save battleyemanager -o battleyemanager.tar
