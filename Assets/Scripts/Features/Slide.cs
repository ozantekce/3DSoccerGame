using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallVision))]
[AddComponentMenu("Features/Slide")]
public class Slide : MonoBehaviour
{

    [SerializeField]
    private float slidePower;

    private Ball ball;

    private Inputter inputter;
    private BallVision ballVision;
    private Rigidbody rb;
    private AnimationControl animationControl;

    [SerializeField]
    private float cooldownTimeSlide = 700f;
    private CooldownManualReset cooldownForSlide;
    

    private void Start()
    {

        ball = Ball.Instance;
        inputter = GetComponent<Inputter>();
        ballVision = GetComponent<BallVision>();
        rb = GetComponent<Rigidbody>();
        animationControl = GetComponent<AnimationControl>();

        cooldownForSlide = new CooldownManualReset(cooldownTimeSlide);

    }

    private void Update()
    {
        if (inputter.GetButtonSlideValue() > 0)
            Slide_(0.3f);


    }

    private void Slide_(float wait)
    {


        if (cooldownForSlide.TimeOver() && slideFinished && !ballVision.IsThereBallInVision())
        {
            StartCoroutine(Slide__(slidePower, wait));

        }



    }





    private bool slideFinished = true;

    public bool SlideFinished { get => slideFinished; set => slideFinished = value; }
    public CooldownManualReset CooldownForSlide { get => cooldownForSlide; set => cooldownForSlide = value; }

    private IEnumerator Slide__(float velocityMagnitude, float wait)
    {

        slideFinished = false;

       
        animationControl.ChangeAnimation("Slide");
        yield return new WaitForSeconds(wait);
        Vector3 targetPosition;
        targetPosition =  transform.position + transform.forward*5f;
        MyMovePosition(targetPosition, velocityMagnitude);
        cooldownForSlide.ResetTimer();
        slideFinished = true;
        

    }











    public void MyMovePosition(Vector3 position, float speed)
    {

        if (transform.position != position)
        {
            Vector3 directionVector = position - transform.position;
            directionVector = directionVector.normalized;
            directionVector.y = 0;
            //speed = Mathf.Clamp(speed, 0, Vector3.Distance(position, transform.position));
            rb.velocity = directionVector * speed;

        }


    }








}
