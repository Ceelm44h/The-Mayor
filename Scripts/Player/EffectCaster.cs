using System.Collections;
using UnityEngine;

public class EffectCaster : MonoBehaviour
{
    public static EffectCaster instance;

    private void Awake()
    {
        instance = this;
    }

    static public void CastEffect(EffectType effectToCast, float amount, float duration)
    {
        if (effectToCast == EffectType.MOVEMENTCHANGE)
            instance.StartCoroutine(ChangeMovement(amount,duration));
        else if (effectToCast == EffectType.HPCHANGE)
            FindObjectOfType<Vitality>().maxHp += (int)amount;
    }

    static IEnumerator ChangeMovement(float amount, float time)
    {
        FindObjectOfType<PlayerController>().moveSpeed += amount;
        yield return new WaitForSeconds(time);
        FindObjectOfType<PlayerController>().moveSpeed -= amount;
    }

    IEnumerator ChangeHP(int amount, float time)
    {
        FindObjectOfType<Vitality>().maxHp += amount;
        FindObjectOfType<Vitality>().hp += amount;
        yield return new WaitForSeconds(time);
        FindObjectOfType<Vitality>().maxHp -= amount;
        FindObjectOfType<Vitality>().hp -= amount;
    }

    IEnumerator ChangeAccuracy(float amount, float time)
    {
        //todo
        yield return new WaitForSeconds(time);
    }

}

public enum EffectType
{
    MOVEMENTCHANGE,
    HPCHANGE,
    LIGHTNING,
    ACCURACYCHANGE,
    PLAYERSTATECHANGE
}
