using System.Collections;
using UnityEngine;

public class SpiderMeleeDamageScript : MonoBehaviour
{
    public int damageAmount = 10;
    private bool flag = false;
    public PlayerHealth health;

    void Start()
    {
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name == "Capsule")
            {
                health = obj.GetComponent<PlayerHealth>();
            }
        }
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);
        foreach (Collider nearbyObj in hitColliders)
        {
            if (nearbyObj.CompareTag("Player"))
            {
                health.TakeDamage(damageAmount);
                Debug.Log("Player taking damage");
                break;
            }
        }
        
    }

    private IEnumerator CheckEveryFewSeconds()
    {
        yield return new WaitForSeconds(1);
        flag = false;
    }
}
