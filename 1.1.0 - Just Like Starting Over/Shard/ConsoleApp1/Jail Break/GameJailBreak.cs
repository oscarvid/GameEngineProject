using GameJailBreak;
using System;

namespace Shard
{
    class GameJailBreak : Game
    {

        private GameObject player;
        private GameObject background;

        public override void initialize()
        {
            createPlayer();
            createBackground();
        }

        public override void update()
        {

        }

        private void createPlayer()
        {
            player = new Player();
            Camera.mainCamera.Bundle = player;
        }

        private void createBackground()
        {
            background = new Background();

        }


    }
}
