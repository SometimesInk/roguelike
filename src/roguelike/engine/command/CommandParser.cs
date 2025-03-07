// In this class, CHECKS does the following:
//  - Checks if the name and usage translatables of registered commands exist.

// #define CHECKS

using roguelike.roguelike.engine.command.manage;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command
  // ReSharper disable once ArrangeNamespaceBody
{
  public class CommandParser
  {
    private readonly List<Command> registeredCommands = [];

    public CommandParser()
    {
      // Register commands
      registeredCommands.Add(new CommandManageQuit());
      registeredCommands.Add(new CommandManageLocale());

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

    public CommandResult? ParseCommand(Window win, string[] args)
    {
      // Find registered command
      Command? command = FindCommand(args[0]);
      if (command is null) return null;

      // Execute command
      CommandResult output = command.Execute(args);

      // Process command output
      if (output.CloseWindow) win.SetWindowShouldClose(true);

      return output;
    }

    /// <summary>
    /// Find a registered command with a specified name
    /// </summary>
    /// <param name="name">The name of the command to look for including its
    /// type</param>
    /// <returns>The command of null if it was not found</returns>
    private Command? FindCommand(string name)
    {
      foreach (Command registeredCommand in registeredCommands)
      {
        if (registeredCommand.ToString() == name) return registeredCommand;
      }

      return null;
    }
  }
}