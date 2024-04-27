using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class CoolDogController : MonoBehaviour
{
    public DogStateMachine stateMachine;
    private CoolDogCharcterController playerActions;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private GameObject skateBoard;
    [SerializeField]public SpriteRenderer dogSprite;

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

        stateMachine.HandleInput();
    }

    private void OnDisable()
    {
        moveAction.Disable();

        playerActions.Player.Jump.performed -= OnJump;
        playerActions.Player.Jump.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded || isOnRail)
        {
            stateMachine.ChangeState(new JumpState(this));
            cool -= 10;
        }
    }

    private void FixedUpdate()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
        cool -= Time.deltaTime;
        if (cool < 0) cool = 0;
        if (cool > 100) cool = 100;
        GameManager.Instance.SetCool(cool);

        if (health > 3) health = 3;
        if (health < 0) health = 0;
        GameManager.Instance.SetHealth(health);

        //ground check
        //only works on jump
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.05f, groundLayerMask);

        //Physics.Raycast(groundCheck.position, Vector3.down, 0.1f, railLayerMask);

        if (isOnRail)
        {
            stateMachine.ChangeState(new RailState(this, isRailRight));
        }

        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.10f, carLayerMask))
        {
            hit.transform.gameObject.GetComponent<Car>().GetStomped();
        }

        //skateboard rotation, need to lock behind idle state (?)

        skateBoard.transform.localRotation = faceRight ? Quaternion.Euler(0, 90, 0) : Quaternion.Euler(0, 270, 0);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, groundCheck.position);
    }
}

