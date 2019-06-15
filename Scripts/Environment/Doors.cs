using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class Doors : MonoBehaviour
{
    private bool areOpen = false;
    private float secondsToClose = 0.4f;

    public Sprite openDoorsSprite;
    public Sprite closedDoorsSprite;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Interaction"))
        {
            
            areOpen = !areOpen;
            if (areOpen == true)
                OpenTheDoors();
            else
                CloseTheDoors();
        }
            
    }

    private void OpenTheDoors()
    {
        transform.GetChild(1).GetComponent<EdgeCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = openDoorsSprite;
        GetComponent<AudioSource>().Play();
        areOpen = true;
    }

    private void CloseTheDoors()
    {
        transform.GetChild(1).GetComponent<EdgeCollider2D>().enabled = true;
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = closedDoorsSprite;
        GetComponent<AudioSource>().Play();
        areOpen = false;
    }

    private IEnumerator WaitThenClose()
    {
        yield return new WaitForSeconds(secondsToClose);
        CloseTheDoors();
    }

}
