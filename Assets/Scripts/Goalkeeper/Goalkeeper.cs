using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper : MonoBehaviour
{
    private GoalkeeperState currentState = GoalkeeperIdleState.goalkeeperIdleState;

    private GoalkeeperAction currentAction = null;

    [SerializeField]
    private int team;

    private int direction;

    private Rigidbody rb;
    private Animator animator;
    private BallVision ballVision;
    private Ball ball;

    public Transform handPositionWhileJumping;
    public Hand leftHand;
    public Hand rightHand;



    [SerializeField]
    private float movementSpeed = 15f, shootPower = 50f,jumpPowerY = 10f, jumpPowerX = 20f;

    public Transform waitPositionTransform;

    private Vector3 waitPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        ballVision = GetComponent<BallVision>();
        ball = Ball.Instance;
        waitPosition = waitPositionTransform.position;

        if (team == 1)
            direction = 1;
        else
            direction = -1;


    }

    private void FixedUpdate()
    {
        // animatorde MovementSpeed parametresi run animasyonunun hýzýný belirliyor
        // koþma hýzý ile oranlý animasyon hýzý
        animator.SetFloat("MovementSpeed", 0.7f + (rb.velocity.magnitude / 25f));


        currentState.ExecuteTheState(this);


    }


    public void ChangeCurrentState(GoalkeeperState nextState)
    {
        currentState.ExitTheState(this);
        currentState = nextState;
        currentState.EnterTheState(this);
    }

    public void ChangeCurrentAction(GoalkeeperAction action) { currentAction = action; }

    public void AddActionToCurrentAction(GoalkeeperAction action) { currentAction.AddAction(action); }

    public void StartCurrentAction() { currentAction.StartAction(); }

    public void StopCurrentAction() { currentAction.StopAction(); }

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



    public GoalkeeperState CurrentState { get => currentState; }
    public GoalkeeperAction CurrentAction { get => currentAction; }


    public BallVision BallVision { get => ballVision; }
    public Rigidbody Rb { get => rb; set => rb = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public Ball Ball { get{
            if (ball == null)
                ball = Ball.Instance;
            return ball; 
        } set => ball = value; }

    public float ShootPower { get => shootPower; set => shootPower = value; }
    public int Team { get => team; set => team = value; }

    public Vector3 WaitPosition { get => waitPosition; set => waitPosition = value; }
    public float JumpPowerY { get => jumpPowerY; set => jumpPowerY = value; }
    public float JumpPowerX { get => jumpPowerX; set => jumpPowerX = value; }
    public int Direction { get => direction; set => direction = value; }
}
    