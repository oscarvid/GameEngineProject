using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class Enemy1_1: GameObject, InputListener, CollisionHandler
    {
        bool left, right;
        private string sprite;
        private string direction;
        private double speed = 50;
        private int rangeLeft = 800, rangeRight = 1200;
        private AnimationCollection normalEnemy_1 = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 800.0f;
            this.Transform.Y = 253.0f;
            normalEnemy_1.addAnimation("right", () => new Animation("enemy1-right-", 9, 1.2));
            normalEnemy_1.addAnimation("left", () => new Animation("enemy1-left-", 10, 1.2));
            normalEnemy_1.addAnimation("rightattack", () => new Animation("enemy1-attack-right-", 10, 1.2));
            normalEnemy_1.addAnimation("leftattack", () => new Animation("enemy1-attack-left-", 10, 1.2));
            normalEnemy_1.updateCurrentAnimation("right");
            direction = "right";
            

            Bootstrap.getInput().addListener(this);

            left = false;
            right = true;
            
            setPhysicsEnabled();
            
            MyBody.Mass = 2;
            MyBody.MaxForce = 10;
            MyBody.AngularDrag = 0.01f;
            MyBody.Drag = 0f;
            MyBody.StopOnCollision = false;
            MyBody.ReflectOnCollision = false;
            MyBody.ImpartForce = false;
            MyBody.Kinematic = false;

            MyBody.addRectCollider();

            addTag("normalEnemy_1");

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            
        }

        public override void physicsUpdate()
        {
            
        }

        public override void update()
        {
            if (Transform.X >= 1200)
            {
                right = false;
                left = true;
                direction = "left";
                normalEnemy_1.updateCurrentAnimation("left");
            } else if (Transform.X <= 800)
            {
                right = true;
                left = false;
                direction = "right";
                normalEnemy_1.updateCurrentAnimation("right");
            }
            Transform.translate((left ? -1 : 1) * speed * Bootstrap.getDeltaTime(), 0);

            normalEnemy_1.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(normalEnemy_1.getCurrentSprite());
            
            Bootstrap.getDisplay().addToDraw(this);
        }
        
        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Bullet") == false)
            {
                MyBody.DebugColor = Color.Red;
            }
        }
        
        public void onCollisionExit(PhysicsBody x)
        {
            MyBody.DebugColor = Color.Green;
        }

        public void onCollisionStay(PhysicsBody x)
        {
            MyBody.DebugColor = Color.Blue;
            if (x.Parent.checkTag("hero") == true)
            {
                normalEnemy_1.repeatAnimtaion(direction + "attack", 1);
                Console.WriteLine(direction + "attack");
            }
        }
    }
}