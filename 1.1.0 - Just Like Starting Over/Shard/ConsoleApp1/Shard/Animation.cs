using System;

namespace Shard
{
    public class Animation
    {
        private string sprite;
        private int sum;
        private int current;
        private double loop;
        private long start;
        private double spriteTimer;
        private double deltaTime;

        public Animation(string sprite_, int sum_, double loop_)
        {
            sprite = sprite_;
            sum = sum_;
            loop = loop_;
            deltaTime = loop_ / sum_;
            current = 0;
            spriteTimer = 0;
        }

        public string startAnimation()
        {
            start = DateTime.Now.ToFileTime();
            current = 0;
            return sprite + "0.png";
        }
        
        public void update()
        {
            spriteTimer += Bootstrap.getDeltaTime();
        }

        public double Loop
        {
            get => loop;
            set => loop = value;
        }

        public string Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        public int Sum
        {
            get => sum;
            set => sum = value;
        }
        
        public int Current
        {
            get => current;
            set => current = value;
        }

        public string getCurrentSprite()
        {
            if (spriteTimer >= deltaTime)
            {
                spriteTimer -= deltaTime;
                current++;
                current = current % sum;
            }
            return sprite + current + ".png";
        }
    }
}