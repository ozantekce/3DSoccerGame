using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : Player, Jumpable
{
    public float JumpPowerY { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float JumpPowerX { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float JumpPowerZ { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public Cooldown JumpCooldown => throw new System.NotImplementedException();


    [SerializeField]
    private GameObject rightHand;

    public GameObject RightHand { get => rightHand; set => rightHand = value; }



    [SerializeField]
    private Transform center;
    public float radius = 9;


    public bool RightPosition()
    {

        Vector3 rightPosition = CalculateRightPosition();

        float distance = Vector3.Distance(rightPosition, transform.position);

        return distance<3f;

    }



    public Vector3 CalculateRightPosition()
    {

        Ball ball = Ball.Instance;

        Vector3 directionVector = (ball.transform.position - center.transform.position).normalized;

        Vector3 target = center.position + directionVector * radius;

        if (center.position.z > target.z)
            target.z = center.position.z;

        return target;

    }


}
