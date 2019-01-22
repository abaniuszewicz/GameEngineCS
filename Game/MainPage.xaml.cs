using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Game.GameLogic;
using Microsoft.Graphics.Canvas;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Shapes;
using Windows.Web.Http.Headers;
using Engine.UI;
using Microsoft.Graphics.Canvas.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SnakeGame snakeGame;
        private Rect bounds;

        public MainPage()
        {
            InitializeComponent();
            InitializeSnakeGame();
        }

        private void InitializeSnakeGame()
        {
            snakeGame = new SnakeGame();
            CanvasControl.TargetElapsedTime = snakeGame.UpdateInterval;
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        }

        private void CanvasControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            snakeGame.Resized(e.NewSize, new Size(bounds.Width, bounds.Height));
        }

        private void CanvasControl_OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            snakeGame.Draw(sender, args);
        }

        private void CanvasControl_OnCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(snakeGame.CreateResources(sender, args).AsAsyncAction());
        }

        private void CanvasControl_OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            snakeGame.Update();
        }

        private void DPad_OnOnKeyPressed(VirtualKey key)
        {
            snakeGame.OnKeyPressed(key);
        }

        private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            snakeGame.OnKeyPressed(e.Key);
        }
    }
}
