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
            rightmax = leftmax + ran.Next(200) + 50;
        }
        
        public Enemy4(int left, int right)
        {
            leftmax = left;
            rightmax = right;
        }
        public override void initialize()
        {
            base.initialize();
            
            enemyAnimations.addAnimation("right", () => new Animation("enemy4-right-", 6, 0.8));
            enemyAnimations.addAnimation("left", () => new Animation("enemy4-left-", 6, 0.8));
            enemyAnimations.addAnimation("rightattack", () => new Animation("enemy4-attack-right-", 15, 1.2));
            enemyAnimations.addAnimation("leftattack", () => new Animation("enemy4-attack-left-", 15, 1.2));
            enemyAnimations.addAnimation("rightdie", () => new Animation("enemy4-die-right-", 10, 1));
            enemyAnimations.addAnimation("leftdie", () => new Animation("enemy4-die-left-", 10, 1));
            enemyAnimations.updateCurrentAnimation(direction);
            
            health = 17500;
            defence = 650;
            speed = 100;
        }
    }
}