using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class enemy1: Enemy
    {
        public override void initialize()
        {
            base.initialize();
            
            enemyAnimations.addAnimation("right", () => new Animation("enemy1-right-", 9, 1.2));
            enemyAnimations.addAnimation("left", () => new Animation("enemy1-left-", 10, 1.2));
            enemyAnimations.addAnimation("rightattack", () => new Animation("enemy1-attack-right-", 10, 1.2));
            enemyAnimations.addAnimation("leftattack", () => new Animation("enemy1-attack-left-", 10, 1.2));
            enemyAnimations.addAnimation("rightdie", () => new Animation("enemy1-right-", 9, 1.2));
            enemyAnimations.addAnimation("leftdie", () => new Animation("enemy1-left-", 10, 1.2));
            enemyAnimations.updateCurrentAnimation(direction);
            
            health = 20;
            defence = 0;
            speed = 100;
            rightmax = 1000;
            leftmax = 600;
        }
    }
}