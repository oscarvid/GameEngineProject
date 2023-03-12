using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard;
using System.Drawing;

namespace SmallDemo
{
    class Bullet : GameObject, CollisionHandler
    {
        private bool left, right;
        private int speed = 180;
        private double bulletLifeTime;
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

            if (bulletLifeTime < 1)
            {
                bulletLifeTime += Bootstrap.getDeltaTime();
            }
            else
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }

            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void initialize()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("ball.png");

            setPhysicsEnabled();

            //MyBody.Mass = 2;
            MyBody.StopOnCollision = true;
            MyBody.addRectCollider();
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Transparent;

        }

        public void shoot(float xStart, float yStart, string direction, string tag)
        {
            this.addTag(tag);
            this.Transform.X = xStart;
            this.Transform.Y = yStart;

            if (direction == "right")
                right = true;
            else
                left = true;
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero") && this.checkTag("enemy3Bullet"))
            {
                this.ToBeDestroyed = true;
                //Console.WriteLine("Bullet Destroyed");
            }
            
            if (x.Parent.checkTag("hero") && this.checkTag("enemy4Bullet"))
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
