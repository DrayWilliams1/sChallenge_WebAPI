# sChallenge_WebAPI
An .NET Core Web API demonstration.
## Summary
A back-end web API created in C# using .NET Core and EF Core. The data is stored in an InMemory database for ease of deployment on tester's machines (no local database setup required). The database stores/facilitates operations around database user's (those who will sign into the applications) and patients (those who will have health details logged). The database is initialized with a few entities of each type to ensure testing can begin out-of-the-box.

**Note:** This app was completed as a side-project under a time-limit so some time-optimizing adjustments were made to provide basic system functionality.
## How to Run
Download the repo and open the solution in Visual Studio. Select 'Debug --> Run Without Debugging' and the api will start up on "https://localhost:44322/api/Users" showing a successful retrieval of the generated user data.

**For testing purposes**:
- Agent User || username: **agent1** password: **password**
- Supervisor User || username: **super1** password: **password**
