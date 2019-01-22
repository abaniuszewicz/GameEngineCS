using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLogic
{
    public static class Enums
    {
        public enum Direction
        {
            Left,
            Up,
            Right,
            Down
        };

        public enum CollisionType
        {
            None,
            SnakeFood,
            SnakeSnake,
            SnakeBorder
        };
    }
}
