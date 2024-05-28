using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RedDragon : MonoBehaviour
{
    private int HP = 100;
    public GameObject child;
    public Slider healthBar;
    public GameObject projectile;
    public Transform  projectilePoint;
    bool cooldown = false;
    public void ShootProjectile()
    {
        
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward*0.5f, ForceMode.Impulse);
        
        //rb.AddForce(transform.up, ForceMode.Impulse);
    }
    private IEnumerator ProjCoolDown()
    {
        yield return new WaitForSeconds(2);
        cooldown = false;
    }
    void Update(){
        healthBar.value = HP;
    }
    public void takeDamage(int damageAmount){
        HP -= damageAmount;
        if(HP<=0){
            AudioManager.instance.PlaySFX("DragonDeath");
            GetComponent<Animator>().SetTrigger("isDead");
            StartCoroutine(CheckEveryFewSeconds());

        }else{
            //Plays take damage animation
            AudioManager.instance.PlaySFX("DragonHit");
            GetComponent<Animator>().SetTrigger("isHit");
        }
    }

    private IEnumerator CheckEveryFewSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }

}
