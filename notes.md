## Model Overview

- [Document](#Document)
  - [StarCluster](#StarCluster)
  - [InterstellarObject](#InterstellarObject)
    - [PlanetarySystem](#PlanetarySystem)
  - [StellarObject](#StellarObject)
    - [Planet](#Planet)
---
## Document
- ObjectId Id
## StarCluster
## InterstellarObject
- ObjectId ClusterId
- Point3D? Location
- string? Name
## PlanetarySystem
- StarType StarType
- Sphere Size
- List of StellarObject Objects
## StellarObject
## Planet
