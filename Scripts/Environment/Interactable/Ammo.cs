public class Ammo : Collectable
{
    public int amountOfAmmo = 5;

    protected override void Effect()
    {
        FindObjectOfType<WeaponController>().ammoInMagazines += amountOfAmmo;
        Destroy(gameObject);
    }
}
