FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY src/BattlEyeManager.Spa/bin/release/net5.0/publish/ App/
COPY src/BattlEyeManager.React/build/ App/wwwroot
WORKDIR /App
ENTRYPOINT ["dotnet", "BattlEyeManager.Spa.dll"]
