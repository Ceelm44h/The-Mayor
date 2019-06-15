using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HomeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            StartCoroutine(FadeIn());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        for (float i = 0.05f; i < 1f; i += 0.05f)
        {
            transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - i);
            transform.parent.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut()
    {
        float defaultAlpha = transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color.a;

        for (float i = 0.05f; i < 1f; i += 0.05f)
        {
            transform.parent.GetChild(1).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, defaultAlpha + i);
            transform.parent.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, defaultAlpha + i);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
