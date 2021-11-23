# Introduction

This document is to serve as design notes and documentation for Blue Harvest.

# References

[Markdown Ref](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet) <br>
[DTO Naming](https://richarddingwall.name/2010/04/17/try-not-to-call-your-objects-dtos/)

Web/RESTful Refs:<br>
[First Web API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio) <br>
[CRUD API design](https://stoplight.io/blog/crud-api-design/) <br>
[Use CreateXXX](https://stackoverflow.com/questions/37839278/asp-net-core-rc2-web-api-post-when-to-use-create-createdataction-vs-created) <br>
[CQRS in ASPNET Core](https://code-maze.com/cqrs-mediatr-in-aspnet-core/) <br>
[Pragmatic RESTful API](https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api) <br>

| API | route | Description | Request Body | Response Body |
|:---|:---|:---|:---|:---|
| POST | /api/persons | Add a Person | Person | Person |
| GET | /api/persons/{id} | Get a Person by ID | None | Person |
| GET | /api/persons | Get all Persons | None | Collection of Persons |
| PUT | /api/persons/{id} | Update an existing Person | Person | None |
| DELETE | /api/persons/{id} | Delete an Person | None | None |

# Definitions

- _Cluster_: - The 'World' in Blue Harvest. Collection of Stars, their planets and moons and other objects.
- _Fleet_: Collection of one or more ships used by the player or NPCs.
- _Jump Portal_: Method of travel in Blue Harvest.

# Stars
Links: <br>
https://en.wikipedia.org/wiki/Stellar_classification <br>
https://nineplanets.org/star/

| **Type** | Description | Mass | RL Chance | Notes | 
|:---|:---|:---|:---|:---|
|Class O|-|-|1:3,000,000|Not in BlueHarvest
|Class B|Blue|2.5 to 90.0|1:800 / 0.125%|
|Class A|Blue Giant|2.0 to 150.0|1:160 / 0.625%|
|Class F|White|0.8 to 1.4|1:33 / 3.00%|
|Class G|Yellow Dwarf|0.8 to 1.4|1:13 / 7.7%|
|Class K|Orange Dwarf|0.45 to 0.8|1:12 / 8.3%|
|Class M|-|-|3:4 / 76% |Not in BlueHarvest
                               
Mass is in relation to Solar mass

# Planets
Links: <br>
https://en.wikipedia.org/wiki/List_of_planet_types <br>

Zones:
A) Inner,
B) Inner-Habitable,
C) Habitable,
D) Outer-Habitable,
E) Outer,


| **Type** | Zone(s) | Radius (km) | Distance(1) (AU) | Description | 
|:---|:---:|:---:|:---:|:---|
|Desert     |B,C|2k-8k  |.3-.7|Very litter water, potentially mineral rich | 
|Gas Giant  |E    |35K-80k|4-15|-|
|Ice Giant  |E    |10k-40k|4-15|-|
|Ice        |C,D  |2k-5k  |.3-.7|-|
|Lava       |A,B  |1.5k-6k|.2-.6|-|
|Oceanic    |B,C,D|2k-8k  |.3-.7|-|
|Terrestrial|B,C,D|2k-8k  |.3-.7|-|

1) Distance to 'adjacent' planets, ie, prior and next in range.

# Resources