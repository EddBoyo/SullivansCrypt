using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;                     // The maximum health of the player
    public int currentHealth = 100;                       // The current health of the player

    public float damageCooldownSeconds = 2.0f;  // public cooldown duration

    private float lastDamageTime = -Mathf.Infinity; // track the last time the player took damage


    public AudioClip[] sounds;
    public AudioSource audioSource;

    public Camera camera;
    public PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private ColorGrading color_grading;

    private void Start()
    {
        currentHealth = maxHealth;                  // Set the initial health to maximum

        vignette = postProcessingVolume.profile.GetSetting<Vignette>();
        color_grading = postProcessingVolume.profile.GetSetting<ColorGrading>();

        UpdateHealthFilter();                       // Update the health UI and effects

        if (sounds.Length < 3)
        {
            Debug.LogError("Please assign at least 3 audio clips to the sounds array.");
            return;
        }
    }

    bool flag = false;
    public void Update(){
        if(flag == false && currentHealth < 90){

            flag = true;
            StartCoroutine(healing());

        }
    }

    IEnumerator healing()
    {
        // This will execute immediately
        Debug.Log("Coroutine started!");

        // Wait for 3 seconds
        yield return new WaitForSeconds(5.0f);

        // This will execute after the 3 seconds
        Heal(10);
        flag = false;
    }

    public void TakeDamage(int damageAmount)
    {
        // Check if cooldown has passed since last damage
        if (Time.time - lastDamageTime >= damageCooldownSeconds)
        {
            Debug.Log("Damage");
            currentHealth -= damageAmount;
            lastDamageTime = Time.time;  // update the last time the player took damage

            UpdateHealthFilter();
            StartCoroutine(FlashVignette(Color.red, 0.2f)); // Flash vignette effect in red

            // Generate a random index between 0 and 2 (inclusive) to select a sound
            int randomIndex = Random.Range(0, 3);

            // Play the selected sound over the audio source
            switch (randomIndex)
            {
                case 0:
                    AudioManager.instance.PlaySFX("DamageGrunt1");
                    break;
                case 1:
                    AudioManager.instance.PlaySFX("DamageGrunt2");
                    break;
                case 2: 
                    AudioManager.instance.PlaySFX("DamageGrunt3");
                    break;
                default:
                    AudioManager.instance.PlaySFX("DamageGrunt1");
                    break;
            }
            /*
            audioSource.clip = sounds[randomIndex];
            audioSource.Play();
            */
        }

        // Ensure health doesn't drop below zero
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }




    public void Heal(int healAmount)
    {
        currentHealth += healAmount;                // Add the heal amount to current health

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;              // Ensure health does not exceed the maximum

        UpdateHealthFilter();                       // Update the health UI and effects after healing
    }

    private void UpdateHealthFilter()
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        // Adjust color filters for low health
        if (healthPercentage < 0.5)
        {
            vignette.color.value = Color.black;
            vignette.intensity.value = Mathf.Lerp(0.0f, 1.0f, 1.0f - (healthPercentage / 0.5f));
            color_grading.colorFilter.value.r = Mathf.Lerp(0.0f, 1.0f, (healthPercentage / 0.5f));
            color_grading.colorFilter.value.g = Mathf.Lerp(0.0f, 1.0f, (healthPercentage / 0.5f));
            color_grading.colorFilter.value.b = Mathf.Lerp(0.0f, 1.0f, (healthPercentage / 0.5f));
            if (healthPercentage <= 0)
            {
                Debug.LogWarning("death?");
                AudioManager.instance.musicSource.Stop();
                SceneManager.LoadSceneAsync("GameOver");
            }
        }
        else{
            vignette.intensity.value = 0;
            color_grading.colorFilter.value.r = 1.0f;
            color_grading.colorFilter.value.g = 1.0f;
            color_grading.colorFilter.value.b = 1.0f;
            
        }
    }

    private IEnumerator FlashVignette(Color color, float fadingDuration)
    {
        //adjust color filters for damage taken
        float def_color = vignette.color.value.r;
        float def_vig = vignette.intensity.value;

        float def_color_grade_color_r = color_grading.colorFilter.value.r;
        float def_color_grade_color_g = color_grading.colorFilter.value.g;
        float def_color_grade_color_b = color_grading.colorFilter.value.b;

        float startTime = Time.time;

        vignette.intensity.value = 0.5f;
        vignette.color.value.r = 1.0f;

        //fade effects slowly
        startTime = Time.time;
        while (Time.time < startTime + fadingDuration)
        {
            float t = (Time.time - startTime) / fadingDuration;

            //start at 1.0f and fade back to current intensity
            vignette.intensity.value = Mathf.Lerp(1.0f, def_vig, t);
            vignette.color.value.r = Mathf.Lerp(1.0f, def_color, t);

            //start at 1.0f and fade back to current intensity
            //uses g and b because we want a red effect
            color_grading.colorFilter.value.g = Mathf.Lerp(def_color_grade_color_g - 0.5f, def_color_grade_color_g, t);
            color_grading.colorFilter.value.b = Mathf.Lerp(def_color_grade_color_b - 0.5f, def_color_grade_color_b, t);

            yield return null;
        }

        //ensure everything is back to where it was before
        vignette.intensity.value = def_vig;
        vignette.color.value.r = def_color;

        color_grading.colorFilter.value.r = def_color_grade_color_r;
        color_grading.colorFilter.value.g = def_color_grade_color_g;
        color_grading.colorFilter.value.b = def_color_grade_color_b;
    }

}


