﻿using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.PoC.CLI.Menus;

namespace BlueHarvest.PoC.CLI.Actions;

public class CreateStarClusterAction : ClusterPlaygroundAction
{
   private readonly IStarClusterFactory _starClusterFactory;

   public CreateStarClusterAction(IStarClusterFactory starClusterFactory)
   {
      _starClusterFactory = starClusterFactory;
   }

   private class CreatePromptItem : PromptItem<StarClusterFactoryOptions>
   {
      public CreatePromptItem(string display, StarClusterFactoryOptions? data = default) : base(display, data)
      {
      }
   }

   public override async Task Execute()
   {
      ShowTitle("Test Star Cluster Factory");

      var prompt = new SelectionPrompt<CreatePromptItem>()
         .PageSize(20)
         .AddChoices(new[]
         {
            new CreatePromptItem("Test", StarClusterFactoryOptions.Test), 
            new CreatePromptItem("Small", StarClusterFactoryOptions.Small),
            new CreatePromptItem("Medium", StarClusterFactoryOptions.Medium),
            new CreatePromptItem("Large", StarClusterFactoryOptions.Large),
            new CreatePromptItem("Extra Large", StarClusterFactoryOptions.ExtraLarge),
            new CreatePromptItem("[gray]None[/]"),
         });
      var item = AnsiConsole.Prompt(prompt);

      if (item.Data is not null)
      {
         var canCreate = await _starClusterFactory.CanCreate(item.Data).ConfigureAwait(true);
         if (canCreate)
         {
            var cluster = await _starClusterFactory.Create(item.Data).ConfigureAwait(false);
            AnsiConsole.MarkupLine($"Cluster created with id: [white]'{cluster.Id}'[/]");
         }
         else
         {
            AnsiConsole.MarkupLine($"Cluster already exists with name [yellow]{item.Data.Name}[/].");
         }
      }
      else
      {
         AnsiConsole.WriteLine($"No cluster created.");
      }

      ShowReturn();
   }
}
