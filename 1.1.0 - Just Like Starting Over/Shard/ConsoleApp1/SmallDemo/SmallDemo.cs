using System;
using SmallDemo;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissileCommand;

namespace Shard
{
    class SmallDemo: Game, InputListener
    {
        //GameObject background;
        
        public override void update()
        {
            Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);

        }

        public void handleInput(InputEvent inp, string eventType)
        {
            if (eventType == "MouseDown")
            {
                Console.WriteLine("presspresspresspresspresspressbutton" + inp.Button);
            }
            
        }

        public void createHero()
        {
            GameObject hero = new Hero();
            Camera.mainCamera.Bundle = hero;
        }

        public void createbackground()
        {
            GameObject bg = new Background();
        }

        public void createCamera()
        {
            Camera mainCamera = new Camera();
        }

        public override void initialize()
        {
            Bootstrap.getInput().addListener(this);
            createbackground();
            createHero();
        }
    }
}