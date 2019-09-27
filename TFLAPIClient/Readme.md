
TFL WEB Api Road Query Client
========================================
Release Notes
-------------
25.09.2019 Version 1.0 Created with only road query option

Packages
--------
Required platform Microsoft.NET Core 2.2.0
And SDK can be downloaded from the following link : [https://dotnet.microsoft.com/download/dotnet-core/2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
| **Development Packages** | 
| -------------------- | 
| Newtonsoft.Json|
| Microsoft Configuration|
| Microsoft Dependency Injection| 
| Microsoft Logging Extensions|

| **Testing (TDD & BDD) Packages** | 
| ---------------------------- | 
| Xbehave for BDD|
| xUnit|
| Microsoft Dependency Injection| 
| FluentAssertions|

Features
--------
TFL Client API is a simply (and very simple indeed) console application to query road status given as a paramater.
It is designed by considering SOLID, asyncronous calls and easy extensibility. And as a framework Microsoft's new shiny not windows dependent platform any more .net core 2.2 used. Maybe can also work with latest version 3.0 however didn't test it.

Development Requirements
----------------------------------------
To use TFL Web API, as a first step and before the everything  from the https://api-portal.tfl.gov.uk/ portal you should be register as developer and activate your account than from the portal, 'app_id' and 'app_key' data should be taken and recorded in 'appsettings.json' configuration file.

Clonening the project 
----------------------------------------
From the command line first change your directory to which you want to reside the codes and use the following command line either using git bash or command prompt:
**git clone  [https://github.com/mremrah/TflAPIClient](https://github.com/mremrah/TflAPIClient)**
than you should see the below directory structure in your current directory

Structure
----------------------------------------
Solution contains two projects:
./TFL-WEB-API
|--->  TflApiClient
|---> Test.TflApiClient
The first one is implementation of TFL Web API 
Second project is the name suggests testing for the client project.

Restore &  Publish & Running
----------------------------------------
After cloning the project and before building the app, all required development packages should be restored manually (or in build time automatically) by following command in command prompt after changing the Client API project directory:
**cd TFL-WEB-API/TFLAPIClient**
**dotnet restore**
After restoring all the packages, now client app can be published as a **DLL** with the following command:
**dotnet publish -c Release -o ./publish** 
this command will put all required file inside the **publish** folder and application can be run by using the following commands:
**cd publish**
**dotnet RoadStatus.dll**
By running the application without any argument it should print the **how to use** screen.
And  to query one of the road in London etc. A2, you can use the following command:
**dotnet RoadStatus.dll A2**
And this command shows the road status as follows:
*The status of the A2 as follows
        Road Status is Good
        Road Status Description is No Exceptional Delays*
 Application after completion sets the exit code from the command if type :
 **echo %errorlevel% ** you should see **0**. 
 In case of if you are using PowerShell exit code can be queried with following command:
 **echo $lastexitcode** this should also shows **0**
 
If you want to publish **EXE** file instead of **DLL** file the following command can be used.
**dotnet publish -c Release -r win-x64 -o ./publish **
the **-r** parameter (stands for **runtime**) defines for which platform **EXE** will be generated. More details can be found at Microsoft's Runtime Identifier Catalog (RID Catalog): [https://docs.microsoft.com/en-us/dotnet/core/rid-catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog)
So for Windows x86 architecture publish command should be as follows:
**dotnet publish -c Release -r win-x86 -o ./publish **

After creating **EXE** file you don't need prefixing with **dotnet** and can directly run your application:
**RoadStatus.dll A2**
This command will also the same successfull query message.

If you enter invalid Road name program the **DLL** and **EXE** both will shows an **Not Found** or invalid query result message:
*A2aa is not a valid road!*
and **%errorlevel%** (or **$lastexitcode**) should be **1**


Testing the Application
------------------------------------------------------------
For testing XBehave BDD (along with xUnit) used and after changing current directory to the testing application directory the following commands can be used:
**cd ../../Test-TFLAPIClient**
**dotnet restore**
**dotnet test**
Please note; for changing the directory we assume you were in Client Application folder publish and go two folder up and than enter into the Test project folder.
Dotnet restore command is also needed for the first time test later you can basically skip it.
As a final result Test command should generate the following similiar output:
*Microsoft (R) Test Execution Command Line Tool Version 16.2.0-preview-20190606-02
Copyright (c) Microsoft Corporation.  All rights reserved.
Starting test execution, please wait...
Test Run Successful.
Total tests: 57
     Passed: 57
 Total time: 2.5040 Seconds*
