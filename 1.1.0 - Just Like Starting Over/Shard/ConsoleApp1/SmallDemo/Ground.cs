using Shard;
using SDL2;
using System;
using System.Drawing;

namespace SmallDemo
{
    class Ground : GameObject, CollisionHandler
    {
        public override void initialize()
        {
            this.Transform.X = 0.0f;
            this.Transform.Y = 310.0f;

            setPhysicsEnabled();
            MyBody.addRectCollider();
            MyBody.Mass = 10;
            MyBody.Kinematic = true;
            MyBody.DebugColor = Color.Green;
            addTag("ground");

            this.Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath("ground.png");
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