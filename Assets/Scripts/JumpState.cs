using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : ICoolDogState
{
    CoolDogController controller;

    public JumpState(CoolDogController controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        controller.rb.AddForce(Vector3.up * controller.jumpForce, ForceMode.Impulse);

        controller.railDog.SetActive(false);
        controller.normalDog.SetActive(false);
        controller.jumpDog.SetActive(true);
        Debug.Log("jump enter");

        controller.stateMachine.ChangeState(new MoveState(controller));
    }

    public void ExitState()
    {
        //controller.stateMachine.ChangeState(new IdleState(controller));
        //controller.StartCoroutine(IsJumping());
    }


    public void HandleInput()
    {
        controller.stateMachine.ChangeState(new MoveState(controller));
        //Vector2 moveDirection = controller.moveAction.ReadValue<Vector2>();

        /*        if (controller.moveDirection.x != 0)
                {
                    controller.stateMachine.ChangeState(new MoveState(controller));
                }

                Vector2 velocity = controller.rb.velocity;

                velocity.x = controller.moveDirection.x * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

                controller.rb.velocity = velocity;

                Debug.Log("y handle velocity: " + controller.rb.velocity.y);

                if (controller.moveDirection.x == 0)
                {
                    controller.stateMachine.ChangeState(new IdleState(controller));
                    return;
                }*/

    }

    public void UpdateState()
    {
            Debug.Log("y velocity: " + controller.rb.velocity.y);
        if (controller.rb.velocity.y == 0f || controller.isGrounded || controller.isOnCar || controller.moveDirection.x != 0)
        {
            controller.stateMachine.ChangeState(new MoveState(controller));
        }
    }




}
