using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlueDragon : MonoBehaviour
{
    private int HP = 100;
    public GameObject child;
    public Slider healthBar;
    public GameObject projectile;
    //public GameObject hitSquare;
    public Transform  projectilePoint;
    public void ShootProjectile()
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*0.5f, ForceMode.Impulse);
        //rb.AddForce(transform.up, ForceMode.Impulse);
    }
    void Update(){
        healthBar.value = HP;
    }
    public void takeDamage(int damageAmount){
        HP -= damageAmount;
        if(HP<=0){
            //Plays the death animation
            GetComponent<Animator>().SetTrigger("isDead");
            //GetComponent<HaveIAlreadyBeenDefeated>().setAsDefeated();
            child.GetComponent<MeshCollider>().enabled = false;
            Destroy(gameObject);

        }else{
            //Plays take damage animation
            GetComponent<Animator>().SetTrigger("isHit");
        }
    }
    
}
