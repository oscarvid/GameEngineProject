using Shard;

namespace SmallDemo
{
    class HeroHealth: GameObject
    {
        private int number;
        private int width = 5;
        public HeroHealth(float x, float y, int no)
        {
            Transform.X = x;
            Transform.Y = y;
            number = no;
        }
        public override void initialize()
        {
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("health.png");
        }

        public override void update()
        {
            Transform.X = Camera.mainCamera.Transform.X + 98 + width * number;
            Transform.Y = Camera.mainCamera.Transform.Y + 10;
            //Console.WriteLine("Background: X:" + this.Transform.X + "Y:" + this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
        }
    }
}