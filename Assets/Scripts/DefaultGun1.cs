using UnityEngine;
using UnityEngine.VFX;
using System.Collections.Generic;
using System.Collections;

public class DefaultGun1 : MonoBehaviour, IWeapon
{
    public GameObject bulletPrefab;
    public Transform shootingPoint1;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    // VFX components
    public VisualEffect muzzleFlashVFX;


    // Optional: VFX lifetime duration
    public float vfxDuration = 0.1f;

    private void Start()
    {
        // Ensure VFX component is assigned
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
        if (Input.GetKey(KeyCode.E) && Time.time >= nextFireTime)
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