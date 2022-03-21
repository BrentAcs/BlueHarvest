using BlueHarvest.Core.Rnd;
using Spectre.Console;

// public static class TableRenderer
// {
//    public static Table Render( StarCluster? cluster, int currentPage=1, int pageSize=20)
//    {
//       var table = new Table();
//
//       return table;
//    }
// }

namespace BlueHarvest.CLI.Rnd;

public static class StarClusterTableRenderer
{
   private const int IndexColWidth = 2;
   private const int NameColWidth = 8;
   private const int DescriptionColWidth = 24;
   private const int OwnerColWidth = 20;
   private const int CreatedOnColWidth = 10;
   private const int SizeColWidth = 12;
   private const int ObjectsColWidth = 4;
   private const int FluffColWidth = 17;

   public static Table Render(IEnumerable<StarCluster> clusters, int currentPage = 0, int pageSize = 20) =>
      Render(clusters.ToList(), currentPage, pageSize);

   public static Table Render(IList<StarCluster> clusters, int currentPage = 0, int pageSize = 20)
   {
      var table = new Table()
         .AddColumns(
            new TableColumn(Text.Empty).Width(IndexColWidth).Alignment(Justify.Right).PadLeft(0).PadRight(0),
            new TableColumn("Name").Width(NameColWidth).Alignment(Justify.Left),
            new TableColumn("Description").Width(DescriptionColWidth).Alignment(Justify.Left),
            new TableColumn("Owner").Width(OwnerColWidth).Alignment(Justify.Left),
            new TableColumn("Created").Width(CreatedOnColWidth).Alignment(Justify.Left),
            new TableColumn("Size").Width(SizeColWidth).Alignment(Justify.Left),
            new TableColumn("Objs").Width(ObjectsColWidth).Alignment(Justify.Right),
            new TableColumn("fluff").Width(FluffColWidth).Alignment(Justify.Right)
         );

      int startIndex = currentPage * pageSize;
      int endIndex = startIndex + pageSize;
      if (endIndex > clusters.Count)
         endIndex = clusters.Count;

      for (int index = startIndex; index < endIndex; ++index)
      {
         // TODO: refactor magic strings to constants.
         
         var item = clusters[ index ];
         table.AddRow(
            (index+1).ToMarkup(2),
            item.Name.ToMarkup(8, Color.Yellow),
            item.Description.ToMarkup(24),
            item.Owner.ToMarkup(20),
            item.CreatedOn?.ToShortDateString().ToMarkup(10, color: Color.Yellow),
            $"({item.Size?.XRadius:0.}, {item.Size?.YRadius:0.}, {item.Size?.ZRadius:0.})".ToMarkup(12),
            item.InterstellarObjects.Count.ToMarkup(4),
            Text.Empty
         );
      }

// var table = new Table()
//    .AddColumns(
//       new TableColumn(Text.Empty).Width(2).Alignment(Justify.Right).PadLeft(0).PadRight(0),
//       new TableColumn("Name").Width(8).Alignment(Justify.Left),
//       new TableColumn("Description").Width(24).Alignment(Justify.Left),
//       new TableColumn("Owner").Width(20).Alignment(Justify.Left),
//       new TableColumn("Created").Width(10).Alignment(Justify.Left),
//       new TableColumn("Size").Width(12).Alignment(Justify.Left),
//       new TableColumn("Objs").Width(4).Alignment(Justify.Right),
//       new TableColumn("fluff").Width(17).Alignment(Justify.Right)
//    );
//
// int index = 1;
// foreach (var item in col)
// {
//    table.AddRow(
//       index.ToMarkup(2),
//       item.Name.ToMarkup(8, Color.Yellow),
//       item.Description.ToMarkup(24),
//       item.Owner.ToMarkup(20),
//       item.CreatedOn?.ToShortDateString().ToMarkup(10, color: Color.Yellow),
//       $"({item.Size?.XRadius:0.}, {item.Size?.YRadius:0.}, {item.Size?.ZRadius:0.})".ToMarkup(12),
//       item.InterstellarObjects.Count.ToMarkup(4),
//       Text.Empty      
//    );
//
//    if (6 == ++index)
//       break;
// }

      return table;
   }
}
