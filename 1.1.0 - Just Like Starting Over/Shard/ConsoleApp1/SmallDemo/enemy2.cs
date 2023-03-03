using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class enemy2: GameObject, InputListener, CollisionHandler
    {
        bool left, right;
        private string sprite;
        private string direction;
        private double speed = 100, jumpSpeed = 260;
        private AnimationCollection enemyTwo = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 50.0f;
            this.Transform.Y = 230.0f;
            enemyTwo.addAnimation("right", () => new Animation("enemy2-attack-right-", 19, 1.9));
            enemyTwo.addAnimation("left", () => new Animation("enemy2-attack-left-", 19, 1.9));
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