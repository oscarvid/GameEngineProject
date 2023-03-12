using Shard;
using System.Drawing;

namespace SmallDemo
{
    class RedBullet: GameObject, CollisionHandler
    {
        private bool left, right;
        private int speed = 300;
        private double bulletLifeTime;
        private AnimationCollection redBulletAnimation = new AnimationCollection();
        public override void update()
        {
            //Movement update
            if (right)
            {
                Transform.translate(1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (left)
            {
                Transform.translate(-1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (bulletLifeTime < 0.4)
            {
                bulletLifeTime += Bootstrap.getDeltaTime();
            }
            else
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }
            
            redBulletAnimation.update();
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(redBulletAnimation.getCurrentSprite());
            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void initialize()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("ball.png");
            redBulletAnimation.addAnimation("right", () => new Animation("redBullet-", 10, 0.7));
            redBulletAnimation.addAnimation("left", () => new Animation("redBullet-left-", 7, 0.7));

            setPhysicsEnabled();

            MyBody.Mass = 2;
            MyBody.StopOnCollision = true;
            MyBody.addRectCollider();
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Transparent;

        }

        public void shoot(float xStart, float yStart, string direction, string tag)
        {
            this.addTag(tag);
            redBulletAnimation.updateCurrentAnimation(direction);
            this.Transform.X = xStart;
            this.Transform.Y = yStart - 18;

            if (direction == "right")
                right = true;
            else
                left = true;
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("enemy") && checkTag("redBullet"))
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }
        }

        public void onCollisionExit(PhysicsBody x)
        {
            
        }

        public void onCollisionStay(PhysicsBody x)
        {
            
        }
    }
}