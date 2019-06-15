using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Combat/MeleeWeapon", order = 1)]
public class MeleeWeapon : Weapon
{
    public Vector2 distance;

    public void Hit(Transform _transform)
    {
        Debug.Log("dziab");

        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(FindObjectOfType<WeaponController>().tip.position, distance, 0); //może znaleźć innego gracza
        for (int i = 0; i < enemiesToDamage.Length; ++i)
        {

            if (enemiesToDamage[i].gameObject != _transform.gameObject)
            {

                if (enemiesToDamage[i].GetComponent<Vitality>() != null)
                {
                    Debug.Log("przywaliłem" + enemiesToDamage[i].name);
                    enemiesToDamage[i].GetComponent<Vitality>().TakeDamage(damage);
                }

            }

        }
    }
}
