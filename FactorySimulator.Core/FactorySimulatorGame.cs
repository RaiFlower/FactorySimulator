using System;
using System.Collections.Generic;
using System.Globalization;
using FactorySimulator.Core.Source;
using FactorySimulator.Core.Source.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FactorySimulator.Core
{
    public class FactorySimulatorGame : Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;

        private readonly GameContext _gameContext = new();
        private readonly GameContextView _gameContextView;

        public FactorySimulatorGame()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            Services.AddService(typeof(GraphicsDeviceManager), graphicsDeviceManager);

            _gameContextView = _gameContext.BuildView();

            graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            graphicsDeviceManager.PreferredBackBufferHeight = 720;

            Window.Title = "Factory Simulator";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameContext.InputManager = new(_gameContextView);
            _gameContext.RenderManager = new(_gameContextView, GraphicsDevice);
            _gameContext.SceneManager = new(_gameContextView);
            _gameContext.RegistryManager = new(_gameContextView);
            _gameContext.AssetManager = new(_gameContextView, GraphicsDevice);

            _gameContext.RegistryManager.RegisterContent();

            _gameContext.InputManager.Init();

            _gameContext.SceneManager.SetActiveScene("base:level");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _gameContext.AssetManager.LoadAssets();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameContext.InputManager.Update();
            _gameContext.SceneManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameContext.RenderManager.StartFrame();
            _gameContext.SceneManager.Draw();
            _gameContext.RenderManager.EndFrame();

            base.Draw(gameTime);
        }
    }
}