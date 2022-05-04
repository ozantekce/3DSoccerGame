using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{


    public static void Jump_(Jumpable jumpable, Vector3 jumpVector)
    {

        if (!jumpable.JumpCooldown.Ready())
        {
            return;
        }

        jumpable.Rigidbody.velocity = Vector3.zero;
        jumpable.Rigidbody.velocity = jumpVector;

    }

}
