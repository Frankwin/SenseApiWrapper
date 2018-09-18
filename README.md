# SenseApiWrapper
Wrapper for the Sense (Energy Monitor) API 

This project provides a C# wrapper for the Sense (Energy Monitor) API. Any contributions are appreciated. I'm currently still working through adding additional functionality for the wrapper as I'm finding out different end points for the API as there's no official documentation for it. 

To make the Unit Tests work you have to edit the appsettings.json and put your Sense email and password in it. Once you authenticate once you'll also get an Access Token that you can put in there as well. At this point it's unknown how long the access token is valid for. Still experimenting with this.

If you have any additional information, feel free to contact me.

This work was based of the work done here: 
* https://github.com/scottbonline/sense 
* https://gist.github.com/mbrownnycnyc/db3209a1045746f5e287ea6b6631e19c
