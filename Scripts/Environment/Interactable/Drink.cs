public class Drink : Collectable
{
    public EffectType effectType;
    public float amount, duration;
    override protected void Effect()
    {
        EffectCaster.CastEffect(effectType, amount, duration);
        Destroy(gameObject);
    }
}
