using Shard;
using System;

namespace SmallDemo
{
    class Enemy3: Enemy
    {
        
        public Enemy3()
        {
            Random ran = new Random();
            leftmax = ran.Next(1800);
            rightmax = leftmax + ran.Next(80, 300);
            Console.WriteLine("enemy3 leftmax: " + leftmax + "rightmax: " + rightmax);
        }
        
        public Enemy3(int left, int right)
        {
            leftmax = left;
            rightmax = right;
        }
        
        public override void update()
        {
            if (!isDead && !attacking)
            {
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
                
                if (Transform.X - 300 <= Montor.attackTargetForEnemy.getMontorX() && shootCount >= 3) //attack
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
                    b.shoot(x, Transform.Centre.Y, direction, "enemy3Bullet");
                    attacking = true;
                    enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
                    shootCount = 0;
                }
                else if (Transform.X + 300 >= Montor.attackTargetForEnemy.getMontorX() && shootCount >= 3) //attack
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
                    b.shoot(x, Transform.Centre.Y, direction, "enemy3Bullet");
                    attacking = true;
                    enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
                    shootCount = 0;
                }

                Transform.translate((left ? -1 : 1) * speed * Bootstrap.getDeltaTime(), 0);
            }
            shootCount += Bootstrap.getDeltaTime();
            enemyAnimations.update();

            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(enemyAnimations.getCurrentSprite());

            Bootstrap.getDisplay().addToDraw(this);
        }
        
        public override void initialize()
        {
            
            base.initialize();
            addTag("enemy3");
            
            enemyAnimations.addAnimation("right", () => new Animation("enemy3-right-", 10, 1));
            enemyAnimations.addAnimation("left", () => new Animation("enemy3-left-", 10, 1));
            enemyAnimations.addAnimation("rightattack", () => new Animation("enemy3-attack-right-", 10, 1));
            enemyAnimations.addAnimation("leftattack", () => new Animation("enemy3-attack-left-", 10, 1));
            enemyAnimations.addAnimation("rightdie", () => new Animation("enemy3-die-right-", 10, 1));
            enemyAnimations.addAnimation("leftdie", () => new Animation("enemy3-die-left-", 10, 1));
            enemyAnimations.updateCurrentAnimation(direction);
            
            health = 3800;
            defence = 50;
            rightmax = 1600;
            leftmax = 1200;
            speed = 100;
        }
    }
}