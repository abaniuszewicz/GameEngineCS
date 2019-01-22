using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Engine.Logic;

namespace Game.GameLogic
{
    public class SnakeSegment : ITile
    {
        public Color Color { get; } = Colors.DarkGreen;
        public Point Position { get; private set; }

        public SnakeSegment(Point position)
        {
            Position = position;
        }

        public void SetPosition(Enums.Direction direction)
        {
            switch (direction)
            {
                case Enums.Direction.Left:
                    Position = new Point(Position.X - 1, Position.Y);
                    break;
                case Enums.Direction.Up:
                    Position = new Point(Position.X, Position.Y - 1);
                    break;
                case Enums.Direction.Right:
                    Position = new Point(Position.X + 1, Position.Y);
                    break;
                case Enums.Direction.Down:
                    Position = new Point(Position.X, Position.Y + 1);
                    break;
            }
        }

        public void SetPosition(Point position)
        {
            Position = position;
        }
    }
}
