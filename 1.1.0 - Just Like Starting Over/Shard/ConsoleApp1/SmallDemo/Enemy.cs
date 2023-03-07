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
        protected bool left, right, isDead, attcking;
        protected int health, speed, defence;
        protected string direction;
        protected int leftmax = 800, rightmax = 1200;
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
            isDead = false;

        }

        public override void update()
        {
            if (!isDead && !attcking)
            {
                if (Transform.X >=
                    rightmax) //this number should be get from the enemy1 or enemy2 class so that each enemy will have their own active range
                {
                    if (!left)
                    {
                        right = false;
                        left = true;
                        direction = "left";
                        enemyAnimations.updateCurrentAnimation(direction);
                    }
                }
                else if (Transform.X <= leftmax)
                {
                    if (!right)
                    {
                        right = true;
                        left = false;
                        direction = "right";
                        enemyAnimations.updateCurrentAnimation(direction);
                    }
                }

                Transform.translate((left ? -1 : 1) * speed * Bootstrap.getDeltaTime(), 0);
            }

            enemyAnimations.update();

            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(enemyAnimations.getCurrentSprite());

            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (isDead)
            {
                return;
            }
            if (x.Parent.checkTag("heroBullet"))
            {
                health -= (861 - defence);
                Console.WriteLine("health:"+ health);
            }

            if (x.Parent.checkTag("hero"))
            {
                Console.WriteLine("ENMEY ATTACK!");
                attcking = true;
                enemyAnimations.repeatAnimtaion(direction + "attack", 1);
            }

            if (health <= 0)
            {
                enemyAnimations.repeatAnimtaion(direction + "die", 1, _ => ToBeDestroyed = true);
                isDead = true;
            }
        }

        public void onCollisionExit(PhysicsBody x)
        {
            if (x == null)
            {
                return;
            }

            if (x.Parent.checkTag("hero"))
            {
                attcking = false;
            }
        }
        public void onCollisionStay(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero"))
            {
                Console.WriteLine("ENMEY ATTACK!");
                attcking = true;
                enemyAnimations.repeatAnimtaion(direction + "attack", 1);
            }
        }
    }
}
