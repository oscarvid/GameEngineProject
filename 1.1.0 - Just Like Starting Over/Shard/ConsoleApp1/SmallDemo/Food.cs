using System;
using Shard;

namespace SmallDemo
{
    class Food: GameObject, CollisionHandler
    {
        private int recoverHealth;
        private Action<int> addHealth;

        public Food(float x, float y, string fileName, int recoverHealth_, string foodTag)
        {
            addTag(foodTag);
            recoverHealth = recoverHealth_;
            Transform.X = x;
            Transform.Y = y;
            Transform.SpritePath = Bootstrap.getAssetManager().getAssetPath(fileName);
        }
        
        public override void initialize()
        {
            setPhysicsEnabled();
            MyBody.Mass = 10;
            MyBody.UsesGravity = true;
            MyBody.addRectCollider();
        }
        
        public override void update()
        {
            Bootstrap.getDisplay().addToDraw(this);
        }

        public int RecoverHealth
        {
            set => recoverHealth = value;
            get => recoverHealth;
        }
        
        public void onCollisionEnter(PhysicsBody x)
        {
            if (x.Parent.checkTag("hero"))
            {
                ToBeDestroyed = true;
            }
        }

        public void onCollisionExit(PhysicsBody x)
        {

        }
        public void onCollisionStay(PhysicsBody x)
        {

        }
    }
}