using Shard;

namespace SmallDemo
{
    class Enemy3: Enemy
    {
        public override void initialize()
        {
            base.initialize();
            
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