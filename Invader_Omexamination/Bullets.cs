using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using SFML.Graphics;
using SFML.System;

namespace Invader_Omexamination
{
    public class Bullets : Entity
    {
        private readonly Actor _parent;
        private Vector2f _bulletDirection;
        private readonly float _bulletSpeed = 350f;
        
        public Bullets(Actor parent) : base("spritesheet")
        {
            _parent = parent;
            _bulletSpeed += parent.speed;
        }

        public void Create(Vector2f position, Vector2f direction, Scene scene)
        {
            base.Create(scene);

            Position = position;
            _bulletDirection = direction;
            
            _sprite.Rotation = MathF.Atan2(direction.X, -direction.Y) * 180 / MathF.PI; // rotation as degrees

            _sprite.TextureRect = new IntRect(0, 16, 8, 8);
            _sprite.Origin = new Vector2f(_sprite.TextureRect.Width / 2, _sprite.TextureRect.Height / 2);
            _sprite.Scale = _sprite.Scale * 3f;
        }
        
        public override void Update(Scene scene, float deltaTime)
        {
            // Movement
            Position += _bulletDirection * _bulletSpeed * deltaTime;

            if (!Program.ScreenSize.Contains(Position.X, Position.Y))
            {
                IsDead = true;
            }

            base.Update(scene, deltaTime);
        }
        
        protected override void CollideWith(Scene scene, Entity e)
        {
            if (_parent is PlayerShip && e is PlayerShip) return;
            if (_parent is GreenEnemy && e is GreenEnemy) return;

            if (e is PlayerShip)
            {
                scene.OnHealthLoss(1);
                Console.Write("Player hit");
                IsDead = true;
            }

            else if (e is GreenEnemy)
            {
                ((GreenEnemy)e).Dead(scene);
                IsDead = true;
            }

        }
    }
}