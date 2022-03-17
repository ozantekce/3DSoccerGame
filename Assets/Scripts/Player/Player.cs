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
        // animatorde MovementSpeed parametresi run animasyonunun hýzýný belirliyor
        // koþma hýzý ile oranlý animasyon hýzý
        animator.SetFloat("MovementSpeed", 0.7f + (rb.velocity.magnitude / 25f));
        currentState.ExecuteTheState(this);


    }

    public void ChangeCurrentState(PlayerState nextState)
    {
        currentState.ExitTheState(this);
        currentState = nextState;
        currentState.EnterTheState(this);
    }

    public void ChangeCurrentAction(PlayerAction action){ currentAction = action;}

    public void AddActionToCurrentAction(PlayerAction action){ currentAction.AddAction(action);}

    public void StartCurrentAction(){ currentAction.StartAction();}

    public void StopCurrentAction(){ currentAction.StopAction();}

    public void MoveNextAction()
    {
        if (currentAction == null)
            return;

        currentAction = currentAction.NextAction;

        if (currentAction != null)
            currentAction.StartAction();

    }

    public bool ActionsOver()
    {
        return currentAction == null;
    }

    public void ChangeAnimation(string name)
    {
        //Debug.Log("anim : "+name);
        animator.CrossFade(name, 0.03f);

    }


    public PlayerState CurrentState { get => currentState;}
    public PlayerAction CurrentAction { get => currentAction; }

    public Inputter Inputter { get => inputter; }
    public BallVision BallVision { get => ballVision; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Ball Ball { get => ball; set => ball = value; }

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
