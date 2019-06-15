using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "Combat/RangeWeapon", order = 2)]
public class RangeWeapon : Weapon
{
    public Bullet bulletPrefab;
    public float bulletSpeed = 40f, gunSpeed = 7f;
    public int ammoInMagazines = 14, maxAmmoInMagazine = 7, currentAmountOfAmmo = 7;
    public AudioClip reloadSound;

    public void Fire(Transform _transform)
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = target - new Vector2(_transform.position.x, _transform.position.y);
        direction.Normalize();
        Bullet bullet = Instantiate(bulletPrefab, _transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Vector2 lookAt = (target - (Vector2)_transform.position).normalized;
        bullet.transform.up = lookAt;

        bullet.RemoveAfterDelay();
    }
    
}
