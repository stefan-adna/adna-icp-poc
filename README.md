 # MS1: Development Environment Setup and Exploration of ICP.NET
 Objectives:
 1. Development Environment Setup: Select and set up all necessary 
development tools and environments for the project, including integration with 
ICP.NET. This involves setting up development servers, version control 
systems, and configuring the development environment for C#.
 2. ICP.NET Exploration: Conduct a thorough exploration of the ICP.NET library to 
gain an in-depth understanding of its workings, architecture, and the interfaces 
it provides. This includes studying the documentation, best practices, and 
possibly following tutorials or courses.
 3. Development of a Small C# Application: Design and develop a small, 
functional C# application that utilizes specific features of ICP.NET. This 
application serves to gain practical experience with the library and deepen the 
understanding of its application.

# Deliverables
We have created 2 sample projects that use the ICP.NET library for accessing the ICP network with c#.

## Hashing
Small console application that uses our custom build hashing canister for storing and retrieving root hashes. The Client was created using the [ICP.NET](https://github.com/BoomDAO/ICP.NET) Library

## DocumentStorage
Small console application that uses the ICP default AssetCanister to store and retrieve files.

We have discovered multiple issues in ICP.NET, but with our feedback the ICP.NET developer has fixed them

* [~~AssetCanisterApiClient.UploadAssetChunkedAsync - file is not chunked correctly #123~~](https://github.com/BoomDAO/ICP.NET/issues/123)
* [~~AssetCanisterApiClient.MAX_CHUNK_SIZE is too high #124~~](https://github.com/BoomDAO/ICP.NET/issues/124)
* [~~AssetCanisterApiClient.GetAsync - (Download Asset) is only returning first chunk #125~~](https://github.com/BoomDAO/ICP.NET/issues/125)