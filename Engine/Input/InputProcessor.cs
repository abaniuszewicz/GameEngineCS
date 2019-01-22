using Windows.System;
using Windows.UI.Xaml.Input;

namespace Engine.Input
{
    public class InputProcessor
    {
        public VirtualKey GetKey(KeyRoutedEventArgs e)
        {
            return e.Key;
        }

        public VirtualKey GetArrowDown(VirtualKey key)
        {
            if (key == VirtualKey.Left || key == VirtualKey.Up || key == VirtualKey.Right || key == VirtualKey.Down)
                return key;

            return VirtualKey.None;
        }
    }
}
