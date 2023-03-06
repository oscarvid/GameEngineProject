﻿using System;
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
        }

        public override void update()
        {

            if (Transform.X >= rightmax) //this number should be get from the enemy1 or enemy2 class so that each enemy will have their own active range
            {
                right = false;
                left = true;
                direction = "left";
                enemyAnimations.updateCurrentAnimation(direction);
            }
            else if (Transform.X <= leftmax)
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
                health -= (1540 - defence);
                Console.WriteLine("health:"+ health);
            }

            if (health <= 0)
            {
                enemyAnimations.repeatAnimtaion(direction + "die", 1);
                ToBeDestroyed = true;
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
