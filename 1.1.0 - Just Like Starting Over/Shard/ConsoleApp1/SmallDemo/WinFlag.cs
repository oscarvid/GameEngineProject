using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class WinFlag: GameObject, InputListener, CollisionHandler
    {
        private string direction;
        //private double speed = 100, jumpSpeed = 260;
        private AnimationCollection win = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 1800.0f;
            this.Transform.Y = 200.0f;
            win.addAnimation("winFlag", () => new Animation("win-", 1, 1));
            win.updateCurrentAnimation("winFlag");


            Bootstrap.getInput().addListener(this);

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

            addTag("winFlag");

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            
        }

        public override void physicsUpdate()
        {
            
        }

        public override void update()
        {

            //win.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(win.getCurrentSprite());
            
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