using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperDesicionTree
{

    public Goalkeeper goalkeeper;

    private Node root = GoalkeeperIsJumpingNode.goalkeeperIsJumpingNode;


    public void Execute()
    {

        Node currentNode = root.Decision(goalkeeper);

        while(currentNode != null)
        {   
            currentNode = currentNode.Decision(goalkeeper);
        }
        

    }



    public abstract class Node
    {

        public Node yesNode;
        public Node noNode;
        public Goalkeeper goalkeeper;

        public Node Decision(Goalkeeper goalkeeper)
        {
            this.goalkeeper = goalkeeper;
            if (Decision_())
            {
                return yesNode;
            }
            else
            {
                return noNode;
            }

        }

        protected abstract bool Decision_();

    }


    public class GoalkeeperIsJumpingNode : Node
    {
        public static GoalkeeperIsJumpingNode goalkeeperIsJumpingNode = new GoalkeeperIsJumpingNode();

        // eðer zýplama devam ediyorsa bir þey yapmaz
        // etmiyorsa top kontrolünde mi noduna gider
        private GoalkeeperIsJumpingNode()
        {
            yesNode = null; //do nothing
            noNode = GoalkeeperIsControllingBallNode.goalkeeperIsControllingBallNode;
        }

        protected override bool Decision_()
        {
            return goalkeeper.CurrentState == GoalkeeperJumpState.goalkeeperJumpState;
        }

    }

    public class GoalkeeperIsControllingBallNode : Node
    {
        public static GoalkeeperIsControllingBallNode goalkeeperIsControllingBallNode = new GoalkeeperIsControllingBallNode();

        // top kontrolünde ise topa vurmak için bir sonraki noda gider
        // deðil ise topa vuruldu mu noduna gider
        private GoalkeeperIsControllingBallNode()
        {
            yesNode = SetShootValueNode.setShootValueNode;// shoot
            noNode = BallIsHittedNode.ballIsHittedNode;
        }

        protected override bool Decision_()
        {
            return goalkeeper.BallVision.IsThereBallInVision();
        }
    }

    public class BallIsHittedNode : Node
    {
        public static BallIsHittedNode ballIsHittedNode = new BallIsHittedNode();

        private BallIsHittedNode()
        {
            yesNode = BallIsCloseToGoalkeeperNode.ballIsCloseToGoalkeeperNode;
            noNode = BallIsCloseToGoalkeeperWaitPositionNode.ballIsCloseToGoalkeeperWaitPositionNode;
        }

        protected override bool Decision_()
        {
            return Ball.Instance.GetVelocity().magnitude > 25f;
        }
    }

    public class BallIsCloseToGoalkeeperNode : Node
    {
        public static BallIsCloseToGoalkeeperNode ballIsCloseToGoalkeeperNode = new BallIsCloseToGoalkeeperNode();
        private BallIsCloseToGoalkeeperNode()
        {
            yesNode = null;//do nothing
            noNode = SetJumpValueNode.setJumpValueNode;//jump
        }

        protected override bool Decision_()
        {
            Debug.Log("dis : "+ Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position));
            return Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position) > 35f;
        }
    }

    public class BallIsCloseToGoalkeeperWaitPositionNode : Node
    {
        public static BallIsCloseToGoalkeeperWaitPositionNode ballIsCloseToGoalkeeperWaitPositionNode = new BallIsCloseToGoalkeeperWaitPositionNode();

        private BallIsCloseToGoalkeeperWaitPositionNode()
        {
            yesNode = null;//Run To Ball
            noNode = GoalkeeperInWaitPosition.goalkeeperInWaitPosition;
        }

        protected override bool Decision_()
        {
            return Vector3.Distance(Ball.Instance.transform.position, goalkeeper.transform.position) < 5f;
        }
    }


    public class GoalkeeperInWaitPosition : Node
    {
        public static GoalkeeperInWaitPosition goalkeeperInWaitPosition = new GoalkeeperInWaitPosition();

        private GoalkeeperInWaitPosition()
        {
            yesNode = null;//wait
            noNode = null;// go to wait position
        }

        protected override bool Decision_()
        {
            return Vector3.Distance(goalkeeper.WaitPosition, goalkeeper.transform.position) < 1f;
        }
    }





    // Action nodes


    public class SetShootValueNode : Node
    {
        public static SetShootValueNode setShootValueNode = new SetShootValueNode();

        private SetShootValueNode()
        {

            

        }

        protected override bool Decision_()
        {

            goalkeeper.Inputter.SetButtonShootValue(1);
            return false;
        }
    }

    public class SetJumpValueNode : Node
    {
        public static SetJumpValueNode setJumpValueNode = new SetJumpValueNode();

        private SetJumpValueNode()
        {

            

        }

        protected override bool Decision_()
        {
            
            goalkeeper.Inputter.SetButtonJumpValue(1);

            float distanceX = goalkeeper.transform.position.x - Ball.Instance.transform.position.x;

            if(distanceX > 0.2f)
            {

                goalkeeper.Inputter.SetVerticalValue(1);

            }
            else if(distanceX < -0.2f)
            {
                goalkeeper.Inputter.SetVerticalValue(-1);

            }

            return false;
        }
    }


    /*
    public class SetHorizontalValueNode : Node
    {
        public static SetHorizontalValueNode pressLeftButtonNode = new SetHorizontalValueNode();

        private SetHorizontalValueNode()
        {

            

        }

        protected override bool Decision_()
        {
            return false;
        }

    }

    public class SetVerticalValueNode : Node
    {
        public static SetVerticalValueNode pressRightButtonNode = new SetVerticalValueNode();

        private SetVerticalValueNode()
        {



        }

        protected override bool Decision_()
        {
            return false;
        }

    }

    */
    


}
