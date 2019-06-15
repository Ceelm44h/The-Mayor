using System.Collections;
using UnityEngine;

public class TrapInteraction : MonoBehaviour
{
    private float timer = 0f, timeToHold = 3f;
    public AudioSource openingSound;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && Input.GetButton("Interaction") && transform.parent.GetComponent<SpriteRenderer>().sprite != FindObjectOfType<BearTrap>().openTrap)
        {
            if(!openingSound.isPlaying)
            {
                Debug.Log("gransko");
                openingSound.Play();
            }
                
            StartCoroutine(HoldAButton());
        }
            
    }

    private IEnumerator HoldAButton()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown("Interaction"))
        {
            StopCoroutine(HoldAButton());
            timer = 0f;
            openingSound.Stop();
            
        }
            
        
       else if(timer < timeToHold)
            yield return null;
        else
        {
            timer = 0f;
            transform.parent.GetComponent<BearTrap>().OpenTrap();
        }

    }
}
