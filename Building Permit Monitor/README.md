# Building Permit Monitor for Cityworks

A simple system tray/notification area application that monitors our Cityworks database for newly issued building permits.  Building permit information needs to be tracked in a spreadsheet as well as mapped in our GIS, this application streamlines the process by pulling the data from our Cityworks API, parsing out what is needed, displaying a validation window for user verification, then inserting the validated data into the tracking spreadsheet.  The database is checked every 30 minutes for updates, toast notifications inform of work to be done, clicking the notification triggers data retrieval and parsing.  This is my first larger project in C#.

## External Libraries

### Newtonsoft.Json

&nbsp;&nbsp;&nbsp;&nbsp;*Newtonsoft Json.NET Popular high-performance JSON framework for .NET*
  * [newtonsoft.com](https://www.newtonsoft.com/json)

### Microsoft Toolkit Uwp Notifications

&nbsp;&nbsp;&nbsp;&nbsp;*The official way to send toast notifications on Windows 10*
  * [nuget.org](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp.Notifications/)

### Microsoft Office Interop Excel

&nbsp;&nbsp;&nbsp;&nbsp;*Enables managed code to interact with Microsoft Office application's COM-based object model*
  * [learn.microsoft.com](https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/interop/how-to-access-office-interop-objects)

## Using the Tool

When the tool is opened, a system tray/notification area icon is displayed, the tool retrieves an OAuth token from the database and checks for work to be done.  If found, a notification is displayed which can be clicked to begin retrieving and parsing the data.  Once retrieved, the data is displayed in a data validation window - some values may be typed in, others are required by the [Building Permit Parser](https://github.com/AustinBachurski/BuildingPermitParser) tool to calculate data for our annual report and may be automatically selected if the entry was valid in Cityworks, selected from a dropdown menu if not, invalid sections are highlighted in red.  Buttons on the validation window will abort data insertion, open the corresponding Cityworks webpage for the displayed permit, or push the information to the spreadsheet, permit count progression is displayed on the window title in the top left.  A notification is displayed upon successful insertion, the tool automatically checks for additional permits every thirty minutes, but may be checked manually at any time.  Manual checks will produce a 'Nothing to Do' notification if no permits are found.

## Image Gallery

|Right Click Menu|Permit Found Notification|Data Validation Window|
|:-:|:-:|:-:|
|![right click menu](screenshots/RightClick.png) |![permit found](screenshots/PermitFound.png)|![validation](screenshots/Validation.png)
|<b>Spreadsheet Updated Notification</b>|<b>Spreadsheet Values</b>|<b>No Permits to Map Notification</b>|
|![updated](screenshots/SpreadsheetUpdated.png) |![spreadsheet](screenshots/SpreadsheetValues.png)|![nothing to do](screenshots/NothingToDo.png)