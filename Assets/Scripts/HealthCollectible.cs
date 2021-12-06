using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    public GameObject chomps;
    
void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if(controller.health  < controller.maxHealth)
            {
                controller.ChangeHealth(1);
               
                Destroy(gameObject);

                	controller.PlaySound(collectedClip);
                    
            }
        }

        NEWDragon dController = other.GetComponent<NEWDragon>();

        if (dController != null)
        {
            //GameObject.Instantiate(GameObject.Find("Rotten"), transform.position, transform.rotation);
            //GetComponent<Renderer>(new Color(37f,255,193))
            //GetComponent<Renderer>().material.color = new Color(163, 255, 193); //C sharp
            
            Destroy(gameObject);

            //rottenObject.SetActive(true);
            //dController.PlaySound(rottenClip);
        }
    }
}
