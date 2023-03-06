using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class enemy2: Enemy
    {
        public override void initialize()
        {
            base.initialize();
            enemyAnimations.addAnimation("right", () => new Animation("enemy2-attack-right-", 19, 1.9));
            enemyAnimations.addAnimation("left", () => new Animation("enemy2-attack-left-", 19, 1.9));
            enemyAnimations.addAnimation("rightdie", () => new Animation("enemy2-attack-right-", 19, 1.9));
            enemyAnimations.addAnimation("leftdie", () => new Animation("enemy2-attack-left-", 19, 1.9));
            enemyAnimations.updateCurrentAnimation(direction);

            health = 50;
            defence = 0;
            speed = 100;
            rightmax = 1800;
            leftmax = 1500;
        }
    }
}