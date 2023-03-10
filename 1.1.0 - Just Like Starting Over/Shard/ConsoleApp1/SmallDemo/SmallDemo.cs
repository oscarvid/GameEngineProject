using System;
using SmallDemo;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        private string[] enemyTypes = { "enemy3", "enemy4" };
        private int enemyNumber;

        // record the enemy occupied space and avoid spawn new enemy at same coordinate
        public static EnemySpace enemySpace = new EnemySpace();
        
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
            if (!isLose() && !isWin())
            {
                //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
                if (enemyNumber < 2)
                {
                    EnemyFactory.Instance.createEnemy(enemyTypes[0], 400, 260f, 150);
                    EnemyFactory.Instance.createEnemy(enemyTypes[1], 700, 260f, 200);
                    enemyNumber += 2;
                    enemySpace.addEnemy(400, 400 + 150);//write a space class
                    enemySpace.addEnemy(700, 700 + 200);
                }
                Random rand = new Random();
                int distance = rand.Next(5, 10) * 50 + (int)hero.Transform.Centre.X;
                int range = rand.Next(100, 450);
                if(enemySpace.addEnemy(distance, distance + range) && spawnEnemyCount > 15.0 && distance <= 1750 && enemyNumber <= 8)
                {
                    
                    int index = rand.Next(0, 2);
                    EnemyFactory.Instance.createEnemy(enemyTypes[index], distance, 260f, range);
                    Console.WriteLine("New " + enemyTypes[index] + " spawned");
                    spawnEnemyCount = 0;
                    enemyNumber++;
                }

                spawnEnemyCount += Bootstrap.getDeltaTime();
            }
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
            Montor.attackTargetForEnemy.Bundle = hero;
            GameObject dashboard = new Dashboard(hero);
        }

        public void createbackground()
        {
            GameObject bg = new Background();
        }
        
        public void createFood()
        {
            GameObject food1 = new Food(600, 150, "Fresh_cut_crab_sashimi.png", 10, "sashimi");
            GameObject food2 = new Food(1200, 120, "High_pressure_re-bake_soup.png", 10, "soup");
            GameObject food3 = new Food(1600, 120, "High_pressure_re-bake_soup.png", 10, "soup");
        }
        
        public void createWinFlag()
        {
            GameObject winFlag = new WinFlag();
        }

        public override void initialize()
        {
            enemyNumber = 0;
            SoundSystem.mainSoundSystem.addSoundCategory("backgroundMusic", new SoundCategory(1));
            SoundSystem.mainSoundSystem.addSoundCategory("attackSound", new SoundCategory(2));
            random = new Random();
            createbackground();
            createGround();
            createWinFlag();
            createFood();
            createHero();
            //EnemyFactory.Instance.createEnemy("enemy3", 600f, 260f);
            //EnemyFactory.Instance.createEnemy("enemy4", 1000f, 260f);
            //createDashboard();
            SoundSystem.mainSoundSystem.playSound("backgroundMusic", "act16side.wav");
            //Bootstrap.getSound().playSound ("act16side.wav");
        }
    }
}