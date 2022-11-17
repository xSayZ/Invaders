using System.Diagnostics.CodeAnalysis;
using SFML.Graphics;
using SFML.System;

namespace Invader_Omexamination
{
    public class Actor : Entity
    {
        protected Vector2f facingDirection;
        protected int direction;
        public float speed;
        
        public static bool canTakeDamage;
        
        protected bool AbleToShoot;
        protected float CooldownTime;
        protected float ShootCooldown => 0.3f;

        protected Actor(string textureName) : base("spritesheet")
        {
            _sprite.TextureRect = new IntRect(0, 0, 8, 8);
        }
        
        public override void Create(Scene scene)
        {
            _sprite.Origin = new Vector2f(_sprite.TextureRect.Width / 2, _sprite.TextureRect.Height / 2);
            _sprite.Scale = _sprite.Scale * 4f;
            base.Create(scene);
        }
        
        protected virtual void Shoot(Scene scene)
        {
            if(!AbleToShoot) return;

            CooldownTime = ShootCooldown;
            AbleToShoot = false;
            
            Bullets bullets = new Bullets(this);
            bullets.Create(Position, facingDirection, scene);
            scene.Spawn(bullets);
            
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);

            if (!AbleToShoot)
            {
                CooldownTime -= deltaTime;
            }

            if (CooldownTime < 0)
            {
                AbleToShoot = true;
                CooldownTime = ShootCooldown;
            }
        }
        
        protected virtual void Move(float deltaTime) {}
    }
}