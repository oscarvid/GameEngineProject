using System;
using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GameJailBreak
{
    class Background : GameObject
    {
        public override void initialize()
        {
            this.Transform.X = 0.0f;
            this.Transform.Y = 0.0f;
            //ToDo add background picture.
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("background.jpg");
        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }
    }
}
