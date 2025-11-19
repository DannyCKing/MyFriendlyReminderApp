using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FriendlyReminder.Utilities
{
    internal enum SoundEnum { doorbell, plunk, ding, alarm, none}
    internal static class Sounds
    {
        internal static void PlaySound(SoundEnum soundName)
        {
            string resourceName = "";

            switch (soundName)
            {
                case SoundEnum.doorbell:
                    resourceName = "doorbell.wav";
                    break;
                case SoundEnum.ding:
                    resourceName = "ding.wav";
                    break;
                case SoundEnum.plunk:
                    resourceName = "plunk.wav";
                    break;
                case SoundEnum.none:
                    resourceName = null;
                    break;
                default:
                    resourceName = "doorbell.wav";
                    break;
            }

            if(string.IsNullOrEmpty(resourceName))
            {
                // no sound
                return;
            }

            var fullPath = Path.Combine("Resources",resourceName);

            var player = new SoundPlayer(fullPath);
            player.Play();
        }
    }
}
