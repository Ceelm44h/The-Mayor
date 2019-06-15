using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 0.36f, destroyTime = 0.5f;
    public void RemoveAfterDelay ()
    {
        StartCoroutine(Wait(lifeTime));
    }

    IEnumerator Wait (float _duration)
    {
        yield return new WaitForSeconds(_duration); //to trzeba oczywiście zmienić
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().SetBool("isDestroyed", true);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<BoxCollider2D>());
        StartCoroutine(Wait(destroyTime));
    }

}
