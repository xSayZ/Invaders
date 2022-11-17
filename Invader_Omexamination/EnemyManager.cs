using System;
using SFML.System;

namespace Invader_Omexamination
{
    public class EnemyManager : Entity
    {
        private Clock _clock;

        private const float SpawnIntMax = 3;
        private const float SpawnIntMin = 0.2f;
        private const float Speed = 300;
        private const float MaxSpeed = 500;

        private float _spawnInt = SpawnIntMax;
        private float _spawnTimer;
        
        public EnemyManager(string textureName) : base(textureName) {}
        public override void Create(Scene scene)
        {
            _clock = new Clock();
            
            _spawnTimer = _spawnInt - 0.01f;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            
            _spawnInt = Math.Clamp(_spawnInt - deltaTime * 0.05f, SpawnIntMin, SpawnIntMax);
            
            var speed = Math.Max((Speed + _clock.ElapsedTime.AsSeconds()), MaxSpeed);

            _spawnTimer += deltaTime;
            if (!(_spawnTimer >= _spawnInt)) return;
            scene.Spawn(new GreenEnemy(speed));
            _spawnTimer = 0;
        }
    }
}