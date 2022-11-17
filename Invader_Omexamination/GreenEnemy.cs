using System;
using SFML.Graphics;
using SFML.System;

namespace Invader_Omexamination
{
    public class GreenEnemy : Actor
    {
        private Random _random;

        public GreenEnemy(float Speed) : base("spritesheet")
        { speed = Speed; }

        public override void Create(Scene scene)
        {
            _sprite.TextureRect = new IntRect(24, 0, 8, 8);

            _random = new Random();
            Position = new Vector2f(_random.Next((int)Bounds.Width, (int)(Program.ScreenSize.Width - Bounds.Width)), 0 - Bounds.Height);
            _sprite.Position = Position;

            facingDirection.X = _random.Next(0, 2) * 1 - 2;
            facingDirection.Y = 1;
            facingDirection = facingDirection / MathF.Sqrt(facingDirection.X * facingDirection.X + facingDirection.Y * facingDirection.Y);
            
            base.Create(scene);
        }

        protected override void Move(float deltaTime)
        {
            //Left
            if (Bounds.Left <= Program.ScreenSize.Left)
            {
                Reflect(new Vector2f(1, 0));
            }
            //Right
            else if (Bounds.Left + Bounds.Width >= Program.ScreenSize.Width)
            {
                Reflect(new Vector2f(-1, 0));
            }

            if (Position.Y >= Program.ScreenSize.Height + Bounds.Height)
            {
                Position = new Vector2f(Position.X, Program.ScreenSize.Top - Bounds.Height);
            }
            
            Position += facingDirection * speed * deltaTime;
        }
        
        protected override void CollideWith(Scene scene, Entity e)
        {
            if (e is not PlayerShip) return;
            scene.OnHealthLoss(1);
            Dead(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Move(deltaTime);
            
            float newRotation = MathF.Atan2(-facingDirection.X, facingDirection.Y) * 180 / MathF.PI;
            _sprite.Rotation = newRotation;
            
            base.Update(scene, deltaTime);

            if (_random.NextDouble() <= 0.0005f)
            {
                Shoot(scene);
            }
        }
        
        public void Reflect(Vector2f normal)  // Reflect function which takes in 
        {                                     // a direction of which its supposed to bounce towards
            facingDirection -= normal * (2 * (
                facingDirection.X * normal.X + 
                facingDirection.Y * normal.Y
            ));
        }
        
        public void Dead(Scene scene)
        {
            Console.WriteLine("Dead");
            scene.Spawn(new Explosion(Position));
            IsDead = true;
        }
    }
}