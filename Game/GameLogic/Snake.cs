using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Engine.Logic;
using static Windows.System.VirtualKey;

namespace Game.GameLogic
{
    public class Snake
    {
        public List<SnakeSegment> Segments { get; } = new List<SnakeSegment>();
        public Enums.Direction Direction { get; private set; } = Enums.Direction.Right;
        public CollisionDetector CollisionDetector { get; } = new CollisionDetector();

        public Snake()
        {
            Segments.Add(new SnakeSegment(new Point(1, 1)));
        }
        public void Update()
        {
            for (var i = Segments.Count - 1; i > 0; i--)
                Segments[i].SetPosition(Segments[i - 1].Position);

            Segments[0].SetPosition(Direction);
        }

        public void ChangeDirection(VirtualKey key)
        {
            switch (key)
            {
                case Left when Direction != Enums.Direction.Right:
                    Direction = Enums.Direction.Left;
                    break;
                case Up when Direction != Enums.Direction.Down:
                    Direction = Enums.Direction.Up;
                    break;
                case Right when Direction != Enums.Direction.Left:
                    Direction = Enums.Direction.Right;
                    break;
                case Down when Direction != Enums.Direction.Up:
                    Direction = Enums.Direction.Down;
                    break;
            }
        }

        public bool IsCollided(ITile tile)
        {
            var head = Segments[0];
            if (tile == head)
                return false;

            var headRect = new Rect(head.Position, new Size(1, 1));
            var tileRect = new Rect(tile.Position, new Size(1,1));
            return CollisionDetector.IsCollided(headRect, tileRect);
        }

        public void AddSegment()
        {
            Segments.Add(new SnakeSegment(Segments.Last().Position));
        }
    }
}
