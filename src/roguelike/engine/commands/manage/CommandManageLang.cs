using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources;
using roguelike.roguelike.utils.resources.configuring;
using roguelike.roguelike.utils.resources.configuring.configs;
using roguelike.roguelike.utils.resources.locale;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

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

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // Check for arguments
    if (args.Length == 1) return GetTranslatable("output.noArgs").Format(LocaleHandler.ActiveLocale);
    if (args.Length > 2) return GetTranslatable("error.toManyArgs");

    // Check if file is valid and exists
    string file = args[1];
    if (!file.EndsWith(".lang")) return GetTranslatable("error.notLangArg");
    if (!File.Exists(Resources.GetResourcePath("lang", file)))
      return GetTranslatable("error.notExist").Format(file);

    // Check if there is a change to be made
    if (LocaleHandler.ActiveLocale == file)
      return GetTranslatable("error.noChange").Format(file);

    // Change locale
    ConfigHandler.GetConfig<ConfigMain>(win).SetActiveLocale(file).Write();
    return GetTranslatable("output").Format(LocaleHandler.ActiveLocale, file);
  }
}