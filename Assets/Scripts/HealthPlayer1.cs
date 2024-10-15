using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthPlayer1 : MonoBehaviour
{

    private float health;
    private float lerpTimer;
    [SerializeField] private GameObject FloatingText;
    [SerializeField] private float maxhealth = 100f;
    [SerializeField] private float chipSpeed = 2f;
    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    [SerializeField] private Vector3 offset = new Vector3(0, 3, 0);

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxhealth);
        UpdateHealthUIplayer1();
    }

    public void UpdateHealthUIplayer1()
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

    public void TakeDamagePlayer1(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        ShowFloatingText();
    }

    public void HealPlayer1(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }

    void ShowFloatingText()
    {
        var go = Instantiate(FloatingText, transform.position + offset, Quaternion.identity, transform);
        go.GetComponent<TMP_Text>().text = health.ToString(); 
    }


}
