using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float Damage = 15f;
    public float Range = 100f;
    public float FireRate = 15;
    public float ImpactForce = 30f;

    public int MaxAmmo = 30;
    public int CurrentAmmo;
    public float ReloadTime = 5f;
    private bool Reloading = false;
    public Animator animator;

    public Camera FPSCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;

    private float NextTimeToFire = 0f;

    // Update is called once per frame

    private void Start()
    {
        CurrentAmmo = MaxAmmo;
    }

    private void OnEnable()
    {
        Reloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if (Reloading)
            return;

        if (CurrentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return; 
        }

        if (Input.GetKeyDown(KeyCode.R) && CurrentAmmo < MaxAmmo)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
        {
            NextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        Reloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(ReloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        CurrentAmmo = MaxAmmo;
        Reloading = false;
    }
    void Shoot()
    {
        MuzzleFlash.Play();

        CurrentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(FPSCam.transform.position, FPSCam.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            target target = hit.transform.GetComponent<target>();
            if (target != null)
            {
                target.TakeDamage(Damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            GameObject ImpactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGO, 2f);
        }
    }
}
