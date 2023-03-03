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
        //Movement variables
        bool left, right, jumpUp, canJump;
        private double speed = 100, jumpSpeed = 260, jumpCount;
        private int deadZone = 9000, health = 100;

        //Animation variables
        private string direction;
        private AnimationCollection mountain = new AnimationCollection();
        public override void initialize()
        {
            //Initial movement and position values
            this.Transform.X = 300.0f;
            this.Transform.Y = 250.0f;

            //Initial animation setup
            direction = "right";
            mountain.addAnimation("right", () => new Animation("mountain-", 6, 0.6));
            mountain.addAnimation("left", () => new Animation("mountain-left-", 6, 0.6));
            mountain.addAnimation("rightattack1", () => new Animation("mountain-attack-", 6, 0.6));
            mountain.addAnimation("leftattack1", () => new Animation("mountain-left-attack-", 6, 0.6));
            mountain.addAnimation("rightattack2", () => new Animation("mountain-attack2-", 8, 0.8));
            mountain.addAnimation("leftattack2", () => new Animation("mountain-left-attack2-", 8, 0.8));
            mountain.addAnimation("rightattack3", () => new Animation("mountain-attack3-", 10, 1));
            mountain.addAnimation("leftattack3", () => new Animation("mountain-left-attack3-", 10, 1));
            mountain.updateCurrentAnimation(direction);

            //Add input listener
            Bootstrap.getInput().addListener(this);

            //Initial physics setup
            setPhysicsEnabled();
            MyBody.addRectCollider();
            MyBody.Mass = 1.3f;
            MyBody.UsesGravity = true;
            MyBody.Kinematic = false;
            MyBody.StopOnCollision = true;
            MyBody.DebugColor = Color.Green;
            
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
                        direction = "right";
                        mountain.updateCurrentAnimation(direction);
                    }

                    else if (inp.AxisValue < -deadZone)
                    {
                        right = false;
                        left = true;
                        direction = "left";
                        mountain.updateCurrentAnimation(direction);
                    }

                    else
                    {
                        right = false;
                        left = false;
                    }
                }
            }

            //Controller Buttons
            if (eventType == "ButtonDown")
            {
                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A && canJump)
                {
                    jumpUp = true;
                    canJump = false;
                }

                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X)
                {
                    Console.WriteLine("Create bullet");
                    Bullet b = new Bullet();
                    if (direction == "right")
                    {
                        float x = this.Transform.Centre.X + this.Transform.Wid + 30;
                        b.shoot(x, this.Transform.Y, direction, "heroBullet");
                    }
                    else
                    {
                        float x = this.Transform.Centre.X - this.Transform.Wid - 30;
                        b.shoot(x, this.Transform.Y, direction, "heroBullet");
                    }
                    
                    mountain.repeatAnimtaion(direction + "attack1", 1);
                }
            }

            if (eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                    direction = "right";
                    mountain.updateCurrentAnimation(direction);
                }

                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    direction = "left";
                    mountain.updateCurrentAnimation(direction);
                }
                
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_L)
                {
                    Console.WriteLine("Create bullet");
                    Bullet b = new Bullet();
                    b.addTag("heroBullet");
                    
                    if(direction == "right")
                    {
                        b.Transform.X = this.Transform.X + 100;
                    }
                    else
                    {
                        b.Transform.X = this.Transform.X;
                    }
                    b.Transform.Y = this.Transform.Y - 10;
                    mountain.repeatAnimtaion(direction + "attack1", 1);
                }
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_J)
                {
                    mountain.repeatAnimtaion(direction + "attack2", 1);
                }
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_K)
                {
                    mountain.repeatAnimtaion(direction + "attack3", 1);
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

            MyBody.DebugColor = Color.Green;
        }
        
        public void onCollisionExit(PhysicsBody x)
        {
            if (x.Parent.checkTag("ground"))
            {
                MyBody.UsesGravity = true;
            }

            MyBody.DebugColor = Color.Red;
            
        }

        public void onCollisionStay(PhysicsBody x)
        {
            MyBody.DebugColor = Color.Blue;
            if (x.Parent.checkTag("ground"))
            {
                canJump = true;
                MyBody.UsesGravity = false;
            }
        }

    }
}