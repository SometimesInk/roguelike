using roguelike.roguelike.util.resources;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

public class CommandManageLocale : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "locale";
  }

  public override CommandResult Execute(string[] args)
  {
    // Check for arguments
    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (args.Length == 1) return new CommandResult(GetTranslatable("error.noArgs"));
    if (args.Length > 2) return new CommandResult(GetTranslatable("error.toManyArgs"));
    string file = args[1];
    if (!file.EndsWith(".lang")) return new CommandResult(GetTranslatable("error.notLangArg"));

    // Check if file exists
    if (!File.Exists(Resources.GetResourcePath($"lang/{file}")))
      return CommandResult.Format(GetTranslatable("error.notExist"), formattingElements: file);

    // Check if there is a change to be made
    if (Translatable.ActiveLocale == file)
      return CommandResult.Format(GetTranslatable("error.noChange"), formattingElements: file);

    // Change locale
    // TODO: Set locale in [future] config system to be applied on next launch
    string oldLocale = Translatable.ActiveLocale;
    Translatable.ActiveLocale = file;

    return CommandResult.Format(GetTranslatable("output"), false, oldLocale,
      file);
  }
}