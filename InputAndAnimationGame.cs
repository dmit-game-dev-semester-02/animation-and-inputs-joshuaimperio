using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01_animation_and_inputs;

public class InputAndAnimationGame : Game
{
    private const int _WindowWidth = 1280;
    private const int _WindowHeight = 720;

    private int _playerX = 512;
    private int _playerY = 400;
    private bool _walking = false;
    private bool _facingLeft = false;
    
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

        _background = Content.Load<Texture2D>("SnowBG");

        // Load the flag animation.
        Texture2D flagSpritesheet = Content.Load<Texture2D>("Flag");
        _flagSequence = new CelAnimationSequence(flagSpritesheet, 165, 1 / 8f);

        _flagAnimation = new CelAnimationPlayer();
        _flagAnimation.Play(_flagSequence);

        // Load the player's idle animation.
        Texture2D playerSpritesheetIdle = Content.Load<Texture2D>("CharacterIdle");
        _playerSequenceIdle = new CelAnimationSequence(playerSpritesheetIdle, 256, 1 / 8f);

        _playerAnimationIdle = new CelAnimationPlayer();
        _playerAnimationIdle.Play(_playerSequenceIdle);

        // Load the player's walk animation.
        Texture2D playerSpritesheetWalk = Content.Load<Texture2D>("CharacterWalk");
        _playerSequenceWalk = new CelAnimationSequence(playerSpritesheetWalk, 256, 1 / 8f);

        _playerAnimationWalk = new CelAnimationPlayer();
        _playerAnimationWalk.Play(_playerSequenceWalk);
        
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();
        
        // Get player input.
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _playerX -= 8;
            _facingLeft = true;
        }

        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
            _playerX += 8;
            _facingLeft = false;
        }

        if(kbCurrentState.IsKeyDown(Keys.Left) ^ kbCurrentState.IsKeyDown(Keys.Right)) // Exclusive or
        {
            _walking = true;
        }
        else
        {
            _walking = false;
        }

        // Player screen wraparound
        if(_playerX < -256)
        {
            _playerX = 1280;
        }
        if(_playerX > 1280)
        {
            _playerX = -256;
        }

        _flagAnimation.Update(gameTime);
        _playerAnimationIdle.Update(gameTime);
        _playerAnimationWalk.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Magenta);

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        
        _flagAnimation.Draw(_spriteBatch, new Vector2(150, 160), SpriteEffects.None);


        // Switch animations depending on whether the player is moving.
        if(_walking)
        {
            if(_facingLeft)
            {
                _playerAnimationWalk.Draw(_spriteBatch, new Vector2(_playerX, _playerY), SpriteEffects.FlipHorizontally);
            }
            else
            {
                _playerAnimationWalk.Draw(_spriteBatch, new Vector2(_playerX, _playerY), SpriteEffects.None);
            }
        }
        else
        {
            if(_facingLeft)
            {
                _playerAnimationIdle.Draw(_spriteBatch, new Vector2(_playerX, _playerY), SpriteEffects.FlipHorizontally);
            }
            else
            {
                _playerAnimationIdle.Draw(_spriteBatch, new Vector2(_playerX, _playerY), SpriteEffects.None);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
