namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
    }

    public interface IKillable
    {
        void KillTarget();
    }
}
