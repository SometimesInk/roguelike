using roguelike.roguelike.engine.rendering;
using roguelike.roguelike.utils.resources.translatables;

namespace roguelike.roguelike.engine.commands.manage;

public class CommandManageQuit : Command
{
  protected override CommandType GetCommandType()
  {
    return CommandType.Manage;
  }

  protected override string GetName()
  {
    return "quit";
  }

  public override Translatable Execute(EntryWindow win, string[] args)
  {
    // Close window
    Window.ShouldClose = true;
    Window.ShouldRedraw = false;
    Console.Clear();
    return GetTranslatable("output");
  }
}