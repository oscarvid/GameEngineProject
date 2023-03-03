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
        bool left, right, jumpUp, canJump, shoot;
        private double speed = 100, jumpSpeed = 260, jumpCount;
        private int deadZone = 9000, health = 100;

        //Track pressed buttons
        bool special1, special2;

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
            //Joystick input
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


            //Button or Key Down
            if (eventType == "ButtonDown" || eventType == "KeyDown")
            {
                //Walk right
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                    direction = "right";
                    mountain.updateCurrentAnimation(direction);
                }

                //Walk left
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                    direction = "left";
                    mountain.updateCurrentAnimation(direction);
                }

                //Jumping
                else if ((inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A && canJump) || (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_W && canJump))
                {
                    jumpUp = true;
                    canJump = false;
                }

                //Attack 1 (Shoot)
                else if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X || inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_J)
                {
                    shoot = true;
                }


                //Check if special 1 is pressed
                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y || inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_K)
                {
                    special1 = true;
                }

                //Check if special 2 is pressed
                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B || inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_L)
                {
                    special2 = true;
                }
            }

            if (eventType == "ButtonUp" || eventType == "KeyUp")
            {
                //Stop walk right
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                    right = false;

                //Stop walk left
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                    left = false;

                //Check if special 1 is no longer pressed
                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y || inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_K)
                {
                    special1 = false;
                }

                //Check if special 2 is no longer pressed
                if (inp.Button == (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B || inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_L)
                {
                    special2 = false;
                }
            }
        }

        public override void update()
        {
            //Inputs update
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

            if (shoot)
            {
                Bullet b = new Bullet();
                if (direction == "right")
                {
                    float x = this.Transform.Centre.X + this.Transform.Wid / 2 + b.Transform.Wid;
                    b.shoot(x, this.Transform.Centre.Y, direction, "heroBullet");
                }
                else
                {
                    float x = this.Transform.X - b.Transform.Wid - 10;
                    b.shoot(x, this.Transform.Centre.Y, direction, "heroBullet");
                }

                mountain.repeatAnimtaion(direction + "attack1", 1);
                shoot = false;
            }

            if(special1 && special2)
            {
                Bullet b1 = new Bullet();
                float x1 = this.Transform.Centre.X + this.Transform.Wid / 2 + b1.Transform.Wid;
                b1.shoot(x1, this.Transform.Centre.Y, "right", "heroBullet");

                Bullet b2 = new Bullet();
                float x2 = this.Transform.X - b2.Transform.Wid - 10;
                b2.shoot(x2, this.Transform.Centre.Y, "left", "heroBullet");

                mountain.repeatAnimtaion(direction + "attack3", 1);
                special1 = false;
                special2 = false;
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
            if(x != null)
            {
                if (x.Parent.checkTag("ground"))
                {
                    MyBody.UsesGravity = true;
                }
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