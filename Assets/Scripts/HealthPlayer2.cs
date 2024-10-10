using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthPlayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    private float health;
    private float lerpTimer;
    [SerializeField] private GameObject FloatingText;
    [SerializeField] private float maxhealth = 100f;
    [SerializeField] private float chipSpeed = 2f;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxhealth);
        UpdateHealthUIplayer2();
    }

    public void UpdateHealthUIplayer2()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hfraction = health / maxhealth;
        if(fillB > hfraction)
        {
            frontHealthBar.fillAmount = hfraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hfraction, percentComplete);
        }

        if(fillF < hfraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hfraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);

        }
    }

    public void TakeDamagePlayer2(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        ShowFloatingText(damage);
    }

    public void HealPlayer2(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }

    void ShowFloatingText(float damage)
    {
        var go = Instantiate(FloatingText, transform.position + new Vector3(9f, 0.5f, 0f), Quaternion.identity, transform);
        go.GetComponent<TMP_Text>().text = damage.ToString();
    }
}
