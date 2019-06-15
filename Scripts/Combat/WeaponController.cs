using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapons;
    public int indexOfCurrentWeapon = 0;
    public float targetDistance = 3f;
    public int ammoInMagazines, currentAmountOfAmmo;
    public Transform tip;

    private readonly float shotOffset = 1f;
    private float timer = 0f;

    private void Awake()
    {
        if(weapons[0].weaponType == WeaponType.RANGE)
        {
            ammoInMagazines = ((RangeWeapon)weapons[0]).ammoInMagazines;
            currentAmountOfAmmo = ((RangeWeapon)weapons[0]).currentAmountOfAmmo;
        }
    }

    void Update ()
    {
        timer += Time.deltaTime;

        if (weapons.Count != 0)
        {
            //przeładowanie
            if (Input.GetButtonDown("Reload") && weapons[indexOfCurrentWeapon].weaponType == WeaponType.RANGE && timer > 0f)
            {    
                timer = -1.7f;
                Reload();
            }

            //rzut bronią
            if (Input.GetMouseButtonDown(2))
                Throw(weapons[indexOfCurrentWeapon], PointAtDistance(shotOffset));                
            

            //atak
            if (Input.GetMouseButtonDown(0) && timer > weapons[indexOfCurrentWeapon].fireDelay)
            {
                GetComponent<AudioSource>().clip = weapons[indexOfCurrentWeapon].attackSound;
                timer = 0f;
                if (weapons[indexOfCurrentWeapon].weaponType == WeaponType.RANGE && currentAmountOfAmmo > 0)
                {
                    GetComponent<AudioSource>().Play();
                    currentAmountOfAmmo--;
                    ((RangeWeapon)weapons[indexOfCurrentWeapon]).Fire(tip);
                }
                    
                else if (weapons[indexOfCurrentWeapon].weaponType == WeaponType.MELEE)
                {
                    GetComponent<AudioSource>().Play();
                    ((MeleeWeapon)weapons[indexOfCurrentWeapon]).Hit(tip);
                }
                    
            }

            //celowanie
            if (Input.GetMouseButton(1)) //nacisniecie
                Target();

            if (Input.GetMouseButtonUp(1)) //puszczenie
            {
                Camera.main.GetComponent<CameraController>().SetOffset(Vector3.zero);
                Camera.main.GetComponent<CameraController>().cameraMode = CameraMode.NORMAL;
            }
                

            //obsługa zmiany broni
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                indexOfCurrentWeapon++;
                if (indexOfCurrentWeapon > weapons.Count - 1)
                    indexOfCurrentWeapon = 0;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                indexOfCurrentWeapon--;
                if (indexOfCurrentWeapon < 0)
                    indexOfCurrentWeapon = weapons.Count - 1;
            }

            //wybieranie broni numerami
            if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count > 0 && weapons[0].weaponType == WeaponType.RANGE)
                indexOfCurrentWeapon = 0;

            if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1 && weapons[1].weaponType == WeaponType.MELEE)
                indexOfCurrentWeapon = 1;
        }
    }

    Vector2 PointAtDistance(float _distance)
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float viewfinderX, viewfinderY;
        float targetX = Mathf.Abs(target.x - transform.position.x), targetY = Mathf.Abs(target.y - transform.position.y), sinAlpha = targetY / targetX;

        viewfinderX = _distance / (Mathf.Sqrt(Mathf.Pow(sinAlpha, 2) + 1));
        viewfinderY = viewfinderX * sinAlpha;

        //odpowiednia ćwiartka sinusa
        if (target.y < transform.position.y)
            viewfinderY = -viewfinderY;
        if (target.x < transform.position.x)
            viewfinderX = -viewfinderX;

        Vector2 viewfinder = new Vector2(viewfinderX, viewfinderY) + new Vector2(transform.position.x, transform.position.y);
        return viewfinder;
    }

    public void RemoveWeapon(Weapon _target)
    {
        if (indexOfCurrentWeapon == weapons.Count - 1)
            indexOfCurrentWeapon = 0;
        weapons.Remove(_target);
    }

    public void AddRangeWeapon(RangeWeapon _target, int _ammoInMagazines, int _currentAmmo)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponType == WeaponType.RANGE) //jeżeli gracz już posiada ten typ broni
            {
                Drop(weapon, transform);
                break;
            }
                
        }
        weapons.Insert(0, _target);
        ammoInMagazines = _ammoInMagazines;
        currentAmountOfAmmo = _currentAmmo;
    }

    public void AddMeleeWeapon(MeleeWeapon _target)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.weaponType == WeaponType.MELEE) //jeżeli gracz już posiada ten typ broni
            {
                Drop(weapon, transform);
                break;
            }
                
        }

        if (weapons.Count > 0) //to trzeba poprawić przy dodawaniu nowego typu broni
            weapons.Insert(1, _target);
        else
            weapons.Add(_target);
    }

    private void Target()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Vector2.Distance(target, transform.position) > targetDistance)
        {
            Camera.main.GetComponent<CameraController>().cameraMode = CameraMode.NORMAL;
            Camera.main.GetComponent<CameraController>().SetOffset(PointAtDistance(targetDistance)/2);
        }
        else
        {
            Camera.main.GetComponent<CameraController>().cameraMode = CameraMode.TARGETTING;
        }
        
        //zmiana kursora
    }

    public void Reload()
    {
        if (ammoInMagazines > 0 && currentAmountOfAmmo < ((RangeWeapon)weapons[indexOfCurrentWeapon]).maxAmmoInMagazine)
        {
            GetComponent<PlayerController>().body.GetComponent<Animator>().Play("Reload");
            GetComponent<AudioSource>().clip = ((RangeWeapon)weapons[indexOfCurrentWeapon]).reloadSound;
            GetComponent<AudioSource>().Play();

            if (((RangeWeapon)weapons[indexOfCurrentWeapon]).maxAmmoInMagazine - currentAmountOfAmmo > ammoInMagazines)
            {
                currentAmountOfAmmo += ammoInMagazines;
                ammoInMagazines = 0;
            }
            else
            {
                ammoInMagazines -= ((RangeWeapon)weapons[indexOfCurrentWeapon]).maxAmmoInMagazine - currentAmountOfAmmo;
                currentAmountOfAmmo = ((RangeWeapon)weapons[indexOfCurrentWeapon]).maxAmmoInMagazine;
            }
        }
    }

    public void Throw(Weapon _target, Vector2 _position)
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = target - new Vector2(_position.x, _position.y);
        direction.Normalize();
        GameObject weapon = Instantiate(_target.weaponPrefab, new Vector3(_position.x, _position.y, _target.weaponPrefab.transform.position.z), Quaternion.identity);
        Physics2D.IgnoreCollision(weapon.GetComponent<BoxCollider2D>(), GetComponent<Collider2D>());
        weapon.GetComponent<Rigidbody2D>().velocity = direction * _target.throwSpeed;

        if(_target.weaponType == WeaponType.RANGE)
        {
            weapon.GetComponent<GunObject>().ammoInMagazines = ammoInMagazines;
            weapon.GetComponent<GunObject>().currentAmountOfAmmo = currentAmountOfAmmo;
        }
        Vector2 lookAt = (target - _position).normalized;
        weapon.transform.up = lookAt;
        RemoveWeapon(_target);
    }

    public void Drop(Weapon _target, Transform _transform)
    {
        GameObject weapon = Instantiate(_target.weaponPrefab, new Vector3(_transform.position.x, _transform.position.y, _target.weaponPrefab.transform.position.z), Quaternion.identity);
        //weapon.GetComponent<Rigidbody2D>().AddForce(new Vector2(6, 6));

        if (_target.weaponType == WeaponType.RANGE) //jeżeli gracz upuścił broń dystansową to trzeba przekazać jej odpowiednie parametry
        {
            weapon.GetComponent<GunObject>().ammoInMagazines = ammoInMagazines;
            weapon.GetComponent<GunObject>().currentAmountOfAmmo = currentAmountOfAmmo;
        }
        RemoveWeapon(_target);
    }

}
