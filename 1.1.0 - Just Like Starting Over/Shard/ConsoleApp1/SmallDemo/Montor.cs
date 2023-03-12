using Shard;

namespace SmallDemo
{
    class Montor
    {
        private GameObject bundle;
        public static Montor attackTargetForEnemy = new Montor();

        public float getMontorX()
        {
            return bundle.Transform.X;
        }
        
        public float getMontorY()
        {
            return bundle.Transform.Y;
        }

        public GameObject Bundle
        {
            get => bundle;
            set => bundle = value;
        }
    }
}