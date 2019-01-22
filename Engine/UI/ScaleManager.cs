using Microsoft.Graphics.Canvas.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.Graphics.Canvas;

namespace Engine.UI
{
    public class ScaleManager
    {
        public Size DesignSize { get; set; }
        public Size CurrentSize { get; set; }
        private Size ScaleSize => new Size(CurrentSize.Width/DesignSize.Width, CurrentSize.Height/DesignSize.Height);

        public ScaleManager()
        {
        }

        public Transform2DEffect Scale(CanvasBitmap canvasBitmap)
        {
            return new Transform2DEffect
            {
                Source = canvasBitmap,
                TransformMatrix = Matrix3x2.CreateScale((float) ScaleSize.Width, (float) ScaleSize.Height)
            };
        }

        public Transform2DEffect Scale(CanvasBitmap canvasBitmap, float percentWidth, float percentHeight)
        {
            return new Transform2DEffect
            {
                Source = canvasBitmap,
                TransformMatrix = Matrix3x2.CreateScale((float)ScaleSize.Width * percentWidth, (float)ScaleSize.Height * percentHeight)
            };
        }
        
        public Size Scale(Size size)
        {
            return new Size(CurrentSize.Width/size.Width, CurrentSize.Height/size.Height);
        }
    }
}
