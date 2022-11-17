using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;


namespace Invader_Omexamination
{
    public class Entity
    {
        private string textureName = "";
        protected Sprite _sprite;

        public bool IsDead = false;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            _sprite = new Sprite();
        }

        public virtual void Create(Scene scene)
        {
            _sprite.Texture = scene.Assets.LoadTexture(textureName);
        }

        public virtual void Destroy(Scene scene) {}

        public Vector2f Position
        {
            get => _sprite.Position;
            set => _sprite.Position = value;
        }

        public virtual FloatRect Bounds => _sprite.GetGlobalBounds();

        public virtual void Render(RenderTarget target)
        {
            target.Draw(_sprite);
        }

        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(Bounds))
            {
                CollideWith(scene, found);
            }
        }
        
        protected virtual void CollideWith(Scene s, Entity other) {}
    }
}