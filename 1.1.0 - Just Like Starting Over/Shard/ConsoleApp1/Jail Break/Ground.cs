using System;
using Shard;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jail_Break
{
    class Ground: GameObject
    {
        public override void initialize()
        {
            //Initalize physics.
            setPhysicsEnabled();
            setPhysicsEnabled();
            MyBody.addRectCollider();
            MyBody.Mass = 10;
            MyBody.Kinematic = true;
        }
    }
}
