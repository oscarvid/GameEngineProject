using System;
using Shard;

namespace SmallDemo
{
    class Dashboard: GameObject
    {
        private Hero hero;
        private HeroHealth[] showHealth;
        private int lastHealth;

        public Dashboard (Hero h)
        {
            hero = h;
            int contain = hero.Health / 50;
            Array.Resize(ref showHealth, contain);
            for (int i = 0; i < contain; i++)
            {
                showHealth[i] = new HeroHealth(Camera.mainCamera.Transform.X + 98 + i * 5, 10, i);
            }
            lastHealth = 35;
        }

        public override void initialize()
        {
            Transform.X = 0.0f;
            Transform.Y = 0.0f;
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("dashboard.png"); //600 * 400
            
        }

        public override void update()
        {
            int nowHealth = hero.Health / 50 - 1;
            if (lastHealth > nowHealth)
            {
                for (int i = Math.Max(nowHealth + 1, 0); i <= lastHealth; i++)
                {
                    showHealth[i].Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("health_1.png");
                }
            }
            else if (lastHealth < nowHealth)
            {
                for (int i = Math.Max(lastHealth + 1, 0); i <= nowHealth; i++)
                {
                    showHealth[i].Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("health.png");
                }
            }

            lastHealth = nowHealth;
            //follow the camera
            Transform.X = Camera.mainCamera.Transform.X;
            Transform.Y = Camera.mainCamera.Transform.Y;
            //Console.WriteLine("Background: X:" + this.Transform.X + "Y:" + this.Transform.Y);
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void bundle (Hero h)
        {
            hero = h;
        }
        
    }
}