using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimationGame : Game
{
    private const int _WindowWidth = 1280;
    private const int _WindowHeight = 720;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Texture2D _background;
    private CelAnimationSequence _flagSequence, _playerSequenceIdle, _playerSequenceWalk;
    private CelAnimationPlayer _flagAnimation, _playerAnimationIdle, _playerAnimationWalk;

    public InputAndAnimationGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _WindowWidth;
        _graphics.PreferredBackBufferHeight = _WindowHeight;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Texture2D playerSpritesheetIdle = Content.Load<Texture2D>("CharacterIdle");
        Texture2D playerSpritesheetWalk = Content.Load<Texture2D>("CharacterWalk");

        _background = Content.Load<Texture2D>("SnowBG"); 
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Magenta);

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
