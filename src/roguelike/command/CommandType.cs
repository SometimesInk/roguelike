namespace Roguelike.Command {
  public enum CommandType : ushort {
    /// These commands are simple commands related to the current context.
    Common = 0,
    /// These are commands that are related to the program's settings and non-gameplay related elements.
    Manage = ':',
    /// These commands are simplified versions of others.
    Macro = '@'
  }
}
