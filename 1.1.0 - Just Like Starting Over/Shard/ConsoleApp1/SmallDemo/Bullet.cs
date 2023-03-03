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
        private int speed = 200;
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
                Console.WriteLine("Bullet Destroyed");
            }

            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void initialize()
        {
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("ball.png");

            setPhysicsEnabled();

            MyBody.Mass = 2;
            MyBody.UsesGravity = false;
            MyBody.StopOnCollision = true;
            MyBody.addRectCollider();
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Red;

        }

        public void setDirection(string directon)
        {
            if(directon == "right")
            {
                right = true;
                left = false;
            }

            if (directon == "left")
            {
                right = false;
                left = true;
            }
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero") && this.checkTag("enemyBullet"))
            {
                this.ToBeDestroyed = true;
                Console.WriteLine("Bullet Destroyed");
            }

            if (x.Parent.checkTag("enemy") && this.checkTag("heroBullet"))
            {
                this.ToBeDestroyed = true;
                Console.WriteLine("Bullet Destroyed");
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
