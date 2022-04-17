using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState: PlayerState
{

    public static RunningState runningState = new RunningState();

    private State currentState = State.withoutBall;
    private enum State
    {
        withBall, withoutBall
    }

    public void EnterTheState(Player player)
    {
        player.ChangeAnimation("Run");

    }

    public void ExitTheState(Player player)
    {

        if (currentState == State.withoutBall)
        {
            //Debug.Log("exit : RunningState_withoutBall");
        }
        else
        {
            //Debug.Log("exit : RunningState_withBall");
            // top yavaþlatýlmalý
            if(player.ShootInput != 0)
            {
                Vector3 directionVector
                    = VectorCalculater.CalculateDirectionVector(Ball.Instance.GetVelocity(), Vector3.zero);
                Ball.Instance.HitTheBall_(directionVector * player.HitPower);
            }


        }

    }

    public void ExecuteTheState(Player player)
    {
        // Running için Runningwithball ve Runningwithoutball diye 2 state oluþturmak yerine 2 methoda böldüm
        if (player.BallVision.IsThereBallInVision())
        {
            currentState = State.withBall;
            ExecuteWithBall(player);
        }
        else
        {
            currentState = State.withoutBall;
            ExecuteWithoutBall(player);
        }
    }

    private void ExecuteWithBall(Player player)
    {
        if (player.FallBySlide)
        {
            // ayaðýna kayýldý 
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        else if(player.ShootInput != 0)
        {   //Shoot inputu var shootState e gider
            player.ChangeCurrentState(ShotState.shootState);
        }
        else if (player.PassInput != 0)
        {   //Pass inputu var passState e gider
            player.ChangeCurrentState(PassState.passState);
        }
        else if (player.VerticalInput == 0
            && player.HorizontalInput == 0)
        {
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
        }
        else
        {
            //RunningState in iþlemleri yapýlýr
            RunWithBall(player);
        }

    }


    private void ExecuteWithoutBall(Player player)
    {

        // ayaðýna kayýldý 
        if (player.FallBySlide)
        {
            player.ChangeCurrentState(FallBySlideState.fallBySlideState);
        }
        //Slide inputu var SlideState e gider
        else if (player.SlideInput != 0)
        {   
            player.ChangeCurrentState(SlideState.slideState);

        }
        else if (player.VerticalInput== 0
            && player.HorizontalInput == 0)
        {   
            // input olmadýðý için IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
            
        }
        else
        {
            //RunningState in iþlemleri yapýlýr
            Run(player);
        }





    }

    private void Run(Player player)
    {

        // inputa göre x ve z eksenindeki hedef Forward vektörü bulundu 
        // verilen inputa göre forward belirleniyor
        // mevcut forward ile hedeflenen forward ayný olana kadar 
        // karakter döndürülüyor
        Vector2 targetForward 
            = new Vector2(player.VerticalInput, player.HorizontalInput).normalized;

        // Rotation ayarlanmasý için spin methoduna gidilir
        Spin(player, targetForward);

        // y olmadan birim forward vektörü bulundu
        Vector3 directionVector 
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        directionVector *= player.MovementSpeed;    // hýz vektörü oluþturuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin deðiþmesini istemiyorum

        player.Rb.velocity = directionVector;       // playerýn hýzý ayarlandý

    }


    private float minDistanceToHitBall = 1.8f;

    private void RunWithBall(Player player)
    {

        // top görüþ alanýnda mý ?
        switch (player.BallVision.IsThereBallInVision())
        {
            case true:
                {
                    float distanceWithBall = Vector3.Distance(Ball.Instance.transform.position,
                        player.transform.position);
                    // top vuracak kadar yakýn mý ?
                    switch (distanceWithBall<minDistanceToHitBall) {

                        case true:
                            {
                                //Evet topa vur
                                if(Ball.Instance.HitTheBall(
                                    CalculateHitVector(player)))
                                {
                                    //vusdrma animasyonu eklenebilir
                                }
                                else
                                {
                                    //player.ChangeAnimation("Run");
                                }

                            }
                            goto default;
                        case false:
                            {
                                //Hayýr bir þey yapma
                            }
                            goto default;
                        default:
                            {
                                //Her zaman topa doðru koþ
                                Vector3 directionVector
                                    = VectorCalculater.CalculateDirectionVectorWithoutYAxis(
                                        player.transform.position, Ball.Instance.transform.position
                                        );

                                Spin(player, VectorCalculater.Vector3toVector2(directionVector, Axis.y).normalized);
                                float trackingSpeed
                                   = player.MovementSpeed * CONSTANTS.Linear(distanceWithBall, 0, 3f);

                                player.Rb.velocity = trackingSpeed * directionVector; ;
                            }
                            break;

                    }
                }
                break;
            case false:
                { 
                    // do nothing
                }
                break;

        }


    }


    private Vector3 CalculateHitVector(Player player)
    {
        float inputVertical = player.VerticalInput;
        float inputHorizontal = player.HorizontalInput;
        float max = player.MovementSpeed+5f;

        Vector3 ballForwardVector = (player.Ball.transform.position - player.transform.position).normalized;

        Vector3 ballTargetForwardVector
            = new Vector3(inputVertical, 0, inputHorizontal).normalized;

        float angle = Vector3.SignedAngle(ballForwardVector, ballTargetForwardVector, Vector3.up);
        if (Mathf.Abs(angle) > 160f)
        {
            //  targetForward ile mevcut forward arasýndaki açý farký
            //  160 dereceden fazla ise targetForward mevcut forwardýn 90 derece
            //  döndürülmüþ halidir
            //  bunun sebebi -> yönünde ilerelerken <- de input geldiðinde
            //  topu önce aþaðý yönüne sonra <- yönüne hareket ettirmek
            //  bu sayede top karaktere çarpmaz
            ballTargetForwardVector = Quaternion.AngleAxis(-90, Vector3.up) * ballTargetForwardVector;
        }

        ballForwardVector.y = 0;

        Vector3 rtn = player.HitPower * ballTargetForwardVector.normalized;

        if ((Ball.Instance.Rb.velocity + rtn).magnitude>max)
        {
            return rtn * 0.75f;
        }
        else
        {
            return rtn;
        }


    }



    private float gap = 4f;        //  küçük açý farklarý görmezden gelinir
    private float spinSpeed = 500f; //  rotation deðiþim hýzý
    private void Spin(Player player, Vector2 targetForward)
    {
        //  dönme x ve z eksenine göre y de yapýlýyor bu yüzden 2 boyutlu bir iþlem
        //  kuþ bakýþý yapýlýyor olarak düþünülebilir

        // x ve z eksenindeki mevcut Forward vektörü bulundu

        Vector2 curretForward = VectorCalculater.ThreeDForwardToTwoDForward(player.transform.forward);

        //  aradaki açý bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki açý küçük deðil ise iþlem yapýlýr
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- dönme yönüne göre playerýn eulerAnglesý spinSpeed*Time.deltaTime kadar deðiþtirildi
            Vector3 eulerAng = player.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1:-1);
            player.transform.eulerAngles = eulerAng;
            //---

        }

    }


}

