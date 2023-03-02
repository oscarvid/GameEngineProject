using Shard;
using SDL2;

namespace GameJailBreak
{
    class Player : GameObject, InputListener
    {

        //Movement variables
        private bool left, right;
        private int deadZone = 9000;

        public override void initialize()
        {


            //Initial movement and position values.
            this.Transform.X = 500.0f;
            this.Transform.Y = 500.0f;

            //Initailize physics.
            setPhysicsEnabled();
            MyBody.addRectCollider();
            MyBody.Mass = 1;
            MyBody.UsesGravity = true;

            Bootstrap.getInput().addListener(this);

        }


        //Input control
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
                    }

                    else if (inp.AxisValue < -deadZone)
                    {
                        right = false;
                        left = true;
                    }

                    else
                    {
                        right = false;
                        left = false;
                    }
                }
            }

            //Using keyboard.
            else if (eventType == "KeyDown")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = true;
                }
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = true;
                }
            }

            else if (eventType == "KeyUp")
            {
                if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_D)
                {
                    right = false;
                }
                else if (inp.Key == (int)SDL.SDL_Scancode.SDL_SCANCODE_A)
                {
                    left = false;
                }
            }
        }

        public override void update()
        {
            //Draw to screen.
            Bootstrap.getDisplay().addToDraw(this);
        }

        public override void physicsUpdate()
        {
            //ToDo implement physics changes.
        }
    }
}