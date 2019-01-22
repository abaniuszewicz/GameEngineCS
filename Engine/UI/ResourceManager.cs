using System;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Engine.UI
{
    public class ResourceManager
    {
        public async Task<CanvasBitmap> LoadBitmap(CanvasAnimatedControl sender, Uri uri)
        {
           return await CanvasBitmap.LoadAsync(sender, uri);
        }
    }
}
