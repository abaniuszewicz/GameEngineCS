using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Microsoft.Graphics.Canvas;

namespace Game.GameLogic
{
    public class Food : ITile
    {
        public Point Position { get; private set; }
        public CanvasBitmap CanvasBitmap { get; set; }

        public void SetPosition(Point position)
        {
            Position = position;
        }
    }
}
