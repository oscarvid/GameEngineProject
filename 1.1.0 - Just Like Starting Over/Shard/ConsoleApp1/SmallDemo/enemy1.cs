using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class enemy1: GameObject, InputListener, CollisionHandler
    {
        bool left, right;
        private string sprite;
        private string direction;
        private double speed = 100, jumpSpeed = 260;
        private AnimationCollection enemyTwo = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 500.0f;
            this.Transform.Y = 253.0f;
            enemyTwo.addAnimation("right", () => new Animation("enemy1-right-", 9, 1.2));
            enemyTwo.addAnimation("left", () => new Animation("enemy1-left-", 10, 1.2));
            enemyTwo.addAnimation("rightattack", () => new Animation("enemy1-attack-right-", 10, 1.2));
            enemyTwo.addAnimation("leftattack", () => new Animation("enemy1-attack-left-", 10, 1.2));
            enemyTwo.updateCurrentAnimation("right");
            direction = "right";
            

            Bootstrap.getInput().addListener(this);

            left = false;
            right = false;
            
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

            addTag("enemy");

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            
        }

        public override void physicsUpdate()
        {
            
        }

        public override void update()
        {

            enemyTwo.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(enemyTwo.getCurrentSprite());
            
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
        }
    }
}