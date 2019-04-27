using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    public class GameEventHandler {
        public void Invoke() {

        }
    }

    public class GameEvent
    {
        public GameEventHandler Handler { get; private set; }

        public GameEvent(GameEventHandler handler) {
            this.Handler = handler;
        }
    }
}
