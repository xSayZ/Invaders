using System;

namespace Invader_Omexamination
{
    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);
        
        public event ValueChangedEvent LoseHealth;
            
        public void PublishHealthLoss(int amount) => HealthLoss += amount;
        
        public int HealthLoss;
        
        public void Update(Scene scene)
        {
            if (HealthLoss != 0)
            {
                LoseHealth?.Invoke(scene, HealthLoss);
                HealthLoss = 0;
            }
        }
    }
}