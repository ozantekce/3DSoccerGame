using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{

    [SerializeField]
    private Player master;
    public bool IsActive { get; set; }

    private void OnTriggerStay(Collider other)
    {
        if (!IsActive)
            return;

        Fallable fallable = other.GetComponent<Fallable>();

        if (fallable!=null && !fallable.Equals(master)  && !fallable.IsFalling)
        {
            fallable.FallCommand = true;
        }

    }



}
