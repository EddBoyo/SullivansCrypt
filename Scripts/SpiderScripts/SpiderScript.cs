using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SpiderScript : MonoBehaviour
{
    public int HP = 50;
    //public GameObject child;
    public Slider healthBar;
    /*public GameObject projectile;
    public Transform  projectilePoint;
    public void ShootProjectile()
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward, ForceMode.Impulse);
        //rb.AddForce(transform.up, ForceMode.Impulse);
    }*/
    void Update(){
        healthBar.value = HP;
    }
    public void takeDamage(int damageAmount){
        HP -= damageAmount;
        if(HP<=0){
            GetComponent<Animator>().SetTrigger("IsDead");
            StartCoroutine(CheckEveryFewSeconds());

        }else{
            //Plays take damage animation
            GetComponent<Animator>().SetTrigger("IsHit");
        }
    }

    private IEnumerator CheckEveryFewSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
