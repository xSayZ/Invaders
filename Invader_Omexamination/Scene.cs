using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SFML.System;
using SFML.Graphics;


namespace Invader_Omexamination
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    
    public class Scene
    {
        public event ValueChangedEvent LoseHealth;
        public int healthLoss;
        public void OnHealthLoss(int amount) => healthLoss += amount;
        
        private List<Entity> _entities;

        private PlayerShip playerShip;
        public readonly AssetManager Assets;

        public Scene()
        {
            _entities = new List<Entity>();
            //playerShip = new PlayerShip();
            Assets = new AssetManager();
        }

        public void Spawn(Entity entity)
        {
            _entities.Add(entity);
            entity.Create(this);
        }

        public void UpdateAll(float deltaTime)
        {
            // Update all entities
            for (int i = _entities.Count - 1; i >= 0; i--) // iterate backwards
            {
                _entities[i].Update(this, deltaTime);
            }

            // Remove all dead entities
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                if (_entities[i].IsDead)
                {
                    _entities.RemoveAt(i);
                }
            }
            
            if (healthLoss != 0)
            {
                LoseHealth?.Invoke(this, healthLoss);
                healthLoss = 0;
            }
        }

        public void RenderAll(RenderTarget target)
        {
            foreach (var entity in _entities)
            {
                entity.Render(target);
            }
        }

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = _entities.Count - 1;

            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = _entities[i];
                if(entity.IsDead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }

        public void Load()
        {
            HandleSceneChange();
        }

        public void Reload()
        {
            Clear();
            Thread.Sleep(2 * 500);
            Load();
        }

        public void HandleSceneChange()
        {
            SceneLoader();
        }

        private void SceneLoader()
        {
            playerShip = new PlayerShip();
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                _entities.RemoveAt(i);
            }

            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    Spawn(new Background()
                    {
                        Position = new Vector2f(0 + 256 * j, 0 + 256 * k)
                    });
                }
            }

            Spawn(new EnemyManager(""));
            Spawn(new Gui(playerShip, this));
            Spawn(playerShip);
        }

        public void Clear()
        {
            for (int i = _entities.Count - 1; i >= 0; i--) {
                Entity entity = _entities[i];
                _entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }
}
