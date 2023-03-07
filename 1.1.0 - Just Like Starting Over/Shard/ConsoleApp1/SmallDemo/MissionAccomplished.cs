using Shard;

namespace SmallDemo
{
    class MissionAccomplished: GameObject
    {
        public override void initialize()
        {
            this.Transform.X = 0.0f;
            this.Transform.Y = 0.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("missionaccomplished.png"); //2048 * 400
            
        }

        public override void update()
        {
            //Console.WriteLine("Background: X:" + this.Transform.X + "Y:" + this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
        }
    }
}