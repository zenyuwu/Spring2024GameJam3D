using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : ICoolDogState
{
    CoolDogController controller;
    bool check = false;

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

        controller.StartCoroutine(WaitToCheckGround());
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {
        if (check && controller.isGrounded)
        {
            controller.stateMachine.ChangeState(new MoveState(controller));
        }

        Vector2 moveDirection = controller.moveAction.ReadValue<Vector2>();

        Vector2 velocity = controller.rb.velocity;

        velocity.x = moveDirection.x * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

        controller.rb.velocity = velocity;

    }

    IEnumerator WaitToCheckGround()
    {
        yield return new WaitForSeconds(0.5f);
        check = true;
    }


}
