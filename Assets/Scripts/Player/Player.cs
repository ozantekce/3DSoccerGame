using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerState currentState = IdleState.idleState;

    private PlayerAction currentAction = null;

    private bool fallBySlide;


    [SerializeField]
    private float movementSpeed = 15f, shootPower = 50f, passPower = 45f , slidePower = 20f
        , hitPower =5f;



    private int team;
    private int playerIndex;

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


    private float verticalInput;
    private float horizontalInput;
    private float shootInput;
    private float passInput;
    private float slideInput;


    private void Update()
    {
        // animatorde MovementSpeed parametresi run animasyonunun hýzýný belirliyor
        // koþma hýzý ile oranlý animasyon hýzý
        animator.SetFloat("MovementSpeed", 0.7f + (rb.velocity.magnitude / 25f));

        shootInput = Inputter.GetButtonShootValue();
        passInput = Inputter.GetButtonPassValue();
        slideInput = Inputter.GetButtonSlideValue();
        verticalInput = -Inputter.GetJoyStickVerticalValue();
        horizontalInput = Inputter.GetJoyStickHorizontalValue();

    }


    private void FixedUpdate()
    {

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
    public Ball Ball
    {
        get
        {
            if (ball == null)
                ball = Ball.Instance;
            return ball;
        }
        set => ball = value;
    }

    public float ShootPower { get => shootPower; set => shootPower = value; }
    public float PassPower { get => passPower; set => passPower = value; }
    public float SlidePower { get => slidePower; set => slidePower = value; }
    public bool FallBySlide { get => fallBySlide; set => fallBySlide = value; }
    public int Team { get => team; set => team = value; }
    public int PlayerIndex { get => playerIndex; set => playerIndex = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float ShootInput { get => shootInput; set => shootInput = value; }
    public float PassInput { get => passInput; set => passInput = value; }
    public float SlideInput { get => slideInput; set => slideInput = value; }
    public float HitPower { get => hitPower; set => hitPower = value; }

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
