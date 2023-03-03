using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard;
using System.Drawing;

namespace GameCS
{
    class Bullet : GameObject, CollisionHandler
    {
        private int speed = 200;
        public override void update()
        {
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

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero") && this.checkTag("enemyBullet"))
            {
                this.ToBeDestroyed = true;
                Console.WriteLine("Destroyed");
            }

            if (x.Parent.checkTag("enemy") && this.checkTag("heroBullet"))
            {
                this.ToBeDestroyed = true;
                Console.WriteLine("Destroyed");
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
