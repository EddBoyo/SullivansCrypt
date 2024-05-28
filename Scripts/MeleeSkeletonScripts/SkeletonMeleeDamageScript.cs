using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMeleeDamageScript : MonoBehaviour
{
   public int damageAmount = 10;
   bool flag = false;
   public PlayerHealth health;
   public void Start(){
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            if (obj.name == "Capsule")
            {
                health = obj.GetComponent<PlayerHealth>();
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity);
        foreach(Collider nearbyObj in hitColliders){
            if(nearbyObj.tag == "Player" && !flag){
                Debug.LogWarning("PlayerSkeletonCollision");
                flag = true;
                StartCoroutine(CheckEveryFewSeconds());
                health.TakeDamage(10);
                Debug.Log("taking Damage");
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
