using Shard;
using SDL2;
using System;
using System.Drawing;

namespace GameCS
{
    class Ground : GameObject, CollisionHandler
    {
        private AnimationCollection mountain = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 250.0f;
            this.Transform.Y = 350.0f;

            setPhysicsEnabled();
            MyBody.addRectCollider();
            MyBody.Mass = 10;
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Green;
            addTag("ground");

            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("platform.png");
        }

        public void onCollisionEnter(PhysicsBody x)
        {

        }

        public void onCollisionExit(PhysicsBody x)
        {

        }

        public void onCollisionStay(PhysicsBody x)
        {

        }

        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }
    }
}