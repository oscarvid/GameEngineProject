using Shard;
using SDL2;
using System;
using System.Drawing;

namespace GameCS
{
    class Player : GameObject, InputListener, CollisionHandler
    {

        //Movement variables
        private bool left, right, jumpUp, canJump;
        private double speed = 100, jumpSpeed = 200, jumpCount;
        private int deadZone = 9000, health = 100;
        private string action;
        private AnimationCollection mountain = new AnimationCollection();

        //Sprite variables

        public override void initialize()
        {
            //Initial movement and position values.
            this.Transform.X = 300.0f;
            this.Transform.Y = 250.0f;

            //Initial animation setup.
            action = "right";
            mountain.addAnimation("right", () => new Animation("mountain-", 6, 0.6));
            mountain.addAnimation("left", () => new Animation("mountain-left-", 6, 0.6));
            mountain.addAnimation("rightattack1", () => new Animation("mountain-attack-", 6, 0.6));
            mountain.addAnimation("leftattack1", () => new Animation("mountain-left-attack-", 6, 0.6));
            mountain.addAnimation("rightattack2", () => new Animation("mountain-attack2-", 8, 0.8));
            mountain.addAnimation("leftattack2", () => new Animation("mountain-left-attack2-", 8, 0.8));
            mountain.addAnimation("rightattack3", () => new Animation("mountain-attack3-", 10, 1));
            mountain.addAnimation("leftattack3", () => new Animation("mountain-left-attack3-", 10, 1));
            mountain.updateCurrentAnimation(action);
            
            Bootstrap.getInput().addListener(this);

            setPhysicsEnabled();

            MyBody.Mass = 1.3f;
            MyBody.UsesGravity = true;
            MyBody.addRectCollider();
            MyBody.Kinematic = false;
            MyBody.DebugColor = Color.Red;

            addTag("hero");

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            //Using joystick.
            if (eventType == "AxisMotion")
            {

                if (inp.Axis == (int)SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX)
                {
                    if (inp.AxisValue > deadZone)
                    {
                        right = true;
                        left = false;
                        action = "right";
                    }
                    
                    else if (inp.AxisValue < -deadZone)
                    {
                        right = false;
                        left = true;
                        action = "left";
                    }
                   
                    else
                    {
                        right = false;
                        left = false;
                        action = "left";
                    }
                }
            }

            if (eventType == "ButtonDown")
            {
                if(inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A && canJump)
                {
                    jumpUp = true;
                    canJump = false;
                }

                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X)
                {
                    Console.WriteLine("Create bullet");
                    Bullet b = new Bullet();
                    b.addTag("heroBullet");
                    b.Transform.X = this.Transform.X + 100;
                    b.Transform.Y = this.Transform.Y;
                }
            }

            //Using keyboard.
            else if(eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                    action = "right";
                }
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    action = "left";
                }
            }

            else if (eventType == "KeyUp")
            {
                if(inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = false;
                }
                else if(inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = false;
                }
            }
        }

        public override void physicsUpdate()
        {
            
        }

        public override void update()
        {
            //Movement update
            if (right)
            {
                Transform.translate(1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (left)
            {
                Transform.translate(-1 * speed * Bootstrap.getDeltaTime(), 0);
            }

            if (jumpUp)
            {
                canJump = false;
                if (jumpCount < 0.3f)
                {
                    this.Transform.translate(0, -1 * jumpSpeed * Bootstrap.getDeltaTime());
                    jumpCount += Bootstrap.getDeltaTime();
                }
                else
                {
                    jumpCount = 0;
                    jumpUp = false;
                }
            }

            //Animation update
            mountain.update();
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(mountain.getCurrentSprite());
            
            //Draw to screen.
            Bootstrap.getDisplay().addToDraw(this);
        }

        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("ground"))
            {
                canJump = true;
            }

            if (x.Parent.checkTag("enemyBullet"))
            {
                health -= 10;
                Console.WriteLine("Current health: " + health);
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