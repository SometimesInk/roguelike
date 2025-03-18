// In this class, CHECKS does the following:
//  - Checks if the name and usage translatables of registered commands exist.

#define CHECKS

using roguelike.roguelike.engine.commands.manage;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands;

public static class CommandHandler
{
  public static readonly List<Command> RegisteredCommands = [];

  /// <summary>
  /// Initializes commands.
  /// </summary>
  /// <param name="win">Window for logging.</param>
  /// <remarks>Requires translatables, thus LocaleHandler to be previously
  ///  initialized.</remarks>
  public static void Init(EntryWindow win)
  {
    // Register commands
    RegisteredCommands.Add(new CommandManageLangList());
    RegisteredCommands.Add(new CommandManageLang());
    RegisteredCommands.Add(new CommandManageList());
    RegisteredCommands.Add(new CommandManageNetInit());
    RegisteredCommands.Add(new CommandManageNetJoin());
    RegisteredCommands.Add(new CommandManageNetListClients());
    RegisteredCommands.Add(new CommandManageNetSendPacketH());
    RegisteredCommands.Add(new CommandManageQuit());

#if CHECKS
    // Check if every command defines a name and usage
    foreach (Command command in RegisteredCommands)
    {
      Translatable name = command.GetTranslatable();
      Translatable usage = command.GetTranslatable("usage");

      if (name.ToString() == name.Key || usage.ToString() == usage.Key)
      {
        win.WriteMessage(new Translatable("error.translatable.missing").Format(name.Key),
          TranslatablePrintStream.Err);
      }
    }
#endif
  }

  public static void ParseBuffer(EntryWindow win, string input)
  {
    // Check if buffer is whitespace
    if (string.IsNullOrWhiteSpace(input))
    {
      Window.ShouldRedraw = false;
      return;
    }

    // Get command arguments where arg[0] is the command alias used
    string[] args = input.Split(' ');

    // Send invalid translatable if the command wasn't recognized and parsed.
    Translatable output = ParseCommand(win, args);

    // Format command output
    win.WriteMessage(output == Translatable.Empty
      ? new Translatable("command.parsing.invalid").Format(args[0])
      : output);
  }

  private static Translatable ParseCommand(EntryWindow win, string[] args)
  {
    // Find registered command
    Command? command = FindCommand(args[0]);

    if (command is null) return Translatable.Empty;

    // Execute command
    Translatable output = command.Execute(win, args);
    return output;
  }

  /// <summary>
  /// Find a registered command with a specified name.
  /// </summary>
  /// <param name="name">The name of the command to look for including its
  ///  type marker.</param>
  /// <returns>The command of null if it was not found.</returns>
  private static Command? FindCommand(string name)
  {
    // Find the first command whose name matches the given one
    return RegisteredCommands.FirstOrDefault(registeredCommand => registeredCommand.ToString() == name);
  }
}