using SDL2;
using Shard;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System;

namespace SmallDemo
{
    class WinFlag: GameObject, CollisionHandler
    {
        private string direction;
        private AnimationCollection win = new AnimationCollection();
        public override void initialize()
        {
            this.Transform.X = 1800.0f;
            this.Transform.Y = 200.0f;
            win.addAnimation("winFlag", () => new Animation("win-", 1, 1));
            win.updateCurrentAnimation("winFlag");

            setPhysicsEnabled();

            MyBody.Mass = 10;
            MyBody.StopOnCollision = true;
            MyBody.Kinematic = true;

            MyBody.addRectCollider();

            addTag("winFlag");

        }

        public override void update()
        {

            //win.update();
            
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(win.getCurrentSprite());
            
            Bootstrap.getDisplay().addToDraw(this);
        }
        
        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("Bullet") == false)
            {
                MyBody.DebugColor = Color.Red;
            }
        }
        
        public void onCollisionExit(PhysicsBody x)
        {

            MyBody.DebugColor = Color.Green;
        }

        public void onCollisionStay(PhysicsBody x)
        {
            MyBody.DebugColor = Color.Blue;
        }
    }
}