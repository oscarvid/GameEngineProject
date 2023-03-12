using SDL2;
using System;
using System.Collections.Generic;

namespace Shard
{
    public class SoundSystem
    {
        private Dictionary<string, SoundCategory> soundManager = new Dictionary<string, SoundCategory>();
        private int maxChannel = 16, usedChannel;
        public static SoundSystem mainSoundSystem = new SoundSystem();

        public SoundSystem()
        {
            usedChannel = 0;
        }
        
        public uint playAndGetDev(string file)
        {
            SDL.SDL_AudioSpec have, want;
            uint length, dev;
            IntPtr buffer;

            file = Bootstrap.getAssetManager().getAssetPath(file);

            SDL.SDL_LoadWAV(file, out have, out buffer, out length);
            dev = SDL.SDL_OpenAudioDevice(IntPtr.Zero, 0, ref have, out want, 0);
            //Console.WriteLine("dev:" + dev);

            int success = SDL.SDL_QueueAudio(dev, buffer, length);
            SDL.SDL_PauseAudioDevice(dev, 0);

            return dev;
        }
        
        public int addSoundCategory(string categoryName, SoundCategory soundCategory)
        {
            usedChannel += soundCategory.MaxChannel;
            if (usedChannel > maxChannel)
            {
                return -1;
            }
            soundManager.Add(categoryName, soundCategory);
            return 0;
        }
        
        public void playSound(string categoryName, string fileName)
        {
            uint dev = playAndGetDev(fileName);
            //register the dev to this category
            soundManager[categoryName].addSound(dev);

        }
    }
}