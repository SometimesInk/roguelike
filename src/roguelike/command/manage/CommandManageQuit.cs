using Roguelike.Logic;

namespace Roguelike.Command.Manage {
  public class CommandManageQuit : Command {
    public override CommandType GetCommandType() {
      return CommandType.Manage;
    }

    public override String GetName() {
      return "q";
    }

    protected override String DefineUsage() {
      return "Exits program.";
    }

    public override CommandResult Execute(Context context) {
      return new CommandResult("Goodbye", true);
    }
  }
}
