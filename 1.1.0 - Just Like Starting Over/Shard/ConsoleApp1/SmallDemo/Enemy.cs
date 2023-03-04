using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard;

namespace SmallDemo
{
    abstract class Enemy: GameObject, CollisionHandler
    {
        protected bool left, right;
        protected int health, speed;
        protected string direction;
        protected AnimationCollection enemyAnimations = new AnimationCollection();

        public override void initialize()
        {
            //ToDo set x,y
            //ToDo set animation
            setPhysicsEnabled();
            MyBody.Mass = 2;
            MyBody.UsesGravity = true;
            MyBody.addRectCollider();

            addTag("enemy");

            direction = "left";
        }

        public override void update()
        {
            enemyAnimations.update();

            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(enemyAnimations.getCurrentSprite());

            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("heroBullet"))
            {
                health -= 10;
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
