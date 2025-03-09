using roguelike.roguelike.config;
using roguelike.roguelike.util.resources;
using roguelike.roguelike.util.resources.translatable;

// ReSharper disable ConvertIfStatementToSwitchStatement

namespace roguelike.roguelike.engine.command.manage;

public class CommandManageLang : Command
{

  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "lang";
  }

  public override (Translatable, object[]?) Execute(string[] args)
  {
    // Check for arguments
    if (args.Length == 1)
      return (GetTranslatable("output.noArgs"), [HandlerConfig.GetConfig<ConfigMain>().ActiveLocale]);
    if (args.Length > 2) return (GetTranslatable("error.toManyArgs"), null);
    string file = args[1];
    if (!file.EndsWith(".lang")) return (GetTranslatable("error.notLangArg"), null);

    // Check if file exists
    if (!File.Exists(Resources.GetResourcePath("lang", file)))
      return (GetTranslatable("error.NotExist"), [file]);

    // Check if there is a change to be made
    if (Translatable.GetActiveLocale() == file)
      return (GetTranslatable("error.noChange"), [file]);

    // Change locale
    HandlerConfig.GetConfig<ConfigMain>().SetActiveLocale(file).Write();

    return (GetTranslatable("output"), [Translatable.GetActiveLocale(), file]);
  }
}