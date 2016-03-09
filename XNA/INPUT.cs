using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Client
{
    static class INPUT
    {
        static private KeyboardState old;
        static public KeyboardState now { get; private set; }
        static private MouseState old_m;
        static public MouseState now_m;
        static public List<Keys> konfiguracja = new List<Keys>();
        static public void Update()
        {
            old = now;
            now = Keyboard.GetState();
            old_m = now_m;
            now_m = Mouse.GetState();
        }
        static public bool Clicked(Keys key)
        {
            if (old.IsKeyUp(key) && now.IsKeyDown(key)) return true;
            return false;
        }
        static public bool ScrollUp()
        {
            if (now_m.ScrollWheelValue > old_m.ScrollWheelValue) return true;
            else return false;
        }
        static public bool ScrollDown()
        {
            if (now_m.ScrollWheelValue < old_m.ScrollWheelValue) return true;
            else return false;
        }
        static public bool IsAllowed(Keys key)
        {
            switch (key)
            {
                case Keys.Space:
                    return true;
                case Keys.OemPeriod:
                    return true;
                case Keys.OemTilde:
                    return true;
                default:
                    return false;
            }
        }
        static public bool IsFromAlphabet(Keys key)
        {
            switch (key)
            {
                case Keys.A:
                    return true;
                case Keys.Q:
                    return true;
                case Keys.Z:
                    return true;
                case Keys.V:
                    return true;
                case Keys.X:
                    return true;
                case Keys.S:
                    return true;
                case Keys.W:
                    return true;
                case Keys.C:
                    return true;
                case Keys.D:
                    return true;
                case Keys.T:
                    return true;
                case Keys.E:
                    return true;
                case Keys.R:
                    return true;
                case Keys.F:
                    return true;
                case Keys.G:
                    return true;
                case Keys.B:
                    return true;
                case Keys.N:
                    return true;
                case Keys.H:
                    return true;
                case Keys.Y:
                    return true;
                case Keys.U:
                    return true;
                case Keys.J:
                    return true;
                case Keys.M:
                    return true;
                case Keys.I:
                    return true;
                case Keys.K:
                    return true;
                case Keys.L:
                    return true;
                case Keys.O:
                    return true;
                case Keys.P:
                    return true;
                case Keys.D1:
                    return true;
                case Keys.D2:
                    return true;
                case Keys.D0:
                    return true;
                case Keys.D3:
                    return true;
                case Keys.D4:
                    return true;
                case Keys.D5:
                    return true;
                case Keys.D6:
                    return true;
                case Keys.D7:
                    return true;
                case Keys.D8:
                    return true;
                case Keys.D9:
                    return true;
                default:
                    return false;
            }
        }
        static public bool ClickedLPM()
        {
            if (old_m.LeftButton == ButtonState.Released && now_m.LeftButton == ButtonState.Pressed) return true;
            return false;
        }
        static public bool HoldLPM()
        {
            if (now_m.LeftButton == ButtonState.Pressed) return true;
            return false;
        }
        static public bool LPMIsFree()
        {
            if (now_m.LeftButton == ButtonState.Released && old_m.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }
        static public bool ClickedPPM()
        {
            if (old_m.RightButton == ButtonState.Released && now_m.RightButton == ButtonState.Pressed) return true;
            return false;
        }
        static public bool HoldPPM()
        {
            if (now_m.RightButton == ButtonState.Pressed) return true;
            return false;
        }
        static public bool Pressed(Keys key)
        {
            if (now.IsKeyDown(key)) return true;
            return false;
        }
        static public bool Relased(Keys key)
        {
            if (now.IsKeyUp(key)) return true;
            return false;
        }
    }
}
