# Introduction

[Markdown Ref](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet)

This document is to serve as design notes and documentation for Blue Harvest.

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