using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class Hero: GameObject, InputListener, CollisionHandler
    {
        bool left, right;
        public override void initialize()
        {
            this.Transform.X = 400.0f;
            this.Transform.Y = 300.0f;
            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("right1.png");

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

            addTag("hero");

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                    //move(3, 1);
                    //update();
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    //move(4, 1);
                    //update();
                }

            }
            else if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = false;
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = false;
                }
            }
        }

        public override void physicsUpdate()
        {
            if (right)
            {
                MyBody.addForce(this.Transform.Forward, 0.1f);
            }

            if (left)
            {
                MyBody.addForce(this.Transform.Forward, -0.05f);
            }
        }

        public override void update()
        {
            Console.WriteLine("Hero: X:" + this.Transform.X + "Y:" + this.Transform.Y);
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