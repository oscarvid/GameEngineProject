using System;
using Shard;
using System.Drawing;

namespace SmallDemo
{
    class ExplosionBullet: Bullet, CollisionHandler
    {
        private bool left, right;
        private int speed = 100;
        private double bulletLifeTime;
        private double lastXCoordinate;
        private AnimationCollection explosion = new AnimationCollection();
        public override void update()
        {
            //Movement update
            if (right)
            {
                double distanceX = 1 * speed * Bootstrap.getDeltaTime();
                double distanceY;
                if (Transform.Y <= 192)
                {
                    distanceY = 0.04 * (2 * distanceX * lastXCoordinate - 45 * distanceX);
                }
                else
                {
                    distanceY = 0;
                }
                lastXCoordinate += distanceX;
                Console.WriteLine("bullet distancex:" + distanceX + " distancey:" + distanceY);
                Console.WriteLine("bullet x:" + Transform.X + " y:" + Transform.Y);
                Transform.translate(distanceX, distanceY);
                //Transform.translate(1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (left)
            {
                Transform.translate(-1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (bulletLifeTime < 1.2)
            {
                bulletLifeTime += Bootstrap.getDeltaTime();
            }
            else
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }
            
            explosion.update();
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(explosion.getCurrentSprite());
            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void initialize()
        {
            explosion.addAnimation("right", () => new Animation("explosionBullet-", 24, 1.2));
            explosion.addAnimation("left", () => new Animation("explosionBullet-left-", 24, 1.2));
            //this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("ball.png");
            Transform.Ht = 118;
            //Transform.Wid = 150;

            setPhysicsEnabled();

            MyBody.addRectCollider();
            MyBody.Mass = 0;
            MyBody.StopOnCollision = true;
            MyBody.UsesGravity = true;
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Red;

        }

        public void shoot(float xStart, float yStart, string direction, string tag)
        {
            addTag(tag);
            explosion.updateCurrentAnimation(direction);
            lastXCoordinate = 0;
            
            Transform.X = xStart;
            Transform.Y = yStart - Transform.Ht;

            if (direction == "right")
                right = true;
            else
                left = true;
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            //TODO: destory after explosion
            if (x.Parent.checkTag("hero") && this.checkTag("enemyBullet"))
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }

            if (x.Parent.checkTag("enemy") && this.checkTag("heroBullet"))
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