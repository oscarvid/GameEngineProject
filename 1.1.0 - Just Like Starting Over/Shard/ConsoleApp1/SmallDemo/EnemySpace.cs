using System.Collections.Generic;
using System;

namespace SmallDemo
{
    class EnemySpace
    {
        private List<List<int>> enemySpace;
        private int size;

        public EnemySpace()
        {
            enemySpace = new List<List<int>>();
            size = 0;
        }

        private bool compare(List<int> a, List<int> b)
        {
            return a[1] + 50 <= b[0];
        }
        
        public bool addEnemy(int left, int right)
        {
            List<int> insertSpace = new List<int>(2){left, right};
            if (size == 0)
            {
                enemySpace.Add(insertSpace);
                size++;
                return true;
            }
            if (compare(insertSpace, enemySpace[0]))
            {
                enemySpace.Insert(0, insertSpace);
                size++;
                return true;
            }
            
            for (int i = 1; i < size; i++)
            {
                if (compare(enemySpace[i - 1], insertSpace) && compare(insertSpace, enemySpace[i]))
                {
                    enemySpace.Insert(i, insertSpace);
                    size++;
                    return true;
                }
            }
            
            if (compare(enemySpace[size - 1], insertSpace) && right <= 1900)
            {
                enemySpace.Add(insertSpace);
                size++;
                return true;
            }
            
            //sort and save 
            //return 1 means success
            //return 0 means the space is occupied
            display();
            return false;
        }

        public void deleteEnemy(int left, int right)
        {
            List<int> deleteSpace = new List<int>(2) { left, right };
            enemySpace.Remove(deleteSpace);
            display();
        }
        
        public void display()
        {
            Console.WriteLine("Display enemySpace:");
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("[" + enemySpace[i][0] + ", " + enemySpace[i][1] + "]");
            }
        }
    }
}