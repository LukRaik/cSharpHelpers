using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Client
{
    class XNAConsole
    {
        class ConsoleElement
        {
            public string txt;
            double delay;
            double counter;
            public EventHandler usuwanie;
            double alpha;
            public ConsoleElement(string txt)
            {
                delay = 4;
                alpha = 1;
                counter = 0;
                this.txt = txt;
            }
            public void Draw(SpriteBatch spriteBatch,SpriteFont font,Vector2 pos,Color kolor)
            {
                Color color = kolor;
                color*=(float)alpha;
                spriteBatch.DrawString(font, txt, pos, color);
            }
            public void Update(GameTime gameTime)
            {
                if (counter < delay)
                {
                    counter += (double)((double)gameTime.ElapsedGameTime.Milliseconds / 1000);
                }
                else
                {
                    alpha -= 0.02;
                }
                if (alpha <= 0)
                {
                    usuwanie(this, null);
                }
            }
        }
        SpriteFont font;
        Vector2 pos;
        Color kolor;
        int space;
        bool writing;
        public bool draw;
        string console_text;
        Texture2D texture;
        List<ConsoleElement> ListaElementow = new List<ConsoleElement>();
        List<ConsoleElement> ListaDoUsuniecia = new List<ConsoleElement>();
        //EVENTS
        public EventHandler SetPort;
        public EventHandler SetIp;
        public EventHandler Connect;
        public EventHandler Server_msg;
        public EventHandler ShowFps;
        public EventHandler EditorSwitchToTiles;
        public EventHandler EditorSwitchToCollisions;
        //
        public void Przeniesc(object obj, EventArgs e)
        {
            ListaDoUsuniecia.Add((ConsoleElement)obj);
        }
        public XNAConsole(SpriteFont Font, Vector2 Pos, Color Kolor,Texture2D txt)
        {
            console_text = "";
            font = Font;
            pos = Pos;
            kolor = Kolor;
            writing = false;
            space = 12;
            draw = true;
            texture = txt;
        }
        public void Loguj(object obj, EventArgs e)
        {
            ConsoleElement element = new ConsoleElement((string)obj);
            element.usuwanie += Przeniesc;
            ListaElementow.Insert(0, element);
        }
        public void Exec(string text)
        {
            ConsoleElement element = new ConsoleElement(text);
            element.usuwanie += Przeniesc;
            ListaElementow.Insert(0,element);
            if (text.Contains("set port~"))
            {
                string[] buffor = text.Split('~');
                if (SetPort != null) SetPort(buffor[1], null);
            }
            else if (text.Contains("set ip~"))
            {
                string[] buffor = text.Split('~');
                if (SetIp != null) SetIp(buffor[1], null);
            }
            else if (text.Contains("connect~"))
            {
                string[] buffor = text.Split('~');
                if (Connect != null) Connect(buffor[1], null);
            }
            else if (text.Contains("server~"))
            {
                string[] buffor = text.Split('~');
                if (Server_msg != null) Server_msg(buffor[1], null);
            }
            else if (text.Contains("fps~"))
            {
                if (ShowFps != null) ShowFps(null, null);
            }
            else if (text == "edit~tiles")
            {
                if (EditorSwitchToTiles != null) EditorSwitchToTiles(Map_Editor.editor.editor_state.Tile, null);
                Loguj("Zmieniono na tilesy",null);
            }
            else if (text == "edit~collisions")
            {
                if (EditorSwitchToCollisions != null) EditorSwitchToCollisions(Map_Editor.editor.editor_state.Collision, null);
                Loguj("Zmieniono na kolizje", null);
            }
            console_text = "";
        }
        public void Update(GameTime gameTime)
        {
            if (INPUT.Clicked(Keys.F1) == true)
            {
                writing = !writing;
            }
            if (writing)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                foreach(Keys obj in keys)
                {
                    if (INPUT.Clicked(obj)&&(INPUT.IsFromAlphabet(obj)||INPUT.IsAllowed(obj)))
                    {
                        char buffor;
                        if(INPUT.IsAllowed(obj))
                        {
                            switch(obj)
                            {
                                case Keys.OemPeriod:
                                    buffor = '.';
                                    break;
                                case Keys.Space:
                                    buffor = ' ';
                                    break;
                                case Keys.OemTilde:
                                    buffor ='~';
                                    break;

                                default:
                                    buffor = ' ';
                                    break;
                            }
                        }
                        else
                        {
                            if (obj.ToString().Length > 1)
                            {
                                buffor = obj.ToString()[1];
                            }
                            else
                            {
                                buffor = obj.ToString().ToLower()[0];
                            }
                        }
                        this.console_text += buffor;
                    }
                    else if (INPUT.Clicked(Keys.Enter))
                    {
                        Exec(this.console_text);
                    }
                    else if (INPUT.Clicked(Keys.Back))
                    {
                        if (this.console_text.Length > 1)
                        {
                            this.console_text = this.console_text.Substring(0, this.console_text.Length - 1);
                        }
                        else
                        {
                            this.console_text = "";
                        }
                    }
                }

            }
            foreach (ConsoleElement obj in ListaElementow)
            {
                obj.Update(gameTime);
            }
            if (ListaDoUsuniecia.Count > 0)
            {
                foreach (ConsoleElement obj in ListaDoUsuniecia)
                {
                    ListaElementow.Remove(obj);
                }
                ListaDoUsuniecia = new List<ConsoleElement>();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (draw)
            {   
                if (writing)
                {
                    spriteBatch.Draw(texture, new Rectangle(0, 0, 300, 150), Color.White);
                    spriteBatch.DrawString(font, console_text + "_", pos, kolor);
                    if (ListaElementow.Count > 0)
                    {
                        int x = (int)pos.X;
                        int y = (int)pos.Y;
                        y += space;
                        foreach (ConsoleElement obj in ListaElementow)
                        {
                            if (y < 140)
                            {
                                obj.Draw(spriteBatch, font, new Vector2(x, y), kolor);
                                y += space;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (ListaElementow.Count > 0)
                    {
                        int x = (int)pos.X;
                        int y = (int)pos.Y;
                        foreach (ConsoleElement obj in ListaElementow)
                        {
                            if (y < 140)
                            {
                                obj.Draw(spriteBatch, font, new Vector2(x, y), kolor);
                                y += space;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
