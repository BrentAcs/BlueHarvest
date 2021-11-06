using System;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Utilities;

var rng = new SimpleRng();
var attacker = new GroundUnit { BaseStrength = 100 };
//var defender = new GroundUnit { Strength = 100 };

var json = attacker.ToJsonIndented();

//int attackRoll = rng.Next(100);
//int defendRoll = rng.Next(100);

//if (attacker.Strength + attackRoll > defender.Strength + defendRoll)
//{
//   Console.WriteLine($"Attacker victory");
//}
//else if (attacker.Strength + attackRoll == defender.Strength + defendRoll)
//{
//   Console.WriteLine($"Draw");
//}
//else
//{
//   Console.WriteLine($"Defender victory");
//}


Console.WriteLine("Done.");

