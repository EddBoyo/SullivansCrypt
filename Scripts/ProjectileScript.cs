using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //public GameObject proj;
    public float radius = 5;
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
    void Update()
    {
        Debug.Log("Is hitting");
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity);
        foreach(Collider nearbyObj in hitColliders){
            if(nearbyObj.tag == "Player" && flag == false){
                health.TakeDamage(10);
                Destroy(gameObject);
                break;
            }
        }
        
        Destroy(gameObject, 5);
    }
}
