using System;

namespace Roguelike.Engine {
  class Input {
    public Input() {}

    public void Init() {
      // Register commands
    }

    public void Ask(Window win) {
      Console.Write("\n > ");
      String? line = Console.ReadLine();

      if (line == null) return;
      if (line == ":q") win.setWindowShouldClose(true);
    }
  }
}
