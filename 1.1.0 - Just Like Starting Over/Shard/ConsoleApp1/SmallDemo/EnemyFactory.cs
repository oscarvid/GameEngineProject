using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shard;

namespace SmallDemo
{
    public sealed class EnemyFactory
    {
        private EnemyFactory(){}

        private static EnemyFactory instance = null;

        public static EnemyFactory Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new EnemyFactory();
                }
                return instance;
            }
        }


        public void createEnemy(string enemyType, float xPos, float yPos)
        {
            if(enemyType == "enemy1"){
                Enemy e = new enemy1();
                e.Transform.X = xPos;
                e.Transform.Y = yPos;
            }
            
            else if(enemyType == "enemy2")
            {
                Enemy e = new enemy2();
                e.Transform.X = xPos;
                e.Transform.Y = yPos;
            }
            
            else if (enemyType == "enemy3")
            {
                Enemy e = new Enemy3();
                e.Transform.X = xPos;
                e.Transform.Y = yPos;
            }
            
            else if (enemyType == "enemy4")
            {
                Enemy e = new Enemy4();
                e.Transform.X = xPos;
                e.Transform.Y = yPos;
            }

            else
            {
                Console.WriteLine("There is no enemy type: " + enemyType);
            }
        }
    }
}
