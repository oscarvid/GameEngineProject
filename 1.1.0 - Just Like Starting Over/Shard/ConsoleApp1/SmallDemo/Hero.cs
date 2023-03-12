using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace SmallDemo
{
    class Hero: GameObject, InputListener, CollisionHandler
    {
        //Movement variables
        bool left, right, jumpUp, canJump, shoot;
        private double speed = 100, jumpSpeed = 300, jumpCount, invisCount;
        private int deadZone = 9000, health = 1800, defence = 156;
        private double shootCount, specialCount;
        private int speedCoefficient;
        private double speedUpCount;

        //Track pressed buttons
        bool special1, special2;
        
        //Win state
        private bool isWin, isLose;

        //Animation variables
        private string direction;
        private AnimationCollection fia = new AnimationCollection();
        public override void initialize()
        {
            //Initial movement and position values
            this.Transform.X = 300.0f;
            this.Transform.Y = 250.0f;

            //Initial animation setup
            direction = "right";
            fia.addAnimation("right", () => new Animation("fia-right-", 12, 1));
            fia.addAnimation("left", () => new Animation("fia-left-", 12, 1));
            fia.addAnimation("rightattack1", () => new Animation("fia-attack1-right-", 15, 1.2));
            fia.addAnimation("leftattack1", () => new Animation("fia-attack1-left-", 15, 1.2));
            fia.addAnimation("rightattack2", () => new Animation("fia-attack2-right-", 15, 1.2));
            fia.addAnimation("leftattack2", () => new Animation("fia-attack2-left-", 15, 1.2));
            fia.addAnimation("rightdie", () => new Animation("fia-die-right-", 10, 1.2));
            fia.addAnimation("leftdie", () => new Animation("fia-die-left-", 10, 1.2));
            fia.updateCurrentAnimation(direction);

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

            isWin = false;
            isLose = false;
            speedCoefficient = 1;
            speedUpCount = 0;

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
                        if (!right)
                        {
                            right = true;
                            left = false;
                            direction = "right";
                            fia.updateCurrentAnimation(direction);
                        }
                    }

                    else if (inp.AxisValue < -deadZone)
                    {
                        if (!left)
                        {
                            right = false;
                            left = true;
                            direction = "left";
                            fia.updateCurrentAnimation(direction);
                        }
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
                    if (!right)
                    {
                        right = true;
                        left = false;
                        direction = "right";
                        fia.updateCurrentAnimation(direction);
                        speedUpCount = 0;
                    }
                }

                //Walk left
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    if (!left)
                    {
                        left = true;
                        right = false;
                        direction = "left";
                        fia.updateCurrentAnimation(direction);
                        speedUpCount = 0;
                    }
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

                speedUpCount = 0;
            }
        }

        public override void update()
        {
            if (!isLose)
            {
                invisCount += Bootstrap.getDeltaTime();

                if (health <= 0)
                {
                    fia.repeatAnimtaion(direction + "die", 1, _ =>
                    {
                        Black gameoverBackground = new Black();
                        Camera.mainCamera.changeBundle(gameoverBackground, true);
                        Montor.attackTargetForEnemy.Bundle = gameoverBackground;
                        SoundSystem.mainSoundSystem.playSound("attackSound", "gameover.wav");
                        SoundSystem.mainSoundSystem.playSound("backgroundMusic", "gameover-bgm.wav");
                        ToBeDestroyed = true;
                    });
                    isLose = true;
                }

                //Inputs update
                if (right)
                {
                    Transform.translate(speedCoefficient * speed * Bootstrap.getDeltaTime(), 0);
                    speedUpCount += Bootstrap.getDeltaTime();
                }

                if (left)
                {
                    Transform.translate(-1 * speedCoefficient * speed * Bootstrap.getDeltaTime(), 0);
                    speedUpCount += Bootstrap.getDeltaTime();
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

                shootCount += Bootstrap.getDeltaTime();
                if (shoot && shootCount > 0.6f)
                {
                    Bullet b = new Bullet();
                    Random ran = new Random();
                    SoundSystem.mainSoundSystem.playSound("attackSound", "fia-attack-" + ran.Next(4) + ".wav");
                    if (direction == "right")
                    {
                        float x = this.Transform.Centre.X + (this.Transform.Wid / 2);
                        b.shoot(x, this.Transform.Centre.Y, direction, "heroBullet");
                    }
                    else
                    {
                        float x = this.Transform.X - 8;
                        b.shoot(x, this.Transform.Centre.Y, direction, "heroBullet");
                        Console.WriteLine("b wid: " + b.Transform.Wid);
                    }

                    fia.repeatAnimtaion(direction + "attack1", 1);
                    shoot = false;
                    shootCount = 0;
                }

                specialCount += Bootstrap.getDeltaTime();
                if (special1 && special2 && specialCount > 1.0f)
                {
                    RedBullet b = new RedBullet();
                    if (direction == "right")
                    {
                        Console.WriteLine("attack right!");
                        float x = this.Transform.Centre.X + (this.Transform.Wid / 2);
                        b.shoot(x, this.Transform.Centre.Y, direction, "redBullet");
                    }
                    else
                    {
                        Console.WriteLine("attack left!");
                        float x = this.Transform.X - 51;
                        b.shoot(x, this.Transform.Centre.Y, direction, "redBullet");
                    }


                    fia.repeatAnimtaion(direction + "attack2", 1);
                    special1 = false;
                    special2 = false;
                    specialCount = 0;
                }
                //check speed up
                if (speedUpCount >= 1)
                {
                    speedCoefficient = 2;
                }
                else
                {
                    speedCoefficient = 1;
                }
            }

            //Animation update
            fia.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(fia.getCurrentSprite());

            //Draw to screen.
            Bootstrap.getDisplay().addToDraw(this);
        }
        
        public void onCollisionEnter(PhysicsBody x)
        {
            if (isLose || isWin)
            {
                return;
            }
            if (x.Parent.checkTag("ground"))
            {
                canJump = true;
            }
            
            if (x.Parent.checkTag("soup"))
            {
                health = Math.Min(health + 300, 1800);
            }
            
            if (x.Parent.checkTag("sashimi"))
            {
                health = Math.Min(health + 150, 1800);
            }

            if (x.Parent.checkTag("enemy3Bullet"))
            {
                if (invisCount >= 0.3)
                {
                    health -= (300 - defence);
                    //Console.WriteLine("Current health: " + health);
                    invisCount = 0;
                }
            }
            
            if (x.Parent.checkTag("enemy4Bullet"))
            {
                if (invisCount >= 0.3)
                {
                    health -= (500 - defence);
                    //Console.WriteLine("Current health: " + health);
                    invisCount = 0;
                }
            }
            
            if (x.Parent.checkTag("enemy"))
            {
                if(invisCount >= 0.8)
                {
                    health -= 100;
                    //Console.WriteLine("Current health: " + health);
                    invisCount = 0;
                }     
            }
            
            
            if (x.Parent.checkTag("winFlag"))
            {
                MissionAccomplished winBackground = new MissionAccomplished();
                Camera.mainCamera.changeBundle(winBackground, true);
                Montor.attackTargetForEnemy.Bundle = winBackground;
                SoundSystem.mainSoundSystem.playSound("attackSound", "fia-win-0.wav");
                SoundSystem.mainSoundSystem.playSound("backgroundMusic", "win-bgm.wav");
                isWin = true;
                Console.WriteLine("win");
            }
        }
        
        public void onCollisionExit(PhysicsBody x)
        {
            if (isLose || isWin)
            {
                return;
            }
            
            if (x == null)
            {
                return;
            }

            if (x.Parent.checkTag("ground"))
            {
                MyBody.UsesGravity = true;
            }          
        }

        public void onCollisionStay(PhysicsBody x)
        {
            if (isLose || isWin)
            {
                return;
            }
            
            if (x == null)
            {
                return;
            }

            if (x.Parent.checkTag("enemy"))
            {
                if (invisCount >= 0.8)
                {
                    health -= 100;
                    //Console.WriteLine("Current health: " + health);
                    invisCount = 0;
                }
            }

            if (x.Parent.checkTag("ground"))
            {
                canJump = true;
                MyBody.UsesGravity = false;
            }
        }

        public bool IsWin
        {
            get => isWin;
            set => isWin = value;
        }
        
        public bool IsLose
        {
            get => isLose;
            set => isLose = value;
        }
        
        public int Health
        {
            get => health;
            set => health = value;
        }
    }
}