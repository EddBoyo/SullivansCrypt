using UnityEngine;

public class SoundSelector : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource audioSource;

    private void Start()
    {
        // Make sure there are at least 3 sounds in the array
        if (sounds.Length < 3)
        {
            Debug.LogError("Please assign at least 3 audio clips to the sounds array.");
            return;
        }
    }

    public void PlayRandomSound()
    {
        // Generate a random index between 0 and 2
        int randomIndex = Random.Range(0, 3);

        // Play the selected sound over the audio source
        audioSource.clip = sounds[randomIndex];
        audioSource.Play();
    }
}
