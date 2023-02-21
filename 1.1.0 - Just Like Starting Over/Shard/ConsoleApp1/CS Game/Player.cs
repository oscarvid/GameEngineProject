using Shard;
using SDL2;

namespace GameCS
{
    class Player : GameObject, InputListener
    {

        //Movement variables
        private bool left, right;
        private double speed = 100, jumpSpeed = 260;

        //Sprite variables
        private string sprite;
        private int spriteCounter, spriteCounterDir;
        private double spriteTimer;

        public override void initialize()
        {
            //Initial sprite values.
            sprite = "right";
            spriteTimer = 0;
            spriteCounter = 1;
            spriteCounterDir = 1;

            //Initial movement and position values.
            this.Transform.X = 500.0f;
            this.Transform.Y = 500.0f;
            

            Bootstrap.getInput().addListener(this);

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (eventType == "AxisMotion")
            {

                if (inp.Axis == (int)SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX)
                {
                    if (inp.AxisValue > 0)
                    {
                        right = true;
                        left = false;
                        sprite = "right";
                    }
                    
                    else if (inp.AxisValue < 0)
                    {
                        right = false;
                        left = true;
                        sprite = "left";
                    }
                   
                    else
                    {
                        right = false;
                        left = false;
                    }
                }
            }
        }

        public override void update()
        {
            //Movement update.
            if (left)
            {
                this.Transform.translate(-1 * speed * Bootstrap.getDeltaTime(), 0);
                spriteTimer += Bootstrap.getDeltaTime();
            }

            if (right)
            {
                this.Transform.translate(1 * speed * Bootstrap.getDeltaTime(), 0);
                spriteTimer += Bootstrap.getDeltaTime();
            }

            
            //Sprite updates.

            if (spriteTimer > 0.1f)
            {
                spriteTimer -= 0.1f;
                spriteCounter += spriteCounterDir;

                if (spriteCounter >= 4)
                {
                    spriteCounterDir = -1;
                }

                if (spriteCounter <= 1)
                {
                    spriteCounterDir = 1;
                }
            }

            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(sprite + spriteCounter + ".png");
            
            //Draw to screen.
            Bootstrap.getDisplay().addToDraw(this);
        }
    }
}