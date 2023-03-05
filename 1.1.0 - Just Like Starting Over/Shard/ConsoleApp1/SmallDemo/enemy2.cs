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
            enemyAnimations.updateCurrentAnimation(direction);

            health = 50;
            speed = 100;
        }
    }
}