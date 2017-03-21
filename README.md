There are many Notes apps, most will always be better than my examples, but the main reason for me rolling my own note taking system was purely to have something to test my skills on.  The beauty of a notes project is that there are ligitimate reasons for having the ability on multiple platforms.  My intention is to develop apps for Web, Windows (Classic via WPF and UWP), Android, iOS, Amazon Alexa, and then possibly MacOS, Siri, Google Home, and Cortana.

The core of the project will be the API.  All the clients will use this API to communicate with the database. 

In keeping with the Agile approach of starting with a small set of features, the first version of the API will be extremely minmumal.  

Here are the basic API commands for first version:

GET  /api/note  	- Get All Notes;
GET  /api/note/{id}	- Get a specific Note;
POST /api/note		- Create a new note;
PUT / api/note/{id}	- Update an existing note;

Obviously the first version is not production level and will very quickly need to be expanded.
Once the first version of the API is done I'll next move onto the Windows and Web apps.  
