using System;
using SmallDemo;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissileCommand;

namespace Shard
{
    class SmallDemo: Game
    {
        private Hero hero;
        private Random random;
        public override bool isRunning()
        {
            return true;
        }

        public bool isWin()
        {
            return hero.IsWin;
        }

        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
            if (isWin())
            {
                Color col = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                Bootstrap.getDisplay().showText("YOU WIN!", 150, 168, 64, col);
            }
        }

        private void createGround()
        {
            GameObject ground = new Ground();
        }

        public void createHero()
        {
            hero = new Hero();
            Camera.mainCamera.Bundle = hero;
        }

        public void createbackground()
        {
            GameObject bg = new Background();
        }

        // public void createCamera()
        // {
        //     Camera mainCamera = new Camera();
        // }

        public void createEnemy()
        {
            GameObject enemy2 = new enemy2();
            GameObject enemy1 = new enemy1();
        }
        
        public void createWin()
        {
            GameObject winFlag = new WinFlag();
        }
        
        public override void initialize()
        {
            random = new Random();
            createbackground();
            createGround();
            createWin();
            createHero();
            createEnemy();
        }
    }
}