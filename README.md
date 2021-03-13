![.Net version ](https://img.shields.io/badge/.NET-5.0-blue)
![Release](https://img.shields.io/github/downloads/jimmymeister98/MetaProgrammProjekt/total)
# MetaProgrammProjekt
A Program to generate a class library from a uml diagram (.json file)

## Dependencies
-  [Newtonsoft Json.NET](https://www.newtonsoft.com/json) [![NuGet version (Newtonsoft.Json)](https://img.shields.io/nuget/v/Newtonsoft.Json.svg?style=flat-square)](https://www.nuget.org/packages/Newtonsoft.Json/)

### Why those Dependencies?
Of course i could've written my own json parser but this one is proven good, free for commercial use and probably had more development time than me for this project.

## Usage

You choose a .json file which was generated via [Umple](https://cruise.umple.org/umpleonline/). The program will then generate a classlib at the chosen destination and takes care
of dependencies between the classes. Multiplicity is provided by generating a "listclass" which should act as a container for the objects of the correlating object.

## Download

Download the current version from the [releases](https://github.com/jimmymeister98/MetaProgrammProjekt/releases/tag/v1.0) page.

## Acknowledgements
- Shoutouts to the Devs of the [Newtonsoft Json.NET](https://www.newtonsoft.com/json) for their truly great work!



