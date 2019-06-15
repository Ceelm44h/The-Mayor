using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class WorldObject : MonoBehaviour
{
    private SpriteRenderer spr;
    public float minAlpha = 0.6f;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(FadeIn());

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(FadeOut());

    }

    public IEnumerator FadeIn()
    {
        for (float i = 0.05f; i < minAlpha; i += 0.05f)
        {
            spr.color = new Color(1f, 1f, 1f, 1f - i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator FadeOut()
    {
        float defaultAlpha = spr.color.a;

        for (float i = 0.05f; i < minAlpha; i += 0.05f)
        {
            spr.color = new Color(1f, 1f, 1f, defaultAlpha + i);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
