using Engine;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.System;
using Engine.Logic;
using Engine.UI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Game.GameLogic
{
    class SnakeGame : GameEngine
    {
        public TimeSpan UpdateInterval { get; } = new TimeSpan(0, 0, 0, 0, 175);
        public Size BoardSize { get; } = new Size(16, 16);
        public CanvasBitmap StartScreen { get; private set; }
        public CanvasBitmap FinishScreen { get; private set; }
        public CanvasBitmap HeadLeft { get; private set; }
        public CanvasBitmap HeadUp { get; private set; }
        public CanvasBitmap HeadRight { get; private set; }
        public CanvasBitmap HeadDown { get; private set; }
        public CanvasBitmap HeadEatLeft { get; private set; }
        public CanvasBitmap HeadEatUp { get; private set; }
        public CanvasBitmap HeadEatRight { get; private set; }
        public CanvasBitmap HeadEatDown { get; private set; }
        public Board Board { get; private set; }
        public Snake Snake { get; private set; } = new Snake();
        public Food Food { get; } = new Food();

        public SnakeGame()
        {
            Initialize();
        }

        public void Resized(Size newSize, Size initialSize)
        {
            ScaleManager.CurrentSize = newSize;
        }

        protected sealed override void Initialize()
        {
            Board = new Board(BoardSize, Snake, Food);
            Board.OnCollision += OnBoardOnOnCollision;
            StateManager.OnGameStateChanged += s =>
            {
                if (s == StateManager.GameState.Starting)
                    ScaleManager.DesignSize = StartScreen.Size;
                if (s == StateManager.GameState.Finished)
                    ScaleManager.DesignSize = FinishScreen.Size;
            };
        }

        private void OnBoardOnOnCollision(Enums.CollisionType c)
        {
            switch (c)
            {
                case Enums.CollisionType.SnakeFood:
                    Snake.AddSegment();
                    Board.InsertFood();
                    break;
                case Enums.CollisionType.SnakeBorder:
                case Enums.CollisionType.SnakeSnake:
                    StateManager.GoToNextState();
                    break;
            }
        }

        public void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            base.Draw();
            switch (StateManager.CurrentState)
            {
                case StateManager.GameState.Starting:
                    args.DrawingSession.DrawImage(ScaleManager.Scale(StartScreen));
                    return;

                case StateManager.GameState.Playing:
                    foreach (var tile in Board.Tiles)
                    {
                        if (tile == null)
                            continue;

                        if (tile.GetType() == typeof(Food))
                            args.DrawingSession.DrawImage(Food.CanvasBitmap, Board.GetRectangle(tile, ScaleManager.Scale(BoardSize)));
                        else if (tile.GetType() == typeof(SnakeSegment))
                        {
                            var rect = Board.GetRectangle(tile, ScaleManager.Scale(BoardSize));
                            var color = ((SnakeSegment)tile).Color;
                            args.DrawingSession.FillRectangle(rect, color);
                            var distanceFromFood = Math.Sqrt(Math.Pow(Food.Position.X - Snake.Segments[0].Position.X, 2) + Math.Pow(Food.Position.Y - Snake.Segments[0].Position.Y, 2));

                            switch (Snake.Direction)
                            {
                                case Enums.Direction.Left:
                                    args.DrawingSession.DrawImage(distanceFromFood < 3 ? HeadEatLeft : HeadLeft, Board.GetRectangle(Snake.Segments[0], ScaleManager.Scale(BoardSize)));
                                    break;
                                case Enums.Direction.Up:
                                    args.DrawingSession.DrawImage(distanceFromFood < 3 ? HeadEatUp : HeadUp, Board.GetRectangle(Snake.Segments[0], ScaleManager.Scale(BoardSize)));
                                    break;
                                case Enums.Direction.Right:
                                    args.DrawingSession.DrawImage(distanceFromFood < 3 ? HeadEatRight : HeadRight, Board.GetRectangle(Snake.Segments[0], ScaleManager.Scale(BoardSize)));
                                    break;
                                case Enums.Direction.Down:
                                    args.DrawingSession.DrawImage(distanceFromFood < 3 ? HeadEatDown : HeadDown, Board.GetRectangle(Snake.Segments[0], ScaleManager.Scale(BoardSize)));
                                    break;
                            }
                        }
                    }
                    return;

                case StateManager.GameState.Finished:
                    args.DrawingSession.DrawImage(ScaleManager.Scale(FinishScreen));
                    return;
            }
        }

        public async Task CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            StartScreen = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/startscreen.png"));
            FinishScreen = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/finishscreen.jpg"));
            Food.CanvasBitmap = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/apple.png"));
            HeadLeft = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_left.png"));
            HeadUp = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_up.png"));
            HeadRight = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_right.png"));
            HeadDown = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_down.png"));
            HeadEatLeft = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_eat_left.png"));
            HeadEatUp = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_eat_up.png"));
            HeadEatRight = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_eat_right.png"));
            HeadEatDown = await ResourceManager.LoadBitmap(sender, new Uri("ms-appx:///Assets/Images/head_eat_down.png"));
            StateManager.GoToNextState();
        }

        public new void Update()
        {
            base.Update();
            if (StateManager.CurrentState == StateManager.GameState.Playing)
                Board.Update();
        }

        public void OnKeyPressed(VirtualKey key)
        {
            Snake.ChangeDirection(InputProcessor.GetArrowDown(key));

            if (StateManager.CurrentState == StateManager.GameState.Starting)
                StateManager.GoToNextState();
            if (StateManager.CurrentState == StateManager.GameState.Finished)
            {
                Snake = new Snake();
                Board = new Board(BoardSize, Snake, Food);
                Board.OnCollision += OnBoardOnOnCollision;
                StateManager.GoToNextState();
            }
        }
    }
}
