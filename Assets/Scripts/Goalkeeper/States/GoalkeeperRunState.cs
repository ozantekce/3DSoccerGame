using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperRunState : GoalkeeperState
{

    public static GoalkeeperRunState goalkeeperRunState = new GoalkeeperRunState();
    private State currentState = State.withoutBall;
    private enum State
    {
        withBall, withoutBall
    }

    public void EnterTheState(Goalkeeper goalkeeper)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("enter : RunningState_withoutBall");
            goalkeeper.ChangeAnimation("Run");
        }
        else
        {
            //Debug.Log("enter : RunningState_withBall");
            goalkeeper.ChangeAnimation("Run");
        }


    }

    public void ExitTheState(Goalkeeper goalkeeper)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("exit : RunningState_withoutBall");
        }
        else
        {
            //Debug.Log("exit : RunningState_withBall");
            // top yavaþlatýlmalý

            // topun mevcut hýz vektörü bulundu
            Vector3 currentBallVelocity = goalkeeper.Ball.Rb.velocity;
            // hedef vektör için mevcut hýz vektörüne eklenmesi gerekli olan vektör bulundu
            Vector3 addVelocityToBall = Vector3.zero - currentBallVelocity;

            // eðer eklenecek vektör sýnýrlamadan büyük ise büyüklüðü ayarlandý
            if (addVelocityToBall.magnitude > ballTargetVelocity)
            {
                addVelocityToBall = addVelocityToBall.normalized * ballTargetVelocity;

            }
            // eklenecek vektör topa eklendi
            currentBallVelocity += addVelocityToBall;
            goalkeeper.Ball.Rb.velocity = currentBallVelocity;
            //eðer top çok yavaþ ise direk hýzý 0 a eþitlenir
            if (goalkeeper.Ball.Rb.velocity.magnitude < 3f)
            {
                goalkeeper.Ball.Rb.velocity = Vector3.zero;
                goalkeeper.Ball.Rb.angularVelocity = Vector3.zero;
            }

        }

    }

    public void ExecuteTheState(Goalkeeper goalkeeper)
    {


    }

    private void ExecuteWithBall(Goalkeeper goalkeeper)
    {



    }


    private void ExecuteWithoutBall(Goalkeeper goalkeeper)
    {





    }



    private float gap = 2f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Run(Goalkeeper goalkeeper)
    {




    }


    private float ballTargetVelocity = 15.5f;

    private float trackingToBallSpeed = 13f;

    private float minDistanceWithBall = 1.8f;

    private float ballMaxAddVelocity = 5.2f;

    private float minDistanceToAddVelocityToBall = 2f; // minDistanceWithBall dan büyük olmalý


    private Vector3 lastBallForward;
    private void RunWithBall(Goalkeeper goalkeeper)
    {


    }

    private void Spin(Goalkeeper goalkeeper, Vector2 targetForward)
    {

        //  dönme x ve z eksenine göre y de yapýlýyor bu yüzden 2 boyutlu bir iþlem
        //  kuþ bakýþý yapýlýyor olarak düþünülebilir

        // x ve z eksenindeki mevcut Forward vektörü bulundu
        Vector2 curretForward = new Vector2(goalkeeper.transform.forward.x, goalkeeper.transform.forward.z);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre goalkeeperýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = goalkeeper.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1 : -1);
            goalkeeper.transform.eulerAngles = eulerAng;
            //---

        }

    }


}
