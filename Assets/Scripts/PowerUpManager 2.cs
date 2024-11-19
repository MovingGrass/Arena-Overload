using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager2 : MonoBehaviour
{
    // Start is called before the first frame update
    public DefaultGun2 defaultGun;
    public List<MonoBehaviour> powerUpScripts;
    public List<GameObject> powerUpUIImages;
    private IPowerUp currentPowerUp;
    public Transform canvasTransform; // Referensi ke Canvas
    public Vector3 powerUpImagePosition = new Vector3(0, 0, 0); 
    private Coroutine powerUpCoroutine;
    private GameObject activeUIInstance; // Instance Image yang aktif
    private Image activeImageFill;

    private void Start()
    {
        canvasTransform = GameObject.Find("Canvas").transform;
        // Activate default gun at start
        defaultGun.Activate();
    }

    public void ActivatePowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        // Deactivate current weapon (either default gun or current power-up)
        if (currentPowerUp != null)
        {
            currentPowerUp.Deactivate();
            if (activeUIInstance != null)
            {
                Destroy(activeUIInstance); // Hapus UI power-up sebelumnya
            }
        }
        else
        {
            defaultGun.Deactivate();
        }

        // Randomly select and activate a new power-up
        int randomIndex = Random.Range(0, powerUpScripts.Count);
        currentPowerUp = powerUpScripts[randomIndex] as IPowerUp;
        
        if (currentPowerUp != null)
        {
            currentPowerUp.Activate();
            powerUpCoroutine = StartCoroutine(PowerUpTimer(currentPowerUp.Duration));
             if (powerUpUIImages[randomIndex] != null)
            {
                activeUIInstance = Instantiate(powerUpUIImages[randomIndex],  canvasTransform); // Spesifik posisi UI bisa diatur sesuai kebutuhan
                activeImageFill = activeUIInstance.GetComponentInChildren<Image>(); // Ambil komponen Image untuk fill
                activeImageFill.fillAmount = 1; // Mulai dengan fill penuh

                RectTransform imageRectTransform = activeUIInstance.GetComponent<RectTransform>();
                if (imageRectTransform != null)
                {
                    imageRectTransform.anchoredPosition = powerUpImagePosition; // Atur posisi dalam Canvas
                }
            }
        }
        else
        {
            Debug.LogError("Selected power-up does not implement IPowerUp interface");
        }
    }

    private IEnumerator PowerUpTimer(float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (activeImageFill != null)
            {
                activeImageFill.fillAmount = 1 - (elapsedTime / duration); // Hitung sisa waktu
            }
            yield return null; // Tunggu satu frame
        }

        // Deactivate power-up and reactivate default gun
        currentPowerUp.Deactivate();
        defaultGun.Activate();
        currentPowerUp = null;

        if (activeUIInstance != null)
        {
            Destroy(activeUIInstance);
        }
    }
}
