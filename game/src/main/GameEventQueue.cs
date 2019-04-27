using System;
using System.Collections.Generic;
using System.Linq;

using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace game
{
    // Based on http://community.monogame.net/t/best-way-for-queueing-game-events/10362/6

    public class GameEventQueue
    {
        private Queue<GameEvent> _queue;
        private Option<GameEvent> _current;
        public bool Processing { get => _current != None; }

        public GameEventQueue() {
            _queue = new Queue<GameEvent>();
        }

        public void Add(GameEventHandler game_event_handler) {
            _queue.Enqueue(new GameEvent(game_event_handler));
        }

        public void ProcessNext() {
            _current = None;

            if (_queue.Count() > 0) {
                _current = Some(_queue.Dequeue());
            }

            _current.Match(
                None: () => {},
                Some: (e) => { e.Handler.Invoke(); }
            );
        }

        public void Clear() {
            _queue.Clear();
            _current = None;
        }

        public int Count() => _queue.Count() + (_current == None ? 0 : 1);
    }
}
