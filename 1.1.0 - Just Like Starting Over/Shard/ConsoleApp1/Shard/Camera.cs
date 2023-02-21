using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Shard
{
    class Camera: GameObject
    {
        private int width = 800, height = 600;
        private GameObject bundle;
        public static Camera mainCamera = new Camera();
        public override void initialize()
        {
            //this.Transform.X = bundle.Transform.X;
            //this.Transform.Y = bundle.Transform.Y;
            width = 800;
            height = 600;
        }

        public override void update()
        {
            Transform.X = bundle.Transform.X - width / 2;
            Transform.Y = bundle.Transform.Y - height / 2;
            Console.WriteLine("Camera: X:" + this.Transform.X + "Y:" + this.Transform.Y);
        }

        public float globalToRelativeX(float x)
        {
            return x - Transform.X;
        }

        public float globalToRelativeY(float y)
        {
            return y - Transform.Y;
        }
        
        // public Tuple<float, float> global2Relative(float x_, float y_)
        // {
        //     return new Tuple<float, float>(x_ - this.Transform.X, y_ - this.Transform.Y);
        // }

        public GameObject Bundle
        {
            get => bundle;
            set => bundle = value;
        }

        
    }
}