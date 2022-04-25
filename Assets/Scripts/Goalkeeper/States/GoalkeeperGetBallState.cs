using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperGetBallState : GoalkeeperState
{

    public static GoalkeeperGetBallState goalkeeperGetBallState = new GoalkeeperGetBallState();


    public override void EnterTheState(Goalkeeper goalkeeper)
    {

        goalkeeper.ChangeAnimation("Run");

    }

    public override void ExecuteTheState(Goalkeeper goalkeeper)
    {


        if (BallCaught(goalkeeper))
        {
            goalkeeper.ChangeCurrentState(GoalkeeperOverhandThrowState.goalkeeperOverhandThrowState);
        }
        else if (goalkeeper.CurrentAction != null)
        {
            //goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);

        }
        else if (IsThereBallInVision(goalkeeper))
        {
            //goalkeeper.ChangeCurrentState(GoalkeeperShootState.goalkeeperShootState);
            if(goalkeeper.CurrentAction == null)
            {
                goalkeeper.ChangeCurrentAction(new GetBallAction(goalkeeper, null));
                goalkeeper.StartCurrentAction();
            }
                

        }
        else if (DistanceBetweenGoalkeeperAndCenterSoFar(goalkeeper))
        {
            //goalkeeper.ChangeCurrentState(GoalkeeperGoWaitPositionState.goalkeeperGoWaitPositionState);
            GoIdleState(goalkeeper);
        }
        else
        {
            Spin(goalkeeper);
            MyMovePosition(goalkeeper, Ball.Instance.transform.position, goalkeeper.MovementSpeed);

        }


    }

    public override void ExitTheState(Goalkeeper goalkeeper)
    {


    }


    public class GetBallAction : GoalkeeperAction
    {

        public GetBallAction(Goalkeeper goalkeeper, GoalkeeperAction nextAction) : base(goalkeeper, nextAction, 300f, 2000f)
        {

        }


        protected override void Action_()
        {

            Goalkeeper.StartCoroutine(SendBallToHands(Goalkeeper,Ball.Instance.transform.position));

        }

        protected override void BeforeAction()
        {
            Goalkeeper.Rb.velocity = Vector3.zero;
            Goalkeeper.rightHand.DropBall = false;
            Goalkeeper.leftHand.DropBall = false;
            Ball.Instance.Rb.isKinematic = true;
            Goalkeeper.Rb.isKinematic = true;
            Goalkeeper.ChangeAnimation("Scoop");

        }



        protected override void AfterAction()
        {
            Goalkeeper.Rb.isKinematic = false;
        }
        private bool processing = false;

        IEnumerator SendBallToHands(Goalkeeper goalkeeper,Vector3 startPosition)
        {
            processing = true;
            //Debug.Log("start");
            Ball.Instance.Rb.velocity = Vector3.zero;
            Ball.Instance.Rb.angularVelocity = Vector3.zero;
            Ball.Instance.Rb.isKinematic = true;
            float T = 0;

            while (true)
            {
                
                T += Time.deltaTime;
                float duration = 0.3f;
                float t01 = T / duration;

                Vector3 A = startPosition;
                Vector3 B = goalkeeper.rightHand.transform.position;
                Vector3 pos = Vector3.Lerp(A, B, t01);

                Ball.Instance.transform.position = pos;

                if (t01 >= 1)
                {
                    Ball.Instance.Rb.isKinematic = false;
                    break;
                }

                yield return new WaitForEndOfFrame();

            }



            while (goalkeeper.CurrentState == GoalkeeperJumpState.goalkeeperJumpState)
            {

                yield return new WaitForEndOfFrame();

            }
            //Debug.Log("over");
            processing = false;

        }




    }






}
