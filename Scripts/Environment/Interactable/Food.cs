using UnityEngine;

public class Food : Collectable
{
    public int hp;

    override protected void Effect()
    {
        GameObject.FindObjectOfType<Vitality>().Heal(hp);
        Destroy(gameObject);
    }
}

public class Collectable : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetButtonDown("Interaction"))
            Effect();
    }

    virtual protected void Effect() {}
}
