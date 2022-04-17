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
            // top yava�lat�lmal�
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
        // Running i�in Runningwithball ve Runningwithoutball diye 2 state olu�turmak yerine 2 methoda b�ld�m
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
            // aya��na kay�ld� 
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
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
        }
        else
        {
            //RunningState in i�lemleri yap�l�r
            RunWithBall(player);
        }

    }


    private void ExecuteWithoutBall(Player player)
    {

        // aya��na kay�ld� 
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
            // input olmad��� i�in IdleState gider
            player.ChangeCurrentState(IdleState.idleState);
            
        }
        else
        {
            //RunningState in i�lemleri yap�l�r
            Run(player);
        }





    }

    private void Run(Player player)
    {

        // inputa g�re x ve z eksenindeki hedef Forward vekt�r� bulundu 
        // verilen inputa g�re forward belirleniyor
        // mevcut forward ile hedeflenen forward ayn� olana kadar 
        // karakter d�nd�r�l�yor
        Vector2 targetForward 
            = new Vector2(player.VerticalInput, player.HorizontalInput).normalized;

        // Rotation ayarlanmas� i�in spin methoduna gidilir
        Spin(player, targetForward);

        // y olmadan birim forward vekt�r� bulundu
        Vector3 directionVector 
            = new Vector3(player.transform.forward.x, 0, player.transform.forward.z).normalized;

        directionVector *= player.MovementSpeed;    // h�z vekt�r� olu�turuldu
        
        directionVector.y = player.Rb.velocity.y;   // y'nin de�i�mesini istemiyorum

        player.Rb.velocity = directionVector;       // player�n h�z� ayarland�

    }


    private float minDistanceToHitBall = 1.8f;

    private void RunWithBall(Player player)
    {

        // top g�r�� alan�nda m� ?
        switch (player.BallVision.IsThereBallInVision())
        {
            case true:
                {
                    float distanceWithBall = Vector3.Distance(Ball.Instance.transform.position,
                        player.transform.position);
                    // top vuracak kadar yak�n m� ?
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
                                //Hay�r bir �ey yapma
                            }
                            goto default;
                        default:
                            {
                                //Her zaman topa do�ru ko�
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
            //  targetForward ile mevcut forward aras�ndaki a�� fark�
            //  160 dereceden fazla ise targetForward mevcut forward�n 90 derece
            //  d�nd�r�lm�� halidir
            //  bunun sebebi -> y�n�nde ilerelerken <- de input geldi�inde
            //  topu �nce a�a�� y�n�ne sonra <- y�n�ne hareket ettirmek
            //  bu sayede top karaktere �arpmaz
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



    private float gap = 4f;        //  k���k a�� farklar� g�rmezden gelinir
    private float spinSpeed = 500f; //  rotation de�i�im h�z�
    private void Spin(Player player, Vector2 targetForward)
    {
        //  d�nme x ve z eksenine g�re y de yap�l�yor bu y�zden 2 boyutlu bir i�lem
        //  ku� bak��� yap�l�yor olarak d���n�lebilir

        // x ve z eksenindeki mevcut Forward vekt�r� bulundu

        Vector2 curretForward = VectorCalculater.ThreeDForwardToTwoDForward(player.transform.forward);

        //  aradaki a�� bulundu
        float angleBetweenVectors = Vector2.SignedAngle(curretForward, targetForward);

        // aradaki a�� k���k de�il ise i�lem yap�l�r
        if (Mathf.Abs(angleBetweenVectors) > gap)
        {
            //-- d�nme y�n�ne g�re player�n eulerAngles� spinSpeed*Time.deltaTime kadar de�i�tirildi
            Vector3 eulerAng = player.transform.eulerAngles;
            eulerAng.y += spinSpeed * Time.deltaTime * ((angleBetweenVectors < 0) ? +1:-1);
            player.transform.eulerAngles = eulerAng;
            //---

        }

    }


}

