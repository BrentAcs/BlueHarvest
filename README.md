# BlueHarvest

If you're a Star Wars fan and nerd, you'll know that Blue Harvest was Lucasfilm's codename for Return of the Jedi. Let's face it, it's a great code name. I have therefore adopted it.

So, what is my Blue Harvest? The one found in this GitHub Repository?

It's my personal, ongoing, growing project. It will sound silly to most/all, however this is my playground for things I want to learn, explore, enhance my knowledge of, etc... There is a grand vision of a game or just a world that is mash-up of several games I played throughout the years. Fromt the original Bard's Tale and Ultima series, to EVE Online and other RPG/MMOs. I have no idea if this project will ever be done. It may always be an evolving playground of different tech.

The first steps are to create small 'world' randomly. Maybe allow for several. These world's are a cluster of stars, populated by planetary systems, their moons, astroid betls or fields and other cosmic entities. The vision is to create and store these worlds in MongoDB and allow for a RESTful API to access them. The, perhaps a Blazor Web UI.

After that, well, we'll see.

Goals are:
- Have fun.
- Learn things.
- Iterate and learn things.
- Code should be of professional quality, unless noted. It may not be be finished (yet), but it must be quality.
- SOLID, DRY and other principals are a must.
- Avoid anti-patterns.

Please feel free to drop any and all comments, feedback, ideas, critques at brent.mann@gmail.com.

Enjoy and/or Laugh.

Or would it be Laugh and/or Enjoy
 
##Cosmic Objects
**Star Cluster**<br/>
+-- **PlanetarySystem** : InterstellarObjects<br/>
+-- +-- **SatelliteSystems** :StellarObject<br/>
+-- +-- +-- SatelliteSystem<br/>
+-- +-- +-- +-- NaturalSatellite : Satellite<br/>
+-- +-- +-- +-- ArtificialSatellite: Satellite<br/>
+-- +-- **AsteroidFields** :StellarObject<br/>
+-- **DeepSpaceObjects** : InterstellarObjects<br/>

##Planetary Zones
| Zone | Distance Range (AU) | # (Range) | (Min Distance) |
| --- |---------------------|-----------|----------------|
| Inner | .1  - .25           | 0 - 2     | .1             |
| InnerHabitable | .25  - 1.5          | 0 - 2     | .5             |
| Habitable | 1.5  - 3.5         | 0 - 2     | .5             |     
| OuterHabitable | 3.5 - 5.0           | 0 - 2     | .5             |
| Outer | 5.0  - ~            | 2 - 10    | 1              |         

      switch (distance)
      {
         case <= 0.2499:
            return PlanetaryZone.Inner;
         case <= 1.499:
            return PlanetaryZone.InnerHabitable;
         case <= 3.499:
            return PlanetaryZone.Habitable;
         case <= 4.999:
            return PlanetaryZone.OuterHabitable;
         default:
            return PlanetaryZone.Outer;
      }
