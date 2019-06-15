using UnityEngine;

public class Weapon : ScriptableObject
{
    public float throwSpeed, fireDelay;
    public int damage;
    public WeaponType weaponType;
    public GameObject weaponPrefab;
    public AudioClip attackSound, hitSound;

}


public enum WeaponType
{
    MELEE,
    RANGE
}