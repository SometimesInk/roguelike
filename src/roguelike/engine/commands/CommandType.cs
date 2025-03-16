namespace roguelike.roguelike.engine.commands;

public enum CommandType : ushort // 16-bit unsigned integer, thus a char.
{
  /// This is the "replacement character" � used when a
  ///  character is missing, unknown or invalid. It is used
  ///  here to remind that Common commands do not have a
  ///  prefix.
  Common = '\uFFFD',

  Manage = '@',

  Macro = ':'
}