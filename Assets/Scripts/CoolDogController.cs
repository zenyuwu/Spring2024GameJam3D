using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class CoolDogController : MonoBehaviour
{
    private CoolDogCharcterController playerActions;
    [SerializeField]private Transform groundCheck;

    private InputAction moveAction;

    [SerializeField] Rigidbody rb;

    [SerializeField] float accel = 5f;
    [SerializeField] float decel = 5f;
    [SerializeField] float baseSpeed = 5f;
    [SerializeField] float cool = 0f;
    [SerializeField] int health = 3;
    [SerializeField] float coolNerf = 10f;

    [SerializeField] bool faceRight = true;
    [SerializeField] float jumpForce = 6f; 
    [SerializeField] bool isGrounded = false;
    [SerializeField] bool isOnRail = false;

    private LayerMask groundLayerMask;
    private LayerMask railLayerMask;

    private Vector2 lastDirection;
    private bool lastRailStatus;
    private bool shmoovementLocked;

    private void Awake()
    {
        playerActions = new CoolDogCharcterController();
        rb = GetComponent<Rigidbody>();

        groundLayerMask = LayerMask.GetMask("Ground");
        railLayerMask = LayerMask.GetMask("Rail");
    }

    private void OnEnable()
    {
        moveAction = playerActions.Player.Move;
        moveAction.Enable();

        playerActions.Player.Jump.performed += OnJump;
        Debug.Log("OnJump subscribed");
        playerActions.Player.Jump.Enable();
        Debug.Log("OnJump enabled");
    }

    private void OnDisable()
    {
        moveAction.Disable();

        playerActions.Player.Jump.performed -= OnJump;
        Debug.Log("OnJump unsubscribed");

        playerActions.Player.Jump.Disable();
        Debug.Log("OnJump disabled");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(isGrounded || isOnRail)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            cool += 10;
        }
    }

    private void FixedUpdate()
    {
        cool -= Time.deltaTime;
        if (cool < 0) cool = 0;
        if (cool > 100) cool = 100;
        GameManager.Instance.SetCool(cool);

        if (health > 3) health = 3;
        if (health < 0) health = 0;
        GameManager.Instance.SetHealth(health);

        //ground check
        //only works on jump
        lastRailStatus = isOnRail;
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 0.05f, groundLayerMask);
        isOnRail = Physics.Raycast(groundCheck.position, Vector3.down, 0.10f, railLayerMask); //needs to be 0.10f to hit both horizontal and tilted rails

        //setting up movement
        Vector2 moveDirection = moveAction.ReadValue<Vector2>();
        if (moveDirection.x != 0 || moveDirection.y != 0) lastDirection = moveDirection;
        Vector2 velocity = rb.velocity;

        if (moveDirection.x > 0) faceRight = false;
        if (moveDirection.x < 0) faceRight = true;
        GetComponent<SpriteRenderer>().flipX = faceRight;

        if (lastRailStatus == true && isOnRail == false)
        {
            shmoovementLocked = true;
            velocity.y += 5;
            StartCoroutine(StopLockedMovement());
        }

        if (isOnRail || shmoovementLocked)
        {
            velocity.x = lastDirection.x * ((cool / (coolNerf/2)) + baseSpeed * 2);
        }
        else
        {
            velocity.x = moveDirection.x * ((cool / coolNerf) + baseSpeed);
        }

        rb.velocity = velocity;

/*        if (velocity != Vector2.zero)
        {
            rb.AddForce(rb.velocity * accel, ForceMode.Force);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            rb.AddForce(rb.velocity * -decel, ForceMode.Force);
        }*/
    }

    IEnumerator StopLockedMovement()
    {
        yield return new WaitForSeconds(1);
        shmoovementLocked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, groundCheck.position);
    }
}

