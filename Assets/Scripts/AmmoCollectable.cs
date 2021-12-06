using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.ChangeCogs(controller.cogCount+4);
            Destroy(gameObject);
            controller.PlaySound(collectedClip);
                    
            
        }
    }
}
