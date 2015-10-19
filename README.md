# ColorVoter
ColorVoter is a simple SignalR app with a single hub and an AngularJS/Bootstrap frontend. Neither .NET MVC or WebApi controllers are used at all. 
  The core references are included, though, and the dependency injector is configured for controllers as well as hubs.

What is in this app?
  1. SignalR 2.2
  2. Microsoft OWIN (required by SignalR)
  3. Simple Injector (for dependency injection)
  4. jQuery 2.1.4 (required by SignalR)
  5. AngularJS
  6. Bootstrap LESS / CSS
  
### Getting started with SignalR
To get started with your own SignalR app, use Visual Studio to create an empty Web Application. You can choose to include the core folders
and references to MVC or WebAPI if you wish. The web application in this example is called "WebLibrary".

Using NuGet, install the following packages:
  * jQuery
  * Microsoft.AspNet.SignalR
  
If you don't install jQuery first, the SignalR package will install it for you,
but it will install version 1.6.4, which is the minimum dependency. jQuery is currently on version 2.4.x.

### Configuring your app to use SignalR
At the root of your project (or wherever you feel most comfortable) add a new .cs file called Startup.cs. Replace the contents of the file with the following:

```cSharp
[assembly: OwinStartup(typeof(Startup))]
namespace WebLibrary
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
```

Now that the app is set up use SignalR, we can add a hub class to the application. Create a folder at the root level called Hubs and add a new class which inherits from Hub. 
In the ColorVoter app, the hub is called ColorHub. At its simplest, a hub looks like this:
```cSharp
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
namespace WebLibrary.Hubs
{
    public class ColorHub : Hub
    {
        // Add hub methods to call client scripts and respond to client updates.
    }
}
```
This hub is currently empty but as you can see, it is simply a C# class that inherits from Hub. Any public methods we put in this
class will be callable from a Javascript client. Similarly, we can call registered javascript methods from this hub instance.

### Creating a client who can communicate with the server
Create an html page in your app called index.html and edit it to be the following:
```html
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SignalR Demo</title>
</head>
<body>

<p>I am the SignalR App!</p>

<!--SignalR has a dependency on jQuery-->
<script src="Scripts/jquery-2.1.4.min.js"></script>
<!--SignalR JS Script-->
<script src="Scripts/jquery.signalR-2.2.0.min.js"></script>
<!--Reference script that SignalR will create for us automatically-->
<script src="signalr/hubs"></script>
<!--Reference our custom JS for the page -->
<script src="Scripts/voter.js"></script>
</body>
</html>
```
Notice the 4 scripts we reference at the end of the body tag. 
 * jQuery - This one is required for SignalR
 * jQuery.signalR - This is the JS that SignalR uses behind the scenes
 * signalr/hubs - This is a file that SignalR creates on our behalf when the application starts and facilitates the communication between serer and client. 
 * voter.js - This is the file that we will write to handle the logic of our app
 
 Add the voter.js script file to the Scripts folder in the web project and edit it to be the following:
 ```javascript
 (function(){
    var colorHub = $.connection.colorHub;
    colorHub.client.exampleClientFunction = function(serverResponse){
      // This is a function that the server can call
      alert(serverResponse);
    };
    $.connection.hub.start().done(function () {
        // This will run when the connection has been initiated between client and server
        $("#someElement").click(function(){
          colorHub.server.exampleServerFunction("Hello!");
        });
    });
 }());
 ```
 On the server Hub, edit the file to include the following:
 ```cSharp
 using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
namespace WebLibrary.Hubs
{
    public class ColorHub : Hub
    {
        public void ExampleServerFunction(string message){
          // Process message and alert all listening clients
          Clients.All.exampleClientMethod("Message received: " + message);
        }
    }
}
```
In our JS, we called colorHub.server.exampleServerFunction() with a lower-case name. The C# function should be declared in C#-style with a capital letter. SignalR expects that 
Javascript will be lowercase and server methods will be uppercase. This can be overridden, but is the default convention.

We call Clients.All.nameOfSomeJavascriptMethod to call the method "nameOfSomeJavascriptMethod" on all clients that are listening. We could have written 
Clients.Caller.nameOfSomeJavascriptMethod to only communicate with the client who called the function, or we can specify particular client IDs, or groups of IDs, depending on the need.

### Conclusion
At this point, you should be familiar with what is possible with just a few simple lines. Try adding other more complicated logic to update
the page or communicate with individual clients. 
