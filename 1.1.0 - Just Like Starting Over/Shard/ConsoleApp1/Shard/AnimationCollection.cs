using System.Collections.Generic;
using System;

namespace Shard
{
    public class AnimationCollection
    {
        //private Dictionary<string, Animation> animationList = new Dictionary<string, Animation>();
        private Dictionary<string, Func<Animation>> animationList = new Dictionary<string, Func<Animation>>();
        private Animation currentAnimation, tmp;
        private int repeat;
        private bool isChecked, isTemporary;

        public AnimationCollection()
        {
            repeat = -1;
            isChecked = false;
            isTemporary = false;
        }

        public void addAnimation(string name, Func<Animation> func)
        {
            animationList.Add(name, func);
        }

        public void updateCurrentAnimation(string sprite)
        {
            currentAnimation = animationList[sprite]();
            isTemporary = false;
        }

        public void update()
        {
            currentAnimation.update();
        }

        public void repeatAnimtaion(string sprite, int rep)
        {
            if (!isTemporary)
            {
                tmp = currentAnimation;//how decide
            }
            currentAnimation = animationList[sprite]();
            repeat = rep;
            isTemporary = true;
        }

        public string getCurrentSprite()
        {
            int currentFrame = currentAnimation.Current;
            string currentSprite = currentAnimation.getCurrentSprite();
            //Console.WriteLine("repeat:" + repeat + "currentFrame:" + currentFrame + "Sum:" + currentAnimation.Sum);
            if (repeat > 0 && (currentFrame + 1) < currentAnimation.Sum && isChecked)
            {
                isChecked = false;
            }
            if (repeat >= 0 && (currentFrame + 1) == currentAnimation.Sum && !isChecked)
            {
                repeat--;
                if (repeat == 0)
                {
                    repeat = -1;
                    currentAnimation = tmp;
                }
                isChecked = true;
            }
            return currentSprite;
        }
    }
}