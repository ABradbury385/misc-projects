using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    //damage Ruby when she enters damage area and if she stays there
    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController controller = collision.GetComponent<RubyController>();
        if(collision != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
