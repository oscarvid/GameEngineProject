using System;
using Shard;

namespace SmallDemo
{
    class Enemy4: Enemy
    {
        public Enemy4()
        {
            Random ran = new Random();
            leftmax = ran.Next(1800);
            rightmax = leftmax + ran.Next(100, 400);
            Console.WriteLine("enemy3 leftmax: " + leftmax + "rightmax: " + rightmax);
        }
        
        public Enemy4(int left, int right)
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
                
                double heroOffset = Montor.attackTargetForEnemy.getMontorX();
                //Console.WriteLine("hero offset:" + heroOffset);
                if (right)
                {
                    if (Transform.X + 300 >= heroOffset && shootCount >= 5)
                    {
                        attacking = true;
                        enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
                        
                        Bullet b = new Bullet();
                        float x = Transform.Centre.X;
                        b.shoot(x, Transform.Centre.Y, direction, "enemy4Bullet");
                        shootCount = 0;
                    }
                }
                else
                {
                    if (Transform.X - 300 <= heroOffset && shootCount >= 5)
                    {
                        attacking = true;
                        enemyAnimations.repeatAnimtaion(direction + "attack", 1, _ => attacking = false);
                        
                        Bullet b = new Bullet();
                        float x = Transform.X - 8;
                        b.shoot(x, Transform.Centre.Y, direction, "enemy4Bullet");
                        shootCount = 0;
                    }
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
            addTag("enemy4");
            
            enemyAnimations.addAnimation("right", () => new Animation("enemy4-right-", 6, 0.8));
            enemyAnimations.addAnimation("left", () => new Animation("enemy4-left-", 6, 0.8));
            enemyAnimations.addAnimation("rightattack", () => new Animation("enemy4-attack-right-", 15, 1.2));
            enemyAnimations.addAnimation("leftattack", () => new Animation("enemy4-attack-left-", 15, 1.2));
            enemyAnimations.addAnimation("rightdie", () => new Animation("enemy4-die-right-", 10, 1));
            enemyAnimations.addAnimation("leftdie", () => new Animation("enemy4-die-left-", 10, 1));
            enemyAnimations.updateCurrentAnimation(direction);
            
            health = 17500;
            defence = 650;
            speed = 60;
            shootCount = 0;
        }
    }
}