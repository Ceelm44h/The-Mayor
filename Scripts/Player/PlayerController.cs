using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb2d;
    public float moveSpeed, bulletSpeed = 40;

    private float horizontal, vertical, targetOffset = 0.5f;
    public PlayerState playerState = PlayerState.NORMAL;

    public GameObject legs, body;
    private void Awake ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        legs = gameObject.transform.GetChild(2).gameObject;
        body = gameObject.transform.GetChild(1).gameObject;
    }
	
	private void FixedUpdate ()
    {
        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        if (playerState == PlayerState.NORMAL)
            rb2d.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        else if(playerState == PlayerState.TRAPPED) //gracz może jedynie trochę się obracać
        { 
            rb2d.velocity = Vector2.zero;
            if ((transform.rotation.z >= -0.1f && transform.rotation.z <= 0.1f) ||
                (transform.rotation.z >= 0.1f && horizontal < 0f) ||
                (transform.rotation.z <= -0.1f && horizontal > 0f))
                transform.Rotate(new Vector3(0, 0, (horizontal * moveSpeed) / 10));
        }
        else if(playerState == PlayerState.STUNNED) //porusza się dwa razy wolniej i porusza się w przeciwnych kierunkach
        {
            rb2d.velocity = new Vector2((-horizontal * moveSpeed)/10, (-vertical * moveSpeed)/10);
        }

        Vector2 mouse = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 mouseInGame = Camera.main.ScreenToWorldPoint(mouse);

        if (Vector2.Distance(mouseInGame, transform.localPosition) < targetOffset) //lekkie zabezpieczenie przy najeżdżaniu kursorem na postać
        {
            if (mouse.x >= screenPoint.x)
                mouse.x = screenPoint.x + targetOffset;
            else
                mouse.x = screenPoint.x - targetOffset;

            if (mouse.y >= screenPoint.y)
                mouse.y = screenPoint.y + targetOffset;
            else
                mouse.y = screenPoint.y - targetOffset;
        }


        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        rb2d.MoveRotation(angle);
    }

    private void Update()
    {
        

        float horizontal = Mathf.Abs(Input.GetAxis("Horizontal")), vertical = Mathf.Abs(Input.GetAxis("Vertical"));

        float animationSpeed;
        if (horizontal == 0 && vertical != 0)
            animationSpeed = vertical;

        else if (horizontal != 0 && vertical == 0)
            animationSpeed = horizontal;

        else
            animationSpeed = horizontal * vertical;

        legs.GetComponent<Animator>().SetFloat("Speed", animationSpeed);
        body.GetComponent<Animator>().SetFloat("Speed", animationSpeed);

    }

    public void SetStateForSeconds(PlayerState _state, float _seconds)
    {
        playerState = _state;
        StartCoroutine(WaitAndClear(_seconds));
    }

    private IEnumerator WaitAndClear(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        playerState = PlayerState.NORMAL;
    }
}

public enum PlayerState
{
    NORMAL,
    TRAPPED,
    STUNNED
}
