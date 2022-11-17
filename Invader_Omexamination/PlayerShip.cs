using System;
using System.Diagnostics;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invader_Omexamination
{
    public class PlayerShip : Actor
    {
        public readonly int MaxHealth = 3;
        public int currentHealth { get; set; }
        
        private readonly float TakeDamageTimer = 3f;
        private float _takeDamageTimer = 0f;
        public bool CanTakeDamage => _takeDamageTimer <= 0;

        public PlayerShip() : base("spritesheet") { }
        
        public override void Create(Scene scene)
        {
            speed = 300f;
            _sprite.TextureRect = new IntRect(0, 0, 8, 16);
            _sprite.Position = new Vector2f(Program.ScreenW / 2, Program.ScreenH - 100);

            currentHealth = MaxHealth;

            base.Create(scene);
            
            scene.LoseHealth += OnLoseHealth;
        }

        protected override void Move(float deltaTime)
        {
            var movement = new Vector2f();

            _sprite.TextureRect = new IntRect(0, 0, 8, 16);
            
            if (Keyboard.IsKeyPressed(Left))
            {
                movement += new Vector2f(-1, 0);
                _sprite.TextureRect = new IntRect(8, 0, 8, 16);
            }
            
            if (Keyboard.IsKeyPressed(Right))
            {
                movement += new Vector2f(1, 0);
                _sprite.TextureRect = new IntRect(16, 0, 8, 16);
            }

            if (Keyboard.IsKeyPressed(Up))
            {
                movement += new Vector2f(0, -1);
            }

            if (Keyboard.IsKeyPressed(Down))
            {
                movement += new Vector2f(0, 1);
            }

            // Sets newPos as the movement
            var newPos = Position + movement * speed * deltaTime;
            
            newPos.X = Math.Clamp
            (
                newPos.X, 
                Program.ScreenSize.Left + _sprite.GetGlobalBounds().Width / 2,
                Program.ScreenSize.Left + Program.ScreenSize.Width - _sprite.GetGlobalBounds().Width / 2
            );
            newPos.Y = Math.Clamp
            (
                newPos.Y, 
                Program.ScreenSize.Top + _sprite.GetGlobalBounds().Height / 2, 
                Program.ScreenSize.Top + Program.ScreenSize.Height - _sprite.GetGlobalBounds().Height / 2
            );

            Position = newPos;
        }
        
        protected override void Shoot(Scene scene)
        {

            if(!AbleToShoot) return;

            CooldownTime = ShootCooldown;
            AbleToShoot = false;
            
            var leftBullet = new Bullets(this);
            leftBullet.Create(new Vector2f(Bounds.Left + 5, Bounds.Top), 
                new Vector2f(0,-1), scene);
            
            var rightBullet = new Bullets(this);
            rightBullet.Create(new Vector2f(Bounds.Left - 5 + Bounds.Width, Bounds.Top),
                new Vector2f(0,-1), scene);

            scene.Spawn(leftBullet); scene.Spawn(rightBullet);
        }

        public override void Update(Scene scene, float deltaTime)
        {

            _takeDamageTimer = Math.Max(_takeDamageTimer -= deltaTime, 0);
            
            Move(deltaTime);
            
            if (Keyboard.IsKeyPressed(Space))
            {
                Shoot(scene);
            }
            
            base.Update(scene, deltaTime);
        }
        
        private void OnLoseHealth(Scene scene, int amount)
        {
            if (!CanTakeDamage) return;
            
            currentHealth -= amount;
            //Console.WriteLine($"Player health: {currentHealth}");
            
            if (currentHealth == 0)
            {
                scene.LoseHealth -= OnLoseHealth;
                IsDead = true;
                scene.Reload();
            }
            else
            {
                _takeDamageTimer = TakeDamageTimer;
            }
        }
        
        public override void Render(RenderTarget target)
        {
            _sprite.Color = CanTakeDamage ? Color.White : new Color(255, 255, 255, 100);
            base.Render(target);
        }
        
    }
}