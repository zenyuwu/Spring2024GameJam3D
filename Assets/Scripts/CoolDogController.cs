using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
[RequireComponent(typeof(Rigidbody))]
public class CoolDogController : MonoBehaviour
{
    [SerializeField] public float railCool, copCool, jumpCool, coolDecay;

    public DogStateMachine stateMachine;
    private CoolDogCharcterController playerActions;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject skateBoard;
    [SerializeField] public SpriteRenderer dogSprite;
    [SerializeField] private GameObject jumpSkateBoard;
    [SerializeField] public SpriteRenderer jumpDogSprite;

    [SerializeField] public AudioSource bark;

    //dogstates
    [SerializeField] public GameObject normalDog;
    [SerializeField] public GameObject railDog;
    [SerializeField] public GameObject jumpDog;

    public InputAction moveAction;

    [SerializeField] public Rigidbody rb;

    [SerializeField] public float accel = 5f;
    [SerializeField] public float decel = 5f;
    [SerializeField] public float baseSpeed = 5f;
    [SerializeField] public float cool = 0f;
    [SerializeField] public int health = 3;
    [SerializeField] public float coolNerf = 10f;

    [SerializeField] public bool faceRight = true;
    [SerializeField] public float jumpForce = 6f;
    [SerializeField] public bool isGrounded = false;
    [SerializeField] public bool isOnRail = false;
    [SerializeField] public bool isOnCar = false;
    public bool isRailRight;

    private LayerMask groundLayerMask;
    private LayerMask railLayerMask;
    private LayerMask carLayerMask;

    private void Awake()
    {
        playerActions = new CoolDogCharcterController();
        rb = GetComponent<Rigidbody>();
        stateMachine = GetComponent<DogStateMachine>();

        groundLayerMask = LayerMask.GetMask("Ground");
        railLayerMask = LayerMask.GetMask("Rail");
        carLayerMask = LayerMask.GetMask("Car");
    }

    private void OnEnable()
    {
        moveAction = playerActions.Player.Move;
        moveAction.Enable();

        playerActions.Player.Jump.performed += OnJump;
        playerActions.Player.Jump.Enable();

        playerActions.Player.Bark.performed += OnBark;
        playerActions.Player.Bark.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();

        playerActions.Player.Jump.performed -= OnJump;
        playerActions.Player.Jump.Disable();

        playerActions.Player.Bark.performed -= OnBark;
        playerActions.Player.Bark.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded || isOnRail || isOnCar)
        {
            stateMachine.ChangeState(new JumpState(this));
            cool += jumpCool;
        }
    }
    private void OnBark(InputAction.CallbackContext context)
    {
        bark.pitch = (Random.Range(0.8f, 1.2f));
        bark.Play();
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();

        stateMachine.Update();
        cool -= Time.deltaTime * coolDecay;
        if (cool < 0) cool = 0;
        if (cool > 100) cool = 100;
        GameManager.Instance.SetCool(cool);

        if (transform.position.y < -50) health--;
        if (health > 3) health = 3;
        GameManager.Instance.SetHealth(health);

        //ground check
        //only works on jump
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.05f, groundLayerMask);
        RaycastHit railHit;
        isOnRail = Physics.Raycast(groundCheck.position, Vector3.down, out railHit, 0.1f, railLayerMask);
        RaycastHit hit;
        isOnCar = Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.10f, carLayerMask);

        //Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, railLayerMask);

        if (isOnRail == true)
        {
            railHit.transform.gameObject.GetComponentInChildren<Rail>().isOnRail = true;
            stateMachine.ChangeState(new RailState(this, isRailRight));
        }

        if (moveDirection.x > 0)
        {
            dogSprite.flipX = false;
            jumpDogSprite.flipX = false;
            skateBoard.transform.localRotation = Quaternion.Euler(0, 270, 0);
            jumpSkateBoard.transform.localRotation = Quaternion.Euler(0, 270, 0);
        }
        if (moveDirection.x < 0) 
        {
            dogSprite.flipX = true;
            jumpDogSprite.flipX = true;
            skateBoard.transform.localRotation = Quaternion.Euler(0, 90, 0);
            jumpSkateBoard.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        //seperate to car.cs
        if (isOnCar)
        {
            if (hit.transform.gameObject.GetComponent<Car>().carCrash == false)
            {
                cool += copCool;
                hit.transform.gameObject.GetComponent<Car>().GetStomped();
            }
            //rb.velocity += new Vector3(0, 7.5f, 0);
        }

        //skateboard rotation, need to lock behind idle state (?)


    }

    /*    private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawRay(transform.position, groundCheck.position);
        }*/
}
