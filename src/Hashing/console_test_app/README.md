# Hashing console test app
This little c# console app demonstrates how you can access a custom created canister with c#.

## Usage
Make sure the `hashing canister` (found in src/Hashing/hashing_canister) is running and the `CanisterId` is set in the `appsettings.json` file.

![](pictures/appsettings.png)

# Developer notes
We have used the [ICP.NET](https://github.com/BoomDAO/ICP.NET) library to generate the client code. 

Add the following nuget package `EdjCase.ICP.Agent`

Execute the following code in Visual Studio
```
candid-client-generator init ./
```

This creates the file `candid-client.toml` file which can be used the generate the c# code based on a (*.did) file or directly from a live running canister
```
# Base namespace for all generated files
namespace = "console_test_app.Clients"

# Will create a subfolder within this directory with all the files
output-directory = "./Clients"

# Will override default boundry node url (like for local development)
# Only useful for generating clients from a canister id
#url = "https://localhost:8000"

# Will make generated files in a flat structure with no folders, for all clients
#no-folders = true

[[clients]]
# Defines name folder and api client class name
name = "HashingClient"

# Get definition from a *.did file
#type = "file"

# Path to the *.did file to use
# file-path = "../MyService.did"

# Or use the following to get definition from a canister
# and remove type and file-path from above

# Get the definition from a live canister on the IC
type = "canister"

# Canister to pull definition from
canister-id = "7pon3-7yaaa-aaaab-qacua-cai"

# Override base output directory, but this specifies the subfolder
# output-directory = "./Clients/MyS"

# Will make generated files in a flat structure with no folders, for this client
#no-folders = true
				
# Can specify multiple clients by creating another [[clients]]
#[[clients]]
#name = "MyClient2" 
#type = "file"
#file-path = "../MyService2.did"
				
```

Now execute the following command which creates the (*.cs) files for the ApiClient and Models
```
candid-client-generator ./
```

After that the client can be used. For example
```
IIdentity identity = null;
var agent = new HttpAgent(identity, new Uri(_settings.BaseUrl));
var canisterId = Principal.FromText(_settings.CannisterId);

var client = new HashingClientApiClient(agent, canisterId);
await client.StoreRootHash("0x9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08", new Transaction());
```

Testing
![](pictures/testing.png)