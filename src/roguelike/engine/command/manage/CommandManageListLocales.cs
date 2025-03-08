using roguelike.roguelike.util.resources;
using roguelike.roguelike.util.resources.translatable;

namespace roguelike.roguelike.engine.command.manage;

public class CommandManageListLocales : Command
{
  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "listLocales";
  }

  public override (Translatable, object[]?) Execute(string[] args)
  {
    if (args.Length != 1) return (GetTranslatable("error.toManyArgs"), null);

    // Read out files in the directory
    int count = 0;
    foreach (string file in Directory.GetFiles(Resources.GetResourcePath("lang")))
    {
      Translatable.Printf(GetTranslatable("output.found"), form: Path.GetFileName(file));
      count++;
    }

    return (GetTranslatable("output"), [count]);
  }
}