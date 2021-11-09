﻿using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal abstract class BaseService : IHostedService
{
   protected record MenuAction
   {
      public string Name { get; set; } = string.Empty;
      public Action? Action { get; set; }
   }

   private IDictionary<ConsoleKey, MenuAction> _actions = new Dictionary<ConsoleKey, MenuAction>();
   private readonly IConfiguration _configuration;
   private readonly ILogger<BaseService> _logger;
   private readonly IHostApplicationLifetime _appLifetime;

   public BaseService(
      IConfiguration configuration,
      ILogger<BaseService> logger,
      IHostApplicationLifetime appLifetime)
   {
      _configuration = configuration;
      _logger = logger;
      _appLifetime = appLifetime;
      ;
   }

   protected abstract string Title { get; }

   protected IConfiguration Configuration => _configuration;

   protected ILogger<BaseService> Logger => _logger;

   protected IHostApplicationLifetime AppLifetime => _appLifetime;

   protected abstract void InitMenu();

   protected void ClearActions() => _actions.Clear();

   protected void AddMenuAction(ConsoleKey key, string name, Action action) =>
      _actions.Add(key, new MenuAction { Name = name, Action = action });

   protected void ProcessMenu()
   {
      var done = false;
      while (!done)
      {
         Clear();
         WriteLine(Title);
         foreach (var pair in _actions)
         {
            WriteLine($"{pair.Key} - {pair.Value.Name}");
         }
         WriteLine("Q - Quit");

         var keyInfo = ReadKey(true);
         if (_actions.ContainsKey(keyInfo.Key))
         {
            _actions[keyInfo.Key].Action();
         }

         done = keyInfo.Key == ConsoleKey.Q;
      }
   }

   public virtual Task StartAsync(CancellationToken cancellationToken)
   {
      InitMenu();
      return Task.CompletedTask;
   }
   public virtual Task StopAsync(CancellationToken cancellationToken) 
   {
      return Task.CompletedTask; 
   }
}