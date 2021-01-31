# Windows installation

In order to seutp website, you need

1. build website first 
2. deploy build to IIS 

You can build website on yur own machine and deploy it on web server, however here in instructions we will do both parts on same web server. 


## Tools to create build 

1. You need git to download sources from githiub. 
2. You need to install asp.net sdk and nodejs in order to build sources. 

### Asp.NET SDK 

ASP.NET SDK 5.x

https://dotnet.microsoft.com/download/dotnet/5.0

Choose "windows x64 SDK" and you will be redirected to

https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.102-windows-x64-installer

### Web tools 

NodeJs
https://nodejs.org/en/


### Editor

VS Code
https://code.visualstudio.com/

### Git installation

Git for windows 
https://git-scm.com/download/win

---

## Tools to setup website

1. you need runtime to run asp.net 5 website. 
2. you need to have mariadb/mysql on your hosting 

### Asp.Net 

ASP.NET Core Runtime 5.x
https://dotnet.microsoft.com/download/dotnet/5.0

Choose "windows hosting bundle" and you will be redirected to

Hosting bundle 
https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-aspnetcore-5.0.2-windows-hosting-bundle-installer


### Database tools

Mariadb
https://mariadb.org/download/

Dont forget to use "Utf8" as default charset and you also can add HeidiSql into installation or you can install it separately. 

HeidiSql
https://www.heidisql.com/download.php


## Setting up

### Build website content

1. Create folders for dev and prod (i.e. `C:\dev` and `C:\prod`)
2. open command line, navigate to dev folder 
3. `git clone --recurse-submodules https://github.com/tym32167/battleyemanager.git`

To build Frontend

1. go to `C:\dev\battleyemanager\src\BattlEyeManager.React` folder 
2. execute `npm install`
2. execute `npm run build`

to build backend 

1. go to `C:\dev\battleyemanager\src\BattlEyeManager.Spa` folder 
2. execute `dotnet build`
2. execute `dotnet publish -c release`

Now we are ready to combine build 

1. goto `C:\prod`
2. create `battleyemanager` folder here
3. goto `C:\prod\battleyemanager`
4. create `website` folder here
5. goto `C:\prod\battleyemanager\website`
6. create `wwwroot` folder here
7. copy content of `C:\dev\battleyemanager\src\BattlEyeManager.Spa\bin\release\net5.0\publish` to `C:\prod\battleyemanager\website`
8. copy content of `C:\dev\battleyemanager\src\BattlEyeManager.React\build` to `C:\prod\battleyemanager\website\wwwroot`
9. open file `"C:\prod\battleyemanager\website\appsettings.json"` and update string `"DefaultConnection": "server=localhost; database=battleyemanager; port=3306; user=root; password=<your password here>"`

At this moment we have website content ready for deployment in folder `C:\prod\battleyemanager\website`. 

### Deploy website content

We are going to follow following guides: 

Host ASP.NET Core on Windows with IIS
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/?view=aspnetcore-5.0

Guide
https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-5.0&tabs=visual-studio

1. Grant access to `C:\prod\battleyemanager\website` for `IIS_IUSRS` group to read and write

2. Open HeidiSQL and create datatbase `battleyemanager`

3. Open IIS (if you dont have IIS, you need to install if from server dashboard)

4. Add application pool with parameters
    - name `battleyemanager_pool`
    - .NET CLR version `No managed Code`
    - Managed pipeline `Integrated`    
    - Start application `checked`

5. Open `advanced settings` for `battleyemanager_pool`
    - Start Mode `Always Running`
    - Idle Timeout (minutes) `0`
    - Regular time interval (minutes) `0`

6. Add website "battleyemanager" with parameters: 
    - name `battleyemanager`
    - Application pool `battleyemanager_pool`
    - physical path `C:\prod\battleyemanager\website`
    - Port `58175`
7. Open `http://localhost:58175/` and try login with default credentials: login `admmin`, password `12qw!@QW`
