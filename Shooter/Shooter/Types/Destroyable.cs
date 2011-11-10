using Microsoft.Xna.Framework;

namespace Shooter.Types
{
    public abstract class Destroyable : Collidable
    {
        protected Destroyable(int health,Vector2 position):base(position)
        {
            Health = health;
        }

        public int  Health
        {
            get; private set;
        }

        public bool IsAlive
        {
            get { return Health > 0; }
        }

        public void Die()
        {
            Health = 0;
            AfterDying();
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
            if(Health<=0) Die();
        }

        protected abstract void AfterDying();
    }
}