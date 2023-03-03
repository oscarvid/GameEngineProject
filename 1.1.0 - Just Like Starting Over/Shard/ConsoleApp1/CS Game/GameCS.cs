using GameCS;
using System;

namespace Shard
{
    class GameCS : Game
    {

        private GameObject player;
        private GameObject background;
        private GameObject ground;

        public override void initialize()
        {
            //createBackground();
            createPlayer();
            createGround();
        }

        public override void update()
        {
            //Bootstrap.getDisplay().showText("FPS: " + Bootstrap.getSecondFPS() + " / " + Bootstrap.getFPS(), 10, 10, 12, 255, 255, 255);
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

        private void createGround()
        {
            ground = new Ground();
        }
    }
}
