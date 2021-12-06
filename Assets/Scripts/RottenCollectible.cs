using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RottenCollectible : MonoBehaviour
{
    //public AudioClip collectedClip;
    public AudioClip slugClip;
    public GameObject slugObject;

    

    //public GameObject slugParticles;

 

    void Start()
    {
        //rottenObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if(controller.health  < controller.maxHealth)
            {
                
                controller.speed = 2;
                controller.ChangeHealth(3);

                //controller.ChangeHealth(4);
                controller.transform.position = new Vector2(13.29f, 5.22f);
                //isInvincible = true;
                //invincibleTimer = timeInvincible;
                //controller.slugObject = Instantiate(slugParticles, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
                Destroy(gameObject);


                controller.PlaySound(slugClip);
                    
            }
        }

        //NEWDragon dController = other.GetComponent<NEWDragon>();

        //if (dController != null)
        //{
            //rottenObject.SetActive(true);
            //dController.PlaySound(rottenClip);
        //}
    }
}