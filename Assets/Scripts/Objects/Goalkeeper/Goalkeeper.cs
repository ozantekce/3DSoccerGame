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
    private GameObject rightHand,catchArea;

    public GameObject RightHand { get => rightHand; set => rightHand = value; }


    public GoalkeeperJumpArea CenterUp { get => centerUp; set => centerUp = value; }
    public GoalkeeperJumpArea Center { get => center; set => center = value; }
    public GoalkeeperJumpArea CenterDown { get => centerDown; set => centerDown = value; }
   
    public GoalkeeperJumpArea RightDown { get => rightDown; set => rightDown = value; }
    public GoalkeeperJumpArea RightUp { get => rightUp; set => rightUp = value; }
    public GoalkeeperJumpArea LeftUp { get => leftUp; set => leftUp = value; }
    public GoalkeeperJumpArea LeftDown { get => leftDown; set => leftDown = value; }
    public GoalkeeperJumpArea Other { get => other; set => other = value; }
    public GameObject CatchArea { get => catchArea; set => catchArea = value; }
    public Transform GoalpostCenter { get => goalpostCenter; set => goalpostCenter = value; }
    public Transform GoalpostCenterRival { get => goalpostCenterRival; set => goalpostCenterRival = value; }

    private GoalkeeperJumpArea centerUp,center,centerDown, rightUp, rightDown,leftUp,leftDown,other;


    public void Start()
    {
        base.Start();

        Transform areas = transform.Find("Areas");
        CenterUp = areas.Find("CenterUp").gameObject.GetComponent<GoalkeeperJumpArea>();
        Center = areas.Find("Center").gameObject.GetComponent<GoalkeeperJumpArea>();
        CenterDown = areas.Find("CenterDown").gameObject.GetComponent<GoalkeeperJumpArea>();

        RightUp = areas.Find("RightUp").gameObject.GetComponent<GoalkeeperJumpArea>();
        RightDown = areas.Find("RightDown").gameObject.GetComponent<GoalkeeperJumpArea>();
        

        LeftUp = areas.Find("LeftUp").gameObject.GetComponent<GoalkeeperJumpArea>();
        LeftDown = areas.Find("LeftDown").gameObject.GetComponent<GoalkeeperJumpArea>();

        Other = areas.Find("Other").gameObject.GetComponent<GoalkeeperJumpArea>();

    }


    [SerializeField]
    private Transform goalpostCenter,goalpostCenterRival;
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

        Vector3 directionVector = (ball.transform.position - goalpostCenter.transform.position).normalized;

        Vector3 target = goalpostCenter.position + directionVector * radius;

        if (goalpostCenter.position.z > target.z)
            target.z = goalpostCenter.position.z;

        return target;

    }

    



}
