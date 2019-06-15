using UnityEngine;
using UnityEngine.UI;

 public class UIController : MonoBehaviour
{
    static public Image weaponImage;

    static public void SetWeapon(Sprite _weaponSprite)
    {
        if (_weaponSprite == null)
            weaponImage.enabled = false;
        else
        {
            if (!weaponImage.enabled)
                weaponImage.enabled = true;
            weaponImage.sprite = _weaponSprite;
        }
            
    }

}
