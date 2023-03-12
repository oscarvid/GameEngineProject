using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard;

namespace SmallDemo
{
    abstract class Enemy: GameObject, CollisionHandler
    {
        protected bool left, right, isDead, attacking;
        protected int health, speed, defence;
        protected string direction;
        protected int leftmax = 800, rightmax = 1200;
        protected float distanceToHero;
        protected AnimationCollection enemyAnimations = new AnimationCollection();
        protected double shootCount;

        public override void initialize()
        {
            //ToDo set x,y
            //ToDo set animation
            setPhysicsEnabled();
            MyBody.Mass = 2;
            MyBody.UsesGravity = true;
            MyBody.addRectCollider();
            MyBody.DebugColor = Color.Transparent;

            addTag("enemy");

            direction = "left";
            isDead = false;

        }

        public override void update()
        {
            if (!isDead && !attacking)
            {
                if (Transform.X - 300 <= Montor.attackTargetForEnemy.getMontorX()) //attack
                {
                    if (!left)
                    {
                        right = false;
                        left = true;
                        direction = "left";
                        enemyAnimations.updateCurrentAnimation(direction);
                    }
                    Bullet b = new Bullet();
                    float x = Transform.X - 8;
                    b.shoot(x, Transform.Centre.Y, direction, "enemyBullet");
                    Console.WriteLine("b wid: " + b.Transform.Wid);
                }
                if (Transform.X + 300 >= Montor.attackTargetForEnemy.getMontorX()) //attack
                {
                    if (!right)
                    {
                        right = true;
                        left = false;
                        direction = "right";
                        enemyAnimations.updateCurrentAnimation(direction);
                    }
                    Bullet b = new Bullet();
                    float x = Transform.Centre.X + (Transform.Wid / 2);
                    b.shoot(x, Transform.Centre.Y, direction, "enemyBullet");
                }
                
                if (Transform.X >= rightmax) 
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
            }
            
            if (x.Parent.checkTag("redBullet"))
            {
                health -= (861 - defence) * 10;
                Console.WriteLine("health:"+ health);
            }

            if (x.Parent.checkTag("hero") && !attacking)
            {
                Console.WriteLine("ENMEY ATTACK!");
                attacking = true;
                enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
            }
            
            if (x.Parent.checkTag("enemy3") || x.Parent.checkTag("enemy4"))
            {
                if (right)
                {
                    right = false;
                    left = true;
                    direction = "left";
                }
                else
                {
                    right = true;
                    left = false;
                    direction = "right";
                }
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
                //attacking = false;
            }
        }
        public void onCollisionStay(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero"))
            {
                attacking = true;
                enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
            }
        }
    }
}
