using System;
using System.Numerics;
using SDL2;

namespace Shard
{
    public class SoundCategory
    {
        private int maxChannel, usingChannel, deleteChannel;
        private uint[] channelList;

        public SoundCategory(int maxChannel_)
        {
            deleteChannel = -1;
            usingChannel = 0;
            maxChannel = maxChannel_;
            Array.Resize(ref channelList, maxChannel);
        }

        public void addSound(uint dev) //return max stands for register success, otherwise return the dev need to be turned off
        {
            usingChannel++;
            if (usingChannel > maxChannel)
            {
                deleteChannel++;
                deleteChannel = deleteChannel % maxChannel;
                usingChannel = maxChannel;
                SDL.SDL_CloseAudioDevice(channelList[deleteChannel]);
                channelList[deleteChannel] = dev;
            }
            else
            {
                channelList[usingChannel - 1] = dev;
            }
        }

        public int MaxChannel
        {
            set => maxChannel = value;
            get => maxChannel;
        }
    }
}