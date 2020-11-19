using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public Text Ammo;
    public Gun GunScript;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        Ammo.text = GunScript.CurrentAmmo + " / " + GunScript.MaxAmmo;
    }
}
