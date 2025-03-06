using System;

namespace Roguelike.Engine {
  class Window {
    private bool shouldClose = false;

    public void setWindowShouldClose(bool value) {
      shouldClose = value;
    }

    public bool windowShouldClose() {
      return shouldClose;
    }
  }
}
