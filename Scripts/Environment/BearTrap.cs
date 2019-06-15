using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class BearTrap : MonoBehaviour
{
    public Sprite closedTrap, closedBloodyTrap, openTrap;
    private SpriteRenderer spr;
    public float secondsToFree = 5f;
    public int damage = 60;

    public AudioSource trapSound;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        spr.sprite = openTrap;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && spr.sprite == openTrap)
        {
            GetComponent<Animator>().Play("Catched");
            spr.sprite = closedBloodyTrap;
            trapSound.Play();
            collision.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, collision.gameObject.transform.position.z);
            collision.gameObject.GetComponent<Vitality>().TakeDamage(damage);
            collision.gameObject.GetComponent<PlayerController>().playerState = PlayerState.TRAPPED;
        }
        else if(collision.gameObject.tag == "Bullet" && spr.sprite == openTrap)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(collision.gameObject);
            spr.sprite = closedTrap;
        }
    }

    private void FreePlayer()
    {
        Debug.Log("otwarto pułapkę");
        GameObject.FindObjectOfType<PlayerController>().playerState = PlayerState.NORMAL;
        GameObject.FindObjectOfType<PlayerController>().gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void OpenTrap()
    {
        spr.sprite = openTrap;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<AudioSource>().Play();
        FreePlayer();
    }
}
