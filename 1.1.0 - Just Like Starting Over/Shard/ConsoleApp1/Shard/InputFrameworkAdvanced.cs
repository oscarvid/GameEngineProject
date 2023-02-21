using SDL2;

namespace Shard
{

    class InputFrameworkAdvanced : InputSystem
    {
        double tick, timeInterval;
        System.IntPtr controller = System.IntPtr.Zero;
        public override void initialize()
        {
            tick = 0;
            timeInterval = 1.0 / 60.0;
            SDL.SDL_Init(SDL.SDL_INIT_GAMECONTROLLER);
            checkForConnectedControllers();
        }


        //Connect avaiable controllers.
        private void checkForConnectedControllers()
        {
            for (int i = 0; i < SDL.SDL_NumJoysticks(); i++)
            {
                if (SDL.SDL_IsGameController(i) == SDL.SDL_bool.SDL_TRUE)
                {
                    controller = SDL.SDL_GameControllerOpen(i);
                    System.Console.WriteLine("Controller " + i + " Connected!");
                }
            }

        }

        //Close selected controller.
        private void closeControllers(System.IntPtr controller)
        {
            if (controller != System.IntPtr.Zero)
            {
                SDL.SDL_GameControllerClose(controller);
                controller = System.IntPtr.Zero;
                System.Console.WriteLine("Controller Disconnected!");
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


                //Handle ButtonDown action for a button on a controller.
                if (ev.type == SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN)
                {
                    if (ev.cbutton.button == (byte)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A)
                    {
                        System.Console.WriteLine("Pressed A!");
                        ie.Button = (int)ev.cbutton.button;
                        informListeners(ie, "ButtonDown");
                    }
                }

            }
        }
    }
}