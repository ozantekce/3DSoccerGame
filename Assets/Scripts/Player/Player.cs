using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerState currentState = IdleState.idleState;

    private PlayerAction currentAction = null;

    private bool fallBySlide;


    [SerializeField]
    private float movementSpeed = 15f, shootPower = 50f, passPower = 20f , slidePower = 20f;


    private Rigidbody rb;
    private Animator animator;
    private Inputter inputter;
    private BallVision ballVision;
    private Ball ball;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inputter = GetComponent<Inputter>();
        ballVision = GetComponent<BallVision>();
        ball = Ball.Instance;
    }

    private void Update()
    {

        animator.SetFloat("MovementSpeed", 0.7f + (rb.velocity.magnitude / 25f));
        currentState.Execute(this);


    }

    public void ChangeCurrentState(PlayerState nextState)
    {
        currentState.Exit(this);
        currentState = nextState;
        currentState.Enter(this);
    }

    public void ChangeAnimation(string name)
    {
        //Debug.Log("anim : "+name);
        animator.CrossFade(name, 0.03f);

    }


    public PlayerState CurrentState { get => currentState;}
    public Inputter Inputter { get => inputter; }
    public BallVision BallVision { get => ballVision; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Ball Ball { get => ball; set => ball = value; }
    public PlayerAction CurrentAction { get => currentAction; set => currentAction = value; }
    public float ShootPower { get => shootPower; set => shootPower = value; }
    public float PassPower { get => passPower; set => passPower = value; }
    public float SlidePower { get => slidePower; set => slidePower = value; }
    public bool FallBySlide { get => fallBySlide; set => fallBySlide = value; }


    private void OnTriggerEnter(Collider other)
    {
        
    }


    private void OnCollisionStay(Collision collision)
    {
        if (FallBySlide == false && collision.gameObject.CompareTag("Player"))
        {

            Player player = collision.gameObject.GetComponent<Player>();
            if (player.currentState == SlideState.slideState)
            {
                
                FallBySlide = true;
            }

        }
    }



}
