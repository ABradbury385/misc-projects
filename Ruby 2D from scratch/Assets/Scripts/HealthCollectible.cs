using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        RubyController controller = otherCollider.GetComponent<RubyController>();
        if (controller.Health < controller.maxHealth)
        {
            if (controller != null)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);
                controller.PlaySound(collectedClip);
            }
        }
    }

}
