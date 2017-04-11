using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using System.Reflection;


namespace TradingLib.MoniterBase
{
    public class SoundNotify
    {
        SoundPlayer player;
        public SoundNotify()
        {
            player = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("TradingLib.MoniterBase.Properties.Resources.cashnotify"));
        }

        public void Play()
        {
            player.Play();
        }
        
    }
}
