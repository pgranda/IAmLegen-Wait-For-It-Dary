using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera WeaponCamera;
    public ParticleSystem MuzzleFlash;
    public GameObject EnemyHitPrefab;
    public GameObject HitPrefab;
    public Animator animator;
    public AudioClip FireSound;
    public AudioClip ReloadSound;
    public AudioSource AudioSource;

    public bool isReloading = false;

    public int CurrentAmmo = 30;
    public int MagCapacity = 30;
    public int AmmoLeft = 30;
    public float FireRate = 12f;
    public float Range = 100f;
    public float impactForce = 10f;

    private float nextTimeToFire = 0f;
    
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.R) || CurrentAmmo == 0) && AmmoLeft > 0 && CurrentAmmo != MagCapacity)
        {
            Reload();
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && CurrentAmmo > 0 && isReloading == false)
        {
            nextTimeToFire = Time.time + 1f / FireRate;
            Shoot();
        }
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }

        StartCoroutine(Reload_Coroutine());

    }

    public void Shoot()
    {
        animator.SetTrigger("Shooting");
        AudioSource.Play();
        
        MuzzleFlash.Play();
        if (CurrentAmmo > 0)
        {
            CurrentAmmo--;
        }
        else
        {
            CurrentAmmo = 0;
        }

        RaycastHit hit;
        if (Physics.Raycast(WeaponCamera.transform.position, WeaponCamera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(50f);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.transform.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(EnemyHitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO,5f);
            }
            else
            {
                GameObject impactGO = Instantiate(HitPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 5f);
            }
        }
    }

    private IEnumerator Reload_Coroutine()
    {
        AudioSource.clip = ReloadSound;
        AudioSource.Play();
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);

        animator.SetBool("Reloading", false);

        UpdateBullets();

        isReloading = false;
        AudioSource.clip = FireSound;
    }

    private void UpdateBullets()
    {
        var ammoToLoad = MagCapacity - CurrentAmmo;
        if (AmmoLeft >= ammoToLoad)
        {
            CurrentAmmo += ammoToLoad;
            AmmoLeft -= ammoToLoad;
        }
        else
        {
            CurrentAmmo += AmmoLeft;
            AmmoLeft = 0;
        }
    }
}
