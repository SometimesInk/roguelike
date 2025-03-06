namespace Roguelike.Command {
  public class CommandResult {
    public bool closeWindow { get; set; }
    public String output { get; set; }

    public CommandResult(String output, bool closeWindow) {
      this.output = output;
      this.closeWindow = closeWindow;
    }
  }
}
