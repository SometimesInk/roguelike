// In this class, CHECKS does the following:
//  - Checks if the name and usage translatables of registered commands exist.

// #define CHECKS

using roguelike.roguelike.engine.command.manage;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command;

public static class HandlerCommand
{
  public static readonly List<Command> RegisteredCommands = [];

  public static void Init()
  {
    // Register commands
    RegisteredCommands.Add(new CommandManageLangList());
    RegisteredCommands.Add(new CommandManageLang());
    RegisteredCommands.Add(new CommandManageList());
    RegisteredCommands.Add(new CommandManageNetInit());
    RegisteredCommands.Add(new CommandManageNetJoin());
    RegisteredCommands.Add(new CommandManageNetListClients());
    RegisteredCommands.Add(new CommandManageQuit());

#if CHECKS
      foreach (Command command in registeredCommands)
      {
        Translatable name = command.GetTranslatable();
        if (name.ToString() == name.Key)
          Translatable.Print(new Translatable("error.translatable.missing").Format(name.Key),
            stream: TranslatablePrintStream.Err);

        Translatable usage = command.GetTranslatable("usage");
        if (usage.ToString() == usage.Key)
          Translatable.Print(new Translatable("error.translatable.missing").Format(usage.Key),
            stream: TranslatablePrintStream.Err);
      }
#endif
  }

  public static (Translatable, object[]?) ParseCommand(string[] args)
  {
    // Find registered command
    Command? command = FindCommand(args[0]);
    if (command is null) return (Translatable.Empty, null);

    // Execute command
    var output = command.Execute(args);

    return output;
  }

  /// <summary>
  /// Find a registered command with a specified name
  /// </summary>
  /// <param name="name">The name of the command to look for including its
  /// type</param>
  /// <returns>The command of null if it was not found</returns>
  private static Command? FindCommand(string name)
  {
    foreach (Command registeredCommand in RegisteredCommands)
    {
      if (registeredCommand.ToString() == name) return registeredCommand;
    }

    return null;
  }
}