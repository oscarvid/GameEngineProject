using System.Collections.Generic;
using System;

namespace Shard
{
    public class AnimationCollection
    {
        //private Dictionary<string, Animation> animationList = new Dictionary<string, Animation>();
        private Dictionary<string, Func<Animation>> animationList = new Dictionary<string, Func<Animation>>();
        private Animation currentAnimation, tmp;
        private bool once;

        public AnimationCollection()
        {
            once = false;
        }

        public void addAnimation(string name, Func<Animation> func)
        {
            animationList.Add(name, func);
        }

        public void updateCurrentAnimation(string sprite)
        {
            currentAnimation = animationList[sprite]();
        }

        public void update()
        {
            currentAnimation.update();
        }

        public void executeOnce(string sprite)
        {
            tmp = currentAnimation;
            currentAnimation = animationList[sprite]();
            once = true;
        }

        public string getCurrentSprite()
        {
            int currentFrame = currentAnimation.Current;
            string currentSprite = currentAnimation.getCurrentSprite();
            if (once && (currentFrame + 1) == currentAnimation.Sum)
            {
                currentAnimation = tmp;
                once = false;
            }
            return currentSprite;
        }
    }
}