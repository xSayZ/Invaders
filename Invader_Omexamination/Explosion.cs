using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Invader_Omexamination
{
    public class Explosion : Entity
    {
        private int Frames
        {
            get { return firstFrame; }
            set
            {
                if (value <= spriteDictionary.Count)
                {
                    firstFrame = value;
                }
                else
                {
                    IsDead = true;
                }
            }
        }
        
        private readonly Dictionary<int, IntRect> spriteDictionary;
        private const float timePerFrame = 0.2f;
        private float timer = timePerFrame;
        private int firstFrame = 1;
        
        public Explosion(Vector2f position) : base("spriteSheet")
        {
            _sprite.Position = position;
            _sprite.Scale = _sprite.Scale * 4f;

                spriteDictionary = new Dictionary<int, IntRect>()
            {
                { 1, new IntRect(16, 16, 8, 8)},
                { 2, new IntRect(16, 24, 8, 8)},
                { 3, new IntRect(16, 32, 8, 8)}
            };
        }
        
        public override void Create(Scene scene)
        {
            spriteDictionary.TryGetValue(1, out IntRect value);
            _sprite.TextureRect = value;
            _sprite.Origin = new Vector2f(value.Width / 2, value.Height / 2);
            base.Create(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            Animation(deltaTime);
            base.Update(scene, deltaTime);
        }

        public void Animation(float deltaTime)
        {
            timer = MathF.Max(timer -= deltaTime, 0);
            
            if (timer == 0) 
            {
                timer = timePerFrame;
                Frames++;
                spriteDictionary.TryGetValue(Frames, out IntRect value);
                _sprite.TextureRect = value;
            }
            
        }
    }
}