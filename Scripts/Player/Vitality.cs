using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class Vitality : MonoBehaviour
{
    public int hp = 200, maxHp = 200;
    public GameObject lightBlood, strongBlood, bloodEffectPref;

    private float timer = 0f, lightDelay = 15f, strongDelay = 5f, effectDuration = 0.5f;
	
	void Update ()
    {
        timer += Time.deltaTime;
        
        float percentOfHp = 100f * (float)hp / (float)maxHp;

		if(percentOfHp <= 70f && hp >= 50f)
        {
            if(timer > lightDelay)
            {
                LightlyBleed();
                timer = 0f;
            }
        }
        else if(hp < 50f && hp >= 25f)
        {
            if (timer > lightDelay)
            {
                StronglyBleed();
                timer = 0f;
            }
        }
        else if(hp < 25f)
        {
            if (timer > strongDelay)
            {
                StronglyBleed();
                timer = 0f;
            }
        }
	}

    public void TakeDamage(int _damage)
    {
        GameObject bloodEffect = Instantiate(bloodEffectPref, transform.position, Quaternion.identity);
        bloodEffect.transform.position += new Vector3(0f, -0.4f, -0.1f);
        StartCoroutine(WaitAndDestroy(bloodEffect, effectDuration));

        hp -= _damage;
        if (100 * _damage / maxHp < 20)
            LightlyBleed();
        else
            StronglyBleed();
    }

    public void Heal(int _heal)
    {
        hp += _heal;
        if (hp > maxHp)
            hp = maxHp;
    }

    private void LightlyBleed()
    {
        Instantiate(lightBlood, new Vector3(transform.position.x, transform.position.y - 0.65f, lightBlood.transform.position.z), Quaternion.identity);
    }

    private void StronglyBleed()
    {
        Instantiate(strongBlood, new Vector3(transform.position.x, transform.position.y - 0.65f, strongBlood.transform.position.z), Quaternion.identity);
    }

    private IEnumerator WaitAndDestroy(GameObject _target, float _duration)
    {
        yield return new WaitForSeconds(_duration);
        Destroy(_target);
    }
}
