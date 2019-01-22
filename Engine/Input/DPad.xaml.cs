using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Engine.Input
{
    public sealed partial class DPad : UserControl
    {
        public delegate void KeyPressed(VirtualKey key);

        public event KeyPressed OnKeyPressed;

        public DPad()
        {
            InitializeComponent();
        }

        private void LeftButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnKeyPressed?.Invoke(VirtualKey.Left);
        }

        private void UpButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnKeyPressed?.Invoke(VirtualKey.Up);
        }

        private void RightButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnKeyPressed?.Invoke(VirtualKey.Right);
        }

        private void DownButton_OnClick(object sender, RoutedEventArgs e)
        {
            OnKeyPressed?.Invoke(VirtualKey.Down);
        }
    }
}
