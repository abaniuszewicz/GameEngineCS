using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Game.GameLogic
{
    public interface ITile
    {
        Point Position { get; }

        void SetPosition(Point position);
    }
}
