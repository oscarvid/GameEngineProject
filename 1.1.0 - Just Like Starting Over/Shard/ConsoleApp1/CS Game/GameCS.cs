using GameCS;
using System;

namespace Shard
{
    class GameCS : Game
    {

        private GameObject player;

        public override void initialize()
        {
            createPlayer();
        }

        public override void update()
        {    
            
        }

        private void createPlayer()
        {
            player = new Player();
        }

        
    }
}
