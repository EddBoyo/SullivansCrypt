using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageScript : MonoBehaviour
{
    //public GameObject proj;
    public float radius = 0.7f;
    public int damageAmount = 10;
    private void OnCollisionEnter(Collision collision)
    {
        
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, Quaternion.identity);
        foreach(Collider nearbyObj in hitColliders){
            if(nearbyObj.tag == "Player"){

                //Debug.Log("taking Damage");
                break;
            }
        }
        
        //Destroy(gameObject, 5);
    }
}
