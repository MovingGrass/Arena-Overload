using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public DefaultGun1 defaultGun;
    public List<MonoBehaviour> powerUpScripts;
    private IPowerUp currentPowerUp;
    private Coroutine powerUpCoroutine;

    private void Start()
    {
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
        }
        else
        {
            Debug.LogError("Selected power-up does not implement IPowerUp interface");
        }
    }

    private IEnumerator PowerUpTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        // Deactivate power-up and reactivate default gun
        currentPowerUp.Deactivate();
        defaultGun.Activate();
        currentPowerUp = null;
    }
}

