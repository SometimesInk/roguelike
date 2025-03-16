using System.Xml;
using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

public class CommandManageLangList : Command
{
  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "lang.list";
  }

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    if (args.Length != 1) return GetTranslatable("error.toManyArgs");

    // Read out files in the directory
    int count = 0;
    foreach (string file in Directory.GetFiles(Resources.GetResourcePath("lang")))
    {
      win.WriteMessage(GetTranslatable("output.found").Format(Path.GetFileName(file)));
      count++;
    }

    return GetTranslatable("output").Format(count);
  }
}