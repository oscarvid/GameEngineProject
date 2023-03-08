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
        private Action<bool> onTemporaryAnimationEnd;

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
            onTemporaryAnimationEnd?.Invoke(false);
            onTemporaryAnimationEnd = null;
            currentAnimation = animationList[sprite]();
            isTemporary = false;
            // tmp = a
        }

        public void update()
        {
            currentAnimation.update();
        }

        public void repeatAnimtaion(string sprite, int rep, Action<bool> onEnd = null)
        {
            if (!isTemporary)
            {
                tmp = currentAnimation;
            }
            else
            {
                onTemporaryAnimationEnd?.Invoke(false);
            }
            currentAnimation = animationList[sprite]();
            repeat = rep;
            isTemporary = true;
            onTemporaryAnimationEnd = onEnd;
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
                    isTemporary = false;
                    onTemporaryAnimationEnd?.Invoke(true);
                    onTemporaryAnimationEnd = null;
                }
                isChecked = true;
            }
            return currentSprite;
        }
    }
}