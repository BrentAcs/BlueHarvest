using BlueHarvest.ConSoul.Common;
using BlueHarvest.Core.Rnd;

namespace BlueHarvest.ConSoul.BuilderRnD.Views;

public class StarClusterView : View<StarCluster>
{
   protected override ViewPrompt<StarCluster>? ViewPrompt => new StarClusterViewPrompt();

   protected override void ShowView()
   {
      var style1 = new Style(Color.Yellow, null, Decoration.Italic);
      var mu1 = new Markup("boobs", style1);
      
      AnsiConsole.Write(mu1);
      AnsiConsole.WriteLine();


      // var detailsTable = new Table()
      //    .Border(Theme.Active.MainTableBorder)
      //    .BorderColor(Theme.Active.MainTableBorderColor)
      //    .Width(Theme.Active.MainTableWidth)
      //    .Title("[yellow]Star Cluster[/]")
      //    .AddColumns( new TableColumn("Details"), new TableColumn(Text.Empty), new TableColumn(Text.Empty), new TableColumn(Text.Empty))
      //    
      //    // .AddColumn(new TableColumn("Item:").Width(null))
      //    // .AddColumn(new TableColumn($"[yellow]{Source?.Name}[/]").Width(null))
      //    // .AddColumn(new TableColumn("Owner:").Width(null))
      //    // .AddColumn(new TableColumn($"[yellow]{Source?.Owner}[/]").Width(null))
      //
      //    
      //    .AddRow("Created On:",
      //       $"[yellow]{Source?.CreatedOn?.ToShortDateString()}[/]",
      //       "Size:",
      //       $"[yellow]{Source?.Size.ToTableString()}[/]")
      //    .AddRow("Description:", $"[yellow]{Source?.Description}[/]");
      //    
      //    // .AddColumn(new TableColumn(Text.Empty).Centered())
      //    // .AddRow(BuildDetailsTable())
      //    //.AddRow(BuildSystemsTable())
      //    ;
      // AnsiConsole.Write(detailsTable);

      // var systemsTable = new Table()
      //    ;
      // AnsiConsole.Write(systemsTable);
   }
   
   private Table BuildSystemsTable()
   {
      var table = new Table()
         .Border(TableBorder.None)
         .Width(96)
         .Title("Planetary Systems")
         .AddColumns(
            new TableColumn(Text.Empty),
            new TableColumn("Name"),
            new TableColumn("Location"),
            new TableColumn("Star"),
            new TableColumn("Size"),
            new TableColumn("Planets"),
            new TableColumn("Fields")
         );
      
      
      //int index = 1;
      var systems = Source?.PlanetarySystems?.ToList();
      for (int i = 0; i < 10; i++)
      {
         table.AddRow($"{i + 1}");
      }
      
      return table;
   }

   private Table BuildDetailsTable()
   {
      var table = new Table()
         .Border(TableBorder.None)
         .Width(96)
         .AddColumn(new TableColumn("Item:").Width(null))
         .AddColumn(new TableColumn($"[yellow]{Source?.Name}[/]").Width(null))
         .AddColumn(new TableColumn("Owner:").Width(null))
         .AddColumn(new TableColumn($"[yellow]{Source?.Owner}[/]").Width(null))
         .AddRow("Created On:",
            $"[yellow]{Source?.CreatedOn?.ToShortDateString()}[/]",
            "Size:",
            $"[yellow]{Source?.Size.ToTableString()}[/]")
         .AddRow("Description:", $"[yellow]{Source?.Description}[/]");
      return table;
   }

   private class StarClusterViewPrompt : ViewPrompt<StarCluster>
   {
      protected override void BuildChoices(SelectionPrompt<ViewPromptItem> prompt, StarCluster? source)
      {
         prompt.AddChoice(new ViewPromptItem("Boobs"));
         prompt.AddChoiceGroup(new ViewPromptItem("Systems"), new[] {new ViewPromptItem("Abc"), new ViewPromptItem("Efg")});
      }
   }
}
