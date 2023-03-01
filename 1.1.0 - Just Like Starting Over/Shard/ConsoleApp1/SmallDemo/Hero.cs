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
        private string sprite;
        //private int spriteCounter, spriteCounterDir;
        //private double spriteTimer;
        private string direction;
        private double speed = 100, jumpSpeed = 260;
        private AnimationCollection mountain = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 400.0f;
            this.Transform.Y = 300.0f;
            mountain.addAnimation("right", () => new Animation("mountain-", 6, 0.6));
            mountain.addAnimation("left", () => new Animation("mountain-left-", 6, 0.6));
            mountain.addAnimation("rightattack1", () => new Animation("mountain-attack-", 6, 0.6));
            mountain.addAnimation("leftattack1", () => new Animation("mountain-left-attack-", 6, 0.6));
            mountain.addAnimation("rightattack2", () => new Animation("mountain-attack2-", 6, 0.8));
            mountain.addAnimation("leftattack2", () => new Animation("mountain-left-attack2-", 6, 0.8));
            mountain.addAnimation("rightattack3", () => new Animation("mountain-attack3-", 6, 1));
            mountain.addAnimation("leftattack3", () => new Animation("mountain-left-attack3-", 6, 1));
            mountain.updateCurrentAnimation("right");
            direction = "left";
            // sprite = "mountain-";
            // spriteTimer = 0;
            // spriteCounter = 1;
            // spriteCounterDir = 1;

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
                    //sprite = "mountain-";
                    direction = "right";
                    mountain.updateCurrentAnimation(direction);
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    direction = "left";
                    mountain.updateCurrentAnimation(direction);
                    //sprite = "mountain-left-";
                    
                }
                
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_L)
                {
                    mountain.repeatAnimtaion(direction + "attack2", 1);
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
                //MyBody.addForce(this.Transform.Forward, 0.1f);
                Transform.translate(1 * speed * Bootstrap.getDeltaTime(), 0);
                //spriteTimer += Bootstrap.getDeltaTime();
            }

            if (left)
            {
                Transform.translate(-1 * speed * Bootstrap.getDeltaTime(), 0);
                //spriteTimer += Bootstrap.getDeltaTime();
                //MyBody.addForce(this.Transform.Forward, -0.1f);
            }
        }

        public override void update()
        {

            // if (spriteTimer > 0.1f)
            // {
            //     spriteTimer -= 0.1f;
            //     spriteCounter += spriteCounterDir;
            //
            //     if (spriteCounter >= 4)
            //     {
            //         spriteCounterDir = -1;
            //     }
            //
            //     if (spriteCounter <= 1)
            //     {
            //         spriteCounterDir = 1;
            //     }
            // }
            
            mountain.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(mountain.getCurrentSprite());
            
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