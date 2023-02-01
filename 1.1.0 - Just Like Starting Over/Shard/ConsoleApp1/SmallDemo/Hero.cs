using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SmallDemo
{
    class Hero: GameObject, InputListener
    {
        bool left, right;

        public override void initialize()
        {
            this.Transform.X = 500.0f;
            this.Transform.Y = 500.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("right1.png");

            Bootstrap.getInput().addListener(this);

        }

        public void handleInput(InputEvent inp, string eventType)
        {

        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }


    }
}