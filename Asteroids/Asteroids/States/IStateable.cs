using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.States
{
    public interface IStateable
    {
        void SetState(IGameState newState);
    }
}
