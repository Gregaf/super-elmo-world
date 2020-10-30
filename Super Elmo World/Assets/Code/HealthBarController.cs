using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public float healthbarChangeTime = 0.25f;

    public PlayerController player;
    // Later should have it udate to where GameManager has a dictionary of each player stored.
    private HealthHandler respectiveHealth;
    [SerializeField] private Image[] healthImages = new Image[2];

    private void Start()
    {
        respectiveHealth = player.Health_Handler;

        

        respectiveHealth.OnHealthLoss += UpdateHealthBar;

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        respectiveHealth.OnHealthLoss -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        float healthbarValue = respectiveHealth.health;

        if (respectiveHealth.health != 0)
        {
            healthbarValue = respectiveHealth.health / respectiveHealth.maxHealth;
        }

        StartCoroutine(Fade(0, 1, healthbarValue));

    }
    IEnumerator Fade(float start, float end, float healthVal)
    {
        healthImages[0].CrossFadeAlpha(end, 0.25f, false);
        healthImages[1].CrossFadeAlpha(end, 0.25f, false);
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(DecreaseHealthSmooth(healthImages[0].fillAmount, healthVal));
    }



    IEnumerator DecreaseHealthSmooth(float currentHealth, float targetHealth)
    {
        float time = 0;

        while (!Mathf.Approximately(healthImages[0].fillAmount, targetHealth))
        {
            time += Time.deltaTime;

            healthImages[0].fillAmount = Mathf.Lerp(currentHealth, targetHealth, time / healthbarChangeTime);

            yield return null;
        }

        healthImages[0].CrossFadeAlpha(0, 0.25f, false);
        healthImages[1].CrossFadeAlpha(0, 0.25f, false);
        yield return null;
    }

}
