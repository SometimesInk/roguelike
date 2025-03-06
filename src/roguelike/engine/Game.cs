using System;

namespace Roguelike.Engine {
  class Game {
    private Input input;
    private Window win;

    public Game() {
      // Init
      input = new Input();
      win = new Window();
    }

    public void Run() {
      while (!win.windowShouldClose()) {
        Loop();
      }
    }

    private void Loop() {
      Console.WriteLine("Context");
      input.Ask(win);
    }
  }
}
