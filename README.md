# PRG2 Assignment

## Team Members

* Qin Guan
* Richard Paul Pamintuan

## Setup

This solution uses .NET5, please install the .NET5 runtime to run the program.

The solution contains 2 projects:

* `Cli`: The CLI interface the user will be interfacing with
* `Core`: All domain related stuff (i.e. Models, Repository Interfaces, etc)

## Cli project structure

The `Cli` project is a .NET Console App. It also makes use of Dependency Injection to make my life easier... I think.

## Core project structure

The `Core` project is a .NET5 Class Library. It follows Clean Architecture, which splits code into 3 major categories:

* Infrastructure (in this case it is the `Cli` project)
  * The infrastructure layer is the external part that the user interacts with and could be a website or API. This layer usually changes often.

* Use Cases (`Core.UseCase`)
  * The use cases layer represents the business actions (i.e. what you can do with your application). This layer should contain only business logic.

* Repository (`Core.Repository`)
  * The repository layer retrieves and store data from and to different sources.

* Entities/Models (`Core.Models`)
  * The entity layer represents the domain objects
  

