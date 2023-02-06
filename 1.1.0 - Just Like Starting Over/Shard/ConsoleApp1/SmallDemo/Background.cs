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
            this.Transform.X = 0.00f;
            this.Transform.Y = 0.00f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("background.jpg");
        
            Bootstrap.getInput().addListener(this);
        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }
    
        public void handleInput(InputEvent inp, string eventType)
        {
        
            
        }

    }
}
