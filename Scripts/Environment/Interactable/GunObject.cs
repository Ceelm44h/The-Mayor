using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GunObject : MonoBehaviour
{
    public RangeWeapon weapon;
    public int ammoInMagazines, currentAmountOfAmmo;
    public Vector2 minSpeedToHit;
    public float wavingOffset;

    private void Awake()
    {
        ammoInMagazines = weapon.ammoInMagazines;
        currentAmountOfAmmo = weapon.currentAmountOfAmmo;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Input.GetButtonDown("Interaction")) //gracz podnosi broń
        {
            collision.GetComponent<WeaponController>().AddRangeWeapon(weapon, ammoInMagazines, currentAmountOfAmmo);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && GetComponent<Rigidbody2D>().velocity.x > minSpeedToHit.x && GetComponent<Rigidbody2D>().velocity.y > minSpeedToHit.y)
        {
            Debug.Log("oberwałeś lecącym pistoletem");
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().clip = weapon.hitSound;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Play();
            collision.transform.GetComponent<PlayerController>().SetStateForSeconds(PlayerState.STUNNED, 3f);
            Destroy(gameObject);
        }
    }
}
