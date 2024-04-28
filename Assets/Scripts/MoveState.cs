using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveState : ICoolDogState
{
    CoolDogController controller;

    public MoveState(CoolDogController controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("move enter");
        controller.railDog.SetActive(false);
        controller.normalDog.SetActive(true);
        controller.jumpDog.SetActive(false);
    }

    public void ExitState()
    {
        Debug.Log("slow down");
        controller.StartCoroutine(StopMovement());
    }

    public void UpdateState()
    {
        Vector2 moveDirection = controller.moveAction.ReadValue<Vector2>();

        if (moveDirection.x == 0)
        {
            controller.stateMachine.ChangeState(new IdleState(controller));
            return;
        }

        Vector2 velocity = controller.rb.velocity;

        velocity.x = moveDirection.x * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

        controller.rb.velocity = velocity;
    }

    IEnumerator StopMovement()
    {
        float decel = controller.decel;
        while (controller.rb.velocity.x > 0.01f || controller.rb.velocity.x < -0.01f)
        {
            Vector3 horizontalKill = Vector3.zero;
            horizontalKill.y = controller.rb.velocity.y;
            controller.rb.velocity = Vector3.Lerp(controller.rb.velocity, horizontalKill, decel);
            //waits for 0.1f seconds before decrementing
            yield return new WaitForSeconds(0.1f);
        }
        controller.rb.velocity = Vector3.zero;
    }

}