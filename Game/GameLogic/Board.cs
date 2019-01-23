using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Shapes;
using Engine.Logic;

namespace Game.GameLogic
{
    public class Board
    {
        private readonly Random rnd = new Random();

        public delegate void CollisionDelegate(Enums.CollisionType collisionType);
        public event CollisionDelegate OnCollision;

        public ITile[,] Tiles { get; }
        public Snake Snake { get; }
        public Food Food { get; }

        public Board(Size boardSize, Snake snake, Food food)
        {
            Tiles = new ITile[(int)boardSize.Width, (int)boardSize.Height];
            Snake = snake;
            Food = food;
            InsertFood();
        }

        public void Update()
        {
            Snake.Update();
            if (Snake.Segments.Any(ss => ss.Position.X < 0 || ss.Position.X >= Tiles.GetLength(0) || 
                                         ss.Position.Y < 0 || ss.Position.Y >= Tiles.GetLength(1)))
                KeepSnakeWithinBoard();
            CheckCollision();
            UpdateSegmentTiles();
        }

        private void KeepSnakeWithinBoard()
        {
            foreach (var segment in Snake.Segments)
            {
                if (segment.Position.X < 0)
                    segment.SetPosition(new Point(Tiles.GetLength(0) - 1, segment.Position.Y));
                if (segment.Position.X >= Tiles.GetLength(0))
                    segment.SetPosition(new Point(0, segment.Position.Y));
                if (segment.Position.Y < 0)
                    segment.SetPosition(new Point(segment.Position.X, Tiles.GetLength(1) - 1));
                if (segment.Position.Y >= Tiles.GetLength(1))
                    segment.SetPosition(new Point(segment.Position.X, 0));
            }
        }

        public Rect GetRectangle(ITile tile, Size tileSize)
        {
            return new Rect(new Point(tile.Position.X * tileSize.Width, tile.Position.Y * tileSize.Height), tileSize);
        }

        public Enums.CollisionType CheckCollision()
        {
            foreach (var tile in Tiles)
            {
                if (tile == null || !Snake.IsCollided(tile))
                    continue;
                
                if (tile.GetType() == typeof(SnakeSegment))
                    OnCollision?.Invoke(Enums.CollisionType.SnakeSnake);
                if (tile.GetType() == typeof(Food))
                    OnCollision?.Invoke(Enums.CollisionType.SnakeFood);
            }
            return Enums.CollisionType.None;
        } 

        public void InsertFood()
        {
            Tiles[(int)Food.Position.X, (int)Food.Position.Y] = null;

            int x, y;
            do
            {
                x = rnd.Next(0, Tiles.GetLength(0) - 1);
                y = rnd.Next(0, Tiles.GetLength(1) - 1);
            } while (Tiles[x, y] != null);

            Food.SetPosition(new Point(x, y));
            Tiles[(int)Food.Position.X, (int)Food.Position.Y] = Food;
        }

        private void UpdateSegmentTiles()
        {
            for (var i = 0; i < Tiles.GetLength(0); i++)
                for (var j = 0; j < Tiles.GetLength(1); j++)
                    if (Tiles[i, j]?.GetType() == typeof(SnakeSegment))
                        Tiles[i, j] = null;

            foreach (var segment in Snake.Segments)
                Tiles[(int)segment.Position.X, (int)segment.Position.Y] = segment;
        }
    }
}
