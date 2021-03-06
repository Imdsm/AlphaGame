﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AlphaGame.Components;

namespace AlphaGame.Framework
{
    class TitleScreen : IScreen
    {
        private VariableService vars;
        private SpriteFont titleFont;
        private Texture2D background;
        private MenuComponent menu;

        public TitleScreen(Game game)
        {
            vars = ServiceExtensionMethods.GetService<VariableService>(game.Services);
            LoadContent();
        }

        private void LoadContent()
        {
            menu = new MenuComponent(vars.Game,
                x: vars.GraphicsDevice.Viewport.Width / 2,
                y: vars.GraphicsDevice.Viewport.Height - (vars.GraphicsDevice.Viewport.Height/3),
                inactiveColor: Color.White,
                activeColor: Color.Yellow,
                font: "Fonts/menu");

            menu.Add("START GAME", new MenuItemCallback(StartGame));
            menu.Add("HIGH SCORES", new MenuItemCallback(HighScores));
            menu.Add("EXIT", new MenuItemCallback(ExitGame));            

            background = vars.Content.Load<Texture2D>("Artwork/background");
            titleFont = vars.Content.Load<SpriteFont>("Fonts/title");
        }

        private void StartGame()
        {
            vars.CurrentScreen = new GameScreen(vars.Game);
        }

        private void HighScores()
        {
            vars.CurrentScreen = new HighScoreScreen(vars.Game);
        }

        private void ExitGame()
        {
            vars.Game.Exit();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            vars.GraphicsDevice.Clear(Color.Black);
            vars.SpriteBatch.Begin();
            DrawBackground();
            DrawTitleText();
            menu.Draw(gameTime);
            vars.SpriteBatch.End();
        }

        private void DrawBackground()
        {
            vars.SpriteBatch.Draw(background, new Rectangle(0, 0, vars.DisplayWidth, vars.DisplayHeight), Color.White);
        }

        private void DrawTitleText()
        {
            var text = "Space Racer";
            var textSize = titleFont.MeasureString(text);
            var x = vars.GraphicsDevice.Viewport.Width / 2 - (int)textSize.X / 2;
            var y = vars.GraphicsDevice.Viewport.Height / 3 - (int)textSize.Y / 2;

            vars.SpriteBatch.DrawString(titleFont, text, new Vector2(x, y), Color.White);
        }
    }
}