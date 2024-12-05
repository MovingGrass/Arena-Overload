using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.VFX;
public class RapidFire : MonoBehaviour, IPowerUp
{
    public GameObject bulletPrefab;
    public Transform shootingPoint1;
    public float fireRate = 0.05f;
    private float nextFireTime = 0f;

    public VisualEffect muzzleFlashVFX;
    
    // Optional: VFX lifetime duration
    public float vfxDuration = 0.1f;

    public float Duration => 10f; // Set the duration for this power-up

    void Start ()
    {
        if (muzzleFlashVFX == null)
        {
            Debug.LogWarning("Muzzle Flash VFX not assigned to " + gameObject.name);
        }
    }
    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
        // Stop any playing VFX when weapon is deactivated
        if (muzzleFlashVFX != null)
        {
            muzzleFlashVFX.Stop();
        }
    }

    void Update()
    {
        bool firePressed = Input.GetKey(KeyCode.E) || 
                           Input.GetButton("Fire1");

        if (firePressed && Time.time >= nextFireTime)
        {
            ShootBullet();
            PlayShootingVFX();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint1.position, shootingPoint1.rotation);
    }

    void PlayShootingVFX()
    {
        if (muzzleFlashVFX != null)
        {
            // Play the VFX
            muzzleFlashVFX.Play();
            
            // Optional: Stop the VFX after duration
            StartCoroutine(StopVFXAfterDuration(muzzleFlashVFX, vfxDuration));
        }
    }

    IEnumerator StopVFXAfterDuration(VisualEffect vfx, float duration)
    {
        yield return new WaitForSeconds(duration);
        vfx.Stop();
    }
}