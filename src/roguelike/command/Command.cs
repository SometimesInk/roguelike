namespace Roguelike.Command {
  public abstract class Command {
    public abstract CommandType GetCommandType();

    public abstract String GetName();

    protected abstract String DefineUsage();
    
    public String GetUsage() {
      return GetName() + " -> " + DefineUsage();
    }

    public abstract CommandResult Execute(Context context);

    public override String ToString() {
      CommandType type = GetCommandType();
      return (type != 0 ? (Char)type : "") + GetName();
    }
  }
}
