namespace Character
{
    public class PlayerHealthComponent : HealthComponent
    {

        protected override void Start()
        {
            base.Start();
            PlayerEvents.Invoke_OnPlayerHealthSet(this);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            PlayerEvents.Invoke_OnPlayerHealthSet(this);
        }
    }
}
