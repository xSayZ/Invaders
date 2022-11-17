using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Invader_Omexamination
{
    public class Gui : Entity
    {
        private readonly string fontName = "8-bit_wonder";
       // private readonly Scene scene;
        private PlayerShip _playerShip;

        private Clock _clock = new Clock();
        private Text _scoreText = new Text();
        private Text _healthText = new Text();
        
        private int currentScore;

        public Gui(PlayerShip playerShip, Scene scene) : base("spritesheet")
        {
            //this.scene = scene;
            _playerShip = playerShip;

            _sprite.TextureRect = new IntRect(32, 56, 8, 8);
            _sprite.Scale = _sprite.Scale * 4f;
            
            _scoreText.Font = scene.Assets.LoadFont(fontName);
            _scoreText.CharacterSize = 20;

            _playerShip.currentHealth = playerShip.MaxHealth;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            currentScore = (int)_clock.ElapsedTime.AsSeconds();
            base.Update(scene, deltaTime); 
        }

        public override void Render(RenderTarget target)
        {
            _sprite.Position = new Vector2f(Program.ScreenSize.Left, Program.ScreenSize.Top + 20);
            for (int i = 0; i < _playerShip.MaxHealth; i++)
            {
                _sprite.TextureRect = i < _playerShip.currentHealth
                    ? new IntRect(32, 56, 8, 8)
                    : new IntRect(0,0,0,0);
                // Space between sprites
                _sprite.Position += new Vector2f(Bounds.Width + 10, 0);
                base.Render(target);
            }
            
            _scoreText.DisplayedString = currentScore == 0 ? "Score " : $"Score {currentScore}";
            
            _scoreText.Position = new Vector2f(Program.ScreenSize.Width - 160, Program.ScreenSize.Top + 20);
            target.Draw(_scoreText);

        }
    }
}