using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Input;
using Engine.Logic;
using Engine.UI;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace Engine
{
    public abstract class GameEngine
    {
        protected ResourceManager ResourceManager { get; } = new ResourceManager();
        protected InputProcessor InputProcessor { get; } = new InputProcessor();
        protected StateManager StateManager { get; } = new StateManager();
        protected ScaleManager ScaleManager { get; } = new ScaleManager();

        protected abstract void Initialize();

        protected void Update()
        {
        }
        protected void Draw()
        {
        }
        protected void Invalidate()
        {
        }
    }
}
