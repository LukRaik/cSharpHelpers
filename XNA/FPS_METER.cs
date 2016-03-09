using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class FPS_METER
    {
        private int FPS=0;
        private int fps_licznik=0;
        private int old_sec=0;
        private bool counting=false;
        public void Count()
        {
            if(counting==false)
            {
                old_sec = DateTime.Now.Second;
                counting=true;
            }
            if (counting == true) fps_licznik++;
            if (counting == true && old_sec != DateTime.Now.Second)
            {
                counting = false;
                FPS=fps_licznik;
                fps_licznik = 0;
            }
        }
        public string Get_Fps()
        {
            return Convert.ToString(FPS);
        }
    }
}

