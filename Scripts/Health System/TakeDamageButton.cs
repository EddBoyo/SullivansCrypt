using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageButton : MonoBehaviour
{
    public int damageAmount = 1000;  // The amount of damage to inflict
    public GameObject player; 

    void Start(){
        player = GameObject.Find("Capsule");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DamageOverTime());
        }
    }

    IEnumerator DamageOverTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            elapsedTime += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}

