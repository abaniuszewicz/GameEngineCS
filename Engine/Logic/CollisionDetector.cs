using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace Engine.Logic
{
    public class CollisionDetector
    {
        public bool IsCollided(Rect rect1, Rect rect2)
        {
            return rect1.Left < rect2.Right
                   && rect1.Right > rect2.Left
                   && rect1.Top < rect2.Bottom
                   && rect1.Bottom > rect2.Top;
        }
    }
}
