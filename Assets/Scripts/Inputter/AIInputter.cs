using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputter : Inputter
{

    public GameObject rivalGoalpost;

    Footballer footballer;

    // Start is called before the first frame update
    void Start()
    {
        footballer = GetComponent<Footballer>();
    }


    int timer;
    // Update is called once per frame


    void Update()
    {
        base.Update();


        footballer.shotInput = 0f;


        if (footballer.BallVision.ControlBall())
        {
            Vector3 target = rivalGoalpost.transform.position - transform.position;
            target.Normalize();

            footballer.verticalInput = target.x;
            footballer.horizontalInput = target.z;
            float distance = Vector3.Distance(transform.position, rivalGoalpost.transform.position);
            if (distance < 40f)
            {
                footballer.shotInput = 1f;
                return;
            }



        }
        else if (!Ball.Instance.Rigidbody.isKinematic)
        {
            Vector3 target = Ball.Instance.transform.position - transform.position;
            target.Normalize();

            footballer.verticalInput = target.x;
            footballer.horizontalInput = target.z;

        }
        else
        {
            Vector3 target = Vector3.zero - transform.position;
            target.Normalize();

            footballer.verticalInput = target.x;
            footballer.horizontalInput = target.z;


        }

    }




    private void FixedUpdate()
    {





    }


    protected override bool DownButtonPressed()
    {
        return false;
    }

    protected override bool JumpButtonPressed()
    {
        return false;
    }

    protected override bool LeftButtonPressed()
    {
        return false;
    }

    protected override bool PassButtonPressed()
    {
        return false;
    }

    protected override bool RightButtonPressed()
    {
        return false;
    }

    protected override bool RunButtonPressed()
    {
        return false;
    }

    protected override bool ShotButtonPressed()
    {
        return false;
    }

    protected override bool SlideButtonPressed()
    {
        return false;
    }

    protected override bool UpButtonPressed()
    {
        return false;
    }




}
