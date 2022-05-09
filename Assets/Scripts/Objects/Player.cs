using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour, Movable,Shotable,Passable,Dribblingable,Slideable
{

    [SerializeField]
    protected float movementSpeed,spinSpeed,shotPower,passPower,slidePower, maxDistanceToDribbling,dribblingPower, trackingSpeed, shotCD,passCD,dribblingCD, slideCD;


    private Rigidbody rb;


    BallVision ballVision;

    private Animator animator;

    private Cooldown shotCooldown,passCooldown, dribblingCooldown,slideCooldown;

    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }
    public float SpinSpeed { get { return spinSpeed; } set { spinSpeed = value; } }
    public float ShotPower { get { return shotPower; } set { shotPower = value; } }
    public float PassPower { get { return passPower; } set { passPower = value; } }
    public float SlidePower { get { return slidePower; } set { slidePower = value; } }

    public float MaxDistanceToDribbling { get { return maxDistanceToDribbling; } set { maxDistanceToDribbling = value; } }
    public float DribblingPower { get { return dribblingPower; } set { dribblingPower = value; } }
    public float TrackingSpeed { get { return trackingSpeed; } set { trackingSpeed = value; } }


    public Cooldown ShotCooldown { get { return shotCooldown; } }
    public Cooldown PassCooldown { get { return passCooldown; } }
    public Cooldown DribblingCooldown { get { return dribblingCooldown; } }

    public Cooldown SlideCooldown { get { return slideCooldown; } }

    public GameObject GameObject { get { return gameObject; } }

    public Rigidbody Rigidbody { get => rb; set => rb = value; }
    
    public BallVision BallVision { get => ballVision; set => ballVision = value; }

    public Animator Animator { get => animator; set => animator = value; }

    public void Start()
    {

        shotCooldown = new Cooldown(shotCD);
        passCooldown = new Cooldown(passCD);
        dribblingCooldown = new Cooldown(dribblingCD);
        slideCooldown = new Cooldown(slideCD);
        Rigidbody = GetComponent<Rigidbody>();
        BallVision = GetComponent<BallVision>();
        animator = GetComponent<Animator>();

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {


    }


}
