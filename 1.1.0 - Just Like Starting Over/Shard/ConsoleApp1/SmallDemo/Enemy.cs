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

            if (Transform.X >= 1200)
            {
                right = false;
                left = true;
                direction = "left";
                enemyAnimations.updateCurrentAnimation(direction);
            }
            else if (Transform.X <= 800)
            {
                right = true;
                left = false;
                direction = "right";
                enemyAnimations.updateCurrentAnimation(direction);
            }
            
            Transform.translate((left ? -1 : 1) * speed * Bootstrap.getDeltaTime(), 0);

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
