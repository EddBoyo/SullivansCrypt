using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SkeletonScript : MonoBehaviour
{
    private int HP = 75;
    public GameObject child;
    public Slider healthBar;
    public GameObject projectile;
    public Transform projectilePoint;
    bool cooldown = false;
    public void ShootProjectile()
    {
        if(!cooldown){
            Vector3 temp = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            //projectilePoint.transform.Rotate(temp);
            //Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            GameObject arrow = Instantiate(projectile);
            arrow.transform.position = projectilePoint.position;
            arrow.GetComponent<Rigidbody>().velocity = projectilePoint.forward*10f;
            //SetDirection();
            //rb.AddForce(transform.forward*2000.0f);
            cooldown = true;
            StartCoroutine(ProjCoolDown());
        }
    }
    private IEnumerator ProjCoolDown()
    {
        yield return new WaitForSeconds(2);
        cooldown = false;
    }
    private void SetDirection()
    {
        if (GetComponent<Rigidbody>().velocity.z > 0.5f)
            transform.forward = GetComponent<Rigidbody>().velocity;
    }
    void Update(){
        healthBar.value = HP;
    }
    public void takeDamage(int damageAmount){
        HP -= damageAmount;
        if(HP<=0){
            AudioManager.instance.PlaySFX("SkeletonDeath");
            GetComponent<Animator>().SetTrigger("IsDead");
            StartCoroutine(CheckEveryFewSeconds());

        }else{
            //Plays take damage animation
            AudioManager.instance.PlaySFX("SkeletonHit");
            GetComponent<Animator>().SetTrigger("IsHit");
        }
    }

    private IEnumerator CheckEveryFewSeconds()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}
