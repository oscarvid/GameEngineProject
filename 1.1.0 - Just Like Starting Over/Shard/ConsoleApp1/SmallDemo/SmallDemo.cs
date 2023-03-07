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
        private double spawnEnemyCount;
        private string[] enemyTypes = { "enemy3, enemy4" };
        
        public override bool isRunning()
        {
            return true;
        }

        public bool isWin()
        {
            return hero.IsWin;
        }
        
        public bool isLose()
        {
            return hero.IsLose;
        }

        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
            if (isWin())
            {
                Color col = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                createBlack();
                Bootstrap.getDisplay().showText("YOU WIN!", 150, 168, 64, col);
            }
            
            if (isLose())
            {
                Color col = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                createBlack();
                Bootstrap.getDisplay().showText("GAME OVER", 140, 168, 64, col);
            }

            if(spawnEnemyCount > 20.0)
            {
                EnemyFactory.Instance.createEnemy("enemy3", 800f, 250f);
                spawnEnemyCount = 0;
            }

            spawnEnemyCount += Bootstrap.getDeltaTime();
        }

        private void createBlack()
        {
            GameObject black = new Black();
        }
        private void createGround()
        {
            GameObject ground = new Ground();
        }

        public void createHero()
        {
            hero = new Hero();
            Camera.mainCamera.Bundle = hero;
            GameObject dashboard = new Dashboard(hero);
        }

        public void createbackground()
        {
            GameObject bg = new Background();
        }
        
        public void createFood()
        {
            GameObject food1 = new Food(800, 280, "Fresh_cut_crab_sashimi.png", 10, "sashimi");
            GameObject food2 = new Food(1500, 250, "High_pressure_re-bake_soup.png", 10, "soup");
        }
        
        public void createWinFlag()
        {
            GameObject winFlag = new WinFlag();
        }

        public override void initialize()
        {
            SoundSystem.mainSoundSystem.addSoundCategory("backgroundMusic", new SoundCategory(1));
            SoundSystem.mainSoundSystem.addSoundCategory("attackSound", new SoundCategory(2));
            random = new Random();
            createbackground();
            createGround();
            createWinFlag();
            createFood();
            createHero();
            EnemyFactory.Instance.createEnemy("enemy3", 800f, 250f);
            EnemyFactory.Instance.createEnemy("enemy4", 1100f, 250f);
            //createDashboard();
            Bootstrap.getSound().playSound ("act16side.wav");
        }
    }
}