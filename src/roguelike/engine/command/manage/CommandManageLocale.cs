using roguelike.roguelike.util.resources;
using roguelike.roguelike.util.resources.translatable;

// ReSharper disable ConvertIfStatementToSwitchStatement

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

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // Check for arguments
    if (args.Length == 1) return (GetTranslatable("error.noArgs"), null);
    if (args.Length > 2) return (GetTranslatable("error.toManyArgs"), null);
    string file = args[1];
    if (!file.EndsWith(".lang")) return (GetTranslatable("error.notLangArg"), null);

    // Check if file exists
    if (!File.Exists(Resources.GetResourcePath(Path.Combine("lang", file))))
      return (GetTranslatable("error.NotExist"), [file]);

    // Check if there is a change to be made
    if (Translatable.GetActiveLocale() == file)
      return (GetTranslatable("error.noChange"), [file]);

    // Change locale
    string oldLocale = Translatable.GetActiveLocale();
    // Translatable.ActiveLocale = file; // Assign locale

    return (GetTranslatable("output"), [oldLocale, file]);
  }
}