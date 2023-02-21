using System;
using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SmallDemo
{
    class Background: GameObject, InputListener
    {
        public override void initialize()
        {
            this.Transform.X = 0.0f;
            this.Transform.Y = 0.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("background.jpg");
        
            Bootstrap.getInput().addListener(this);
        }

        public override void update()
        {
            // Tuple<float, float> relativeTrans =
            //     Camera.mainCamera.global2Relative(this.Transform.X, this.Transform.Y);
            // this.Transform.X = relativeTrans.Item1;
            // this.Transform.Y = relativeTrans.Item2;
            Console.WriteLine("Background: X:" + this.Transform.X + "Y:" + this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
        }
    
        public void handleInput(InputEvent inp, string eventType)
        {
        
            
        }

    }
}
