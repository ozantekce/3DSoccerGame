using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperJumpArea : MonoBehaviour
{

    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }


    public bool IntersectWithMeetingPosition(Vector3 meetingPosition)
    {

        Collider[] intersecting = Physics.OverlapSphere(meetingPosition, 0.5f);

        foreach (var intersect in intersecting)
        {
            if(intersect == collider)
            {
                return true;
            }
        }

        return false;

    }



}
