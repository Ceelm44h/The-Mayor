using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ForkObject : MonoBehaviour
{
    public MeleeWeapon weapon;
    public Vector2 minSpeedToHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && GetComponent<Rigidbody2D>().velocity.x > minSpeedToHit.x && GetComponent<Rigidbody2D>().velocity.y > minSpeedToHit.y)
        {
            Debug.Log("oberwałeś lecącymi widłami");
            collision.gameObject.GetComponent<Vitality>().TakeDamage(weapon.damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Input.GetButtonDown("Interaction"))
        {
            collision.gameObject.GetComponent<WeaponController>().AddMeleeWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
