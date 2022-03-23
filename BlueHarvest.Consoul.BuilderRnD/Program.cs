using BlueHarvest.Consoul.BuilderRnD.Menus;

Console.Title = "Blue Harvest Consoul Builder RnD";

#if false
FakeFactory.Shallow = true;
var cluster = FakeFactory.CreateStarCluster();
var col = new List<StarCluster>();
50.TimesDo(() => col.Add(FakeFactory.CreateStarCluster()));

var enumerable = col as IEnumerable<StarCluster>;
//var table = col.Build( 0, 10);
var table = enumerable.Build();

AnsiConsole.Write(table);
#endif

AppMenu.Main.Show();

Console.WriteLine("Done.");
Console.ReadKey(true);
