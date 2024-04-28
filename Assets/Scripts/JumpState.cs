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
    }

    public void ExitState()
    {
        //controller.StartCoroutine(IsJumping());
    }

    public void HandleInput()
    {
        Vector2 moveDirection = controller.moveAction.ReadValue<Vector2>();

        //if (!controller.isGrounded)
        //{
        //    controller.stateMachine.ChangeState(new FallState(controller));
        //}else 
/*        if (moveDirection.x == 0)
        {
            controller.stateMachine.ChangeState(new IdleState(controller));
            return;
        }*/

        Vector2 velocity = controller.rb.velocity;

        velocity.x = moveDirection.x * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

        controller.rb.velocity = velocity;
    }

    public void UpdateState()
    {
    }

/*    IEnumerator IsJumping()
    {
        while (controller.rb.velocity.y > 0.01f || controller.isGrounded == false)
        {
            yield return new WaitForSeconds(0.1f);
        }
        controller.stateMachine.ChangeState(new IdleState(controller));

    }*/


}
