using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class DefaultGun2 : MonoBehaviour, IWeapon 
{
    public GameObject bulletPrefab;
    public Transform shootingPoint2;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    // VFX components
    public VisualEffect muzzleFlashVFX;
    
    // Optional: VFX lifetime duration
    public float vfxDuration = 0.1f;

    // Audio components
    [Header("Audio Settings")]
    private AudioSource audioSource;
    public AudioClip shootingSound;
    [Range(0f, 1f)]
    public float shootVolume = 1f;

    private void Start()
    {
        // Ensure VFX component is assigned
        if (muzzleFlashVFX == null)
        {
            Debug.LogWarning("Muzzle Flash VFX not assigned to " + gameObject.name);
        }

        // Get or Add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configure AudioSource settings
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 1 = fully 3D sound
        audioSource.volume = shootVolume;
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
        // Check for keyboard or joystick input
        bool firePressed = Input.GetKey(KeyCode.Mouse0) || 
                           Input.GetButton("Fire2");

        if (firePressed && Time.time >= nextFireTime)
        {
            ShootBullet();
            PlayShootingVFX();
            PlayShootingSound();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint2.position, shootingPoint2.rotation);
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

    void PlayShootingSound()
    {
        if (shootingSound != null && audioSource != null)
        {
            audioSource.clip = shootingSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Shooting sound or AudioSource not assigned to " + gameObject.name);
        }
    }

    IEnumerator StopVFXAfterDuration(VisualEffect vfx, float duration)
    {
        yield return new WaitForSeconds(duration);
        vfx.Stop();
    }
}