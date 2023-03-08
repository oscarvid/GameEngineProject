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
            timeInterval = 1.0 / 600.0;
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
                    //System.Console.WriteLine("Controller " + i + " Connected!");
                }
            }

        }

        //Connect new controller.
        private void connectController(int device)
        {
            if (SDL.SDL_IsGameController(device) == SDL.SDL_bool.SDL_TRUE)
            {
                SDL.SDL_GameControllerOpen(device);
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
                Debug.Log("Controller Disconnected!");
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

            //System.Console.WriteLine(tick + " " + timeInterval);

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

                    ////////////////////////
                    //Controller Inputs
                    ////////////////////////

                    //Handle ButtonDown action for a button on a controller.
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                        {
                            ie.Button = ev.cbutton.button;
                            ie.DeviceId = ev.cdevice.which;
                            informListeners(ie, "ButtonDown");
                            break;
                        }

                    //Handle ButtonUp action for a button on a controller.
                    case SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP:
                        {
                            ie.Button = ev.cbutton.button;
                            ie.DeviceId = ev.cdevice.which;
                            informListeners(ie, "ButtonUp");
                            break;
                        }
                    
                    //Handle AxisMotion action from joystick.
                    case SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION:
                        {
                            ie.Axis = ev.caxis.axis;
                            ie.AxisValue = ev.caxis.axisValue;
                            ie.DeviceId = ev.cdevice.which;
                            informListeners(ie, "AxisMotion");
                            break;
                        }

                    //Handle adding device while running the program.
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                        {
                            ie.DeviceId = ev.cdevice.which;
                            connectController(ie.DeviceId);
                            informListeners(ie, "DeviceAdded");
                            break;
                        }

                    //Handle removing device while running the program.
                    case SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                        {
                            ie.DeviceId = ev.cdevice.which;
                            disconnectController(ie.DeviceId);           
                            informListeners(ie, "DeviceRemoved");
                            break;
                        }

                    
                    ////////////////////////
                    //Mouse Inputs
                    ////////////////////////

                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        {
                            ie.Button = ev.button.button;
                            ie.X = ev.button.x;
                            ie.Y = ev.button.y;
                            informListeners(ie, "MouseDown");
                            break;
                        }

                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        {
                            ie.Button = ev.button.button;
                            ie.X = ev.button.x;
                            ie.Y = ev.button.y;
                            informListeners(ie, "MouseUp");
                            break;
                        }

                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        {
                            ie.X = ev.motion.x;
                            ie.Y = ev.motion.y;
                            informListeners(ie, "MouseMotion");
                            break;
                        }

                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        {
                            ie.X = (int)ev.wheel.direction * ev.wheel.x;
                            ie.Y = (int)ev.wheel.direction * ev.wheel.y;
                            informListeners(ie, "MouseWheel");
                            break;
                        }


                    ////////////////////////
                    //Keyboard Inputs
                    ////////////////////////

                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        {
                            ie.Key = (int)ev.key.keysym.scancode;
                            informListeners(ie, "KeyDown");
                            break;
                        }

                    case SDL.SDL_EventType.SDL_KEYUP:
                        {
                            ie.Key = (int)ev.key.keysym.scancode;
                            informListeners(ie, "KeyUp");
                            break;
                        }
                }
                
                tick -= timeInterval;
            }
        }
    }
}