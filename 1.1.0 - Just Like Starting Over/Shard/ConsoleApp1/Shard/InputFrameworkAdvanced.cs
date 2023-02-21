using SDL2;
using System.Collections.Generic;

namespace Shard
{

    class InputFrameworkAdvanced : InputSystem
    {
        double tick, timeInterval;

        public override void initialize()
        {
            tick = 0;
            timeInterval = 1.0 / 60.0;
            SDL.SDL_Init(SDL.SDL_INIT_GAMECONTROLLER);
            connectControllers();
        }


        //Add intial controllers.
        private void connectControllers()
        {
            for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
            {
                if (SDL.SDL_IsGameController(i) == SDL.SDL_bool.SDL_TRUE)
                {
                    SDL.SDL_GameControllerOpen(i);
                    System.Console.WriteLine("Controller " + i + " Connected!");
                }
            }

        }

        //Connect new controller.
        private void connectController(int device)
        {
            if (SDL.SDL_IsGameController(device) == SDL.SDL_bool.SDL_TRUE)
            {
                SDL.SDL_GameControllerOpen(device);
                //ToDo remove debug print
                System.Console.WriteLine("Controller " + device + " Connected!");
            }
        }

        //Remove controller.
        private void disconnectController(int device)
        {
            if(device >= 0)
            {
                System.IntPtr controller = SDL.SDL_GameControllerFromInstanceID(device);
                SDL.SDL_GameControllerClose(controller);
                //ToDo remove debug print
                System.Console.WriteLine("Controller Disconnected!");
            }
            
            else
            {
                //ToDo remove debug print
                System.Console.WriteLine("Controller not found!!!");
            }
        }

        public override void getInput()
        {
            SDL.SDL_Event ev;
            int res;
            InputEvent ie;

            tick += Bootstrap.getDeltaTime();

            if (tick < timeInterval)
            {
                return;
            }

            while (tick >= timeInterval)
            {
                res = SDL.SDL_PollEvent(out ev);


                if (res != 1)
                {
                    return;
                }

                ie = new InputEvent();


                switch (ev.type)
                {
                    //Handle ButtonDown action for a button on a controller.
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        {
                            ie.Button = ev.cbutton.button;
                            informListeners(ie, "ButtonDown");
                            break;
                        }

                    //Handle ButtonUp action for a button on a controller.
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        {
                            ie.Button = ev.cbutton.button;
                            informListeners(ie, "ButtonUp");
                            break;
                        }
                    
                    //Handle AxisMotion action from joystick.
                    case SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                        {
                            ie.Axis = ev.caxis.axis;
                            ie.AxisValue = ev.caxis.axisValue;
                            informListeners(ie, "AxisMotion");
                            break;
                        }

                    //Handle adding device while running the program.
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                        {
                            connectController(ev.cdevice.which);
                            break;
                        }

                    //Handle removing device while running the program.
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                        {
                            disconnectController(ev.cdevice.which);
                            break;
                        }
                }
            }
        }
    }
}