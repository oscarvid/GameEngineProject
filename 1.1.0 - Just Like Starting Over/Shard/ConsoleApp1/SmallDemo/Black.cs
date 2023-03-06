using Shard;

namespace SmallDemo
{
    class Black: GameObject, InputListener
    {
        public override void initialize()
        {
            this.Transform.X = 0.0f;
            this.Transform.Y = 0.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("black.png"); //2048 * 400
        
            Bootstrap.getInput().addListener(this);
        }

        public override void update()
        {
            //Console.WriteLine("Background: X:" + this.Transform.X + "Y:" + this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
        }
    
        public void handleInput(InputEvent inp, string eventType)
        {
        
            
        }
    }
}