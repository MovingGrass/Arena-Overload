using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField] private MonoBehaviour[] scriptsToManage;

    [SerializeField] private GameObject Player2Panel;
    [SerializeField] private GameObject CanvasUI;

    // Tambahan Animator
    [SerializeField] private Animator animator;

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

        // Atur animasi mati berdasarkan kesehatan
        if (health <= 0)
        {
            animator.SetBool("IsDead", true);
        }
    }

    public void UpdateHealthUIplayer1()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hfraction = health / maxhealth;
        if (fillB > hfraction)
        {
            frontHealthBar.fillAmount = hfraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hfraction, percentComplete);
        }

        if (fillF < hfraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hfraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }

        if (health <= 0)
        {
            die();
        }
    }

    public void TakeDamagePlayer1(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        ShowFloatingText(damage);
    }

    public void HealPlayer1(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }

    void ShowFloatingText(float damage)
    {
        var go = Instantiate(FloatingText, transform.position + offset, Quaternion.identity, transform);
        go.GetComponent<TMP_Text>().text = damage.ToString();
    }

    public void die()
    {
        foreach (MonoBehaviour script in scriptsToManage)
        {
            if (script != null)
            {
                script.enabled = false;
            }
            else
            {
                Debug.LogWarning("A null script was found in the array and skipped.");
            }
        }

        // Pastikan animasi mati hanya dimainkan sekali
        if (animator != null && !animator.GetBool("IsDead"))
        {
            animator.SetBool("IsDead", true);
        }

        StartCoroutine(diePanel());
    }

    IEnumerator diePanel()
    {
        yield return new WaitForSeconds(2f);
        Player2Panel.SetActive(true);
        CanvasUI.SetActive(false);
    }
}
