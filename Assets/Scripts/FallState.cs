using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : ICoolDogState
{
    CoolDogController controller;
    bool slowingX = false;

    public FallState(CoolDogController controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        controller.StartCoroutine(AccelDown());
    }

    public void ExitState()
    {
        controller.StartCoroutine(StopMovement());
    }

    public void HandleInput()
    {
        
    }

    public void UpdateState()
    {
        Vector2 moveDirection = controller.moveAction.ReadValue<Vector2>();

        if(moveDirection == Vector2.zero && !slowingX) {
            controller.StartCoroutine(StopMovement());
            slowingX = true;
        }

        if(!slowingX)
        {
            Vector2 velocity = controller.rb.velocity;

            velocity.x = moveDirection.x * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

            controller.rb.velocity = velocity;
        }
    }

    IEnumerator AccelDown()
    {
        while (!controller.isGrounded)
        {
            Vector2 velocity = controller.rb.velocity;
            velocity.y -= controller.accel;
            velocity.y = Mathf.Max(velocity.y, -50);
            controller.rb.velocity = velocity;
            yield return new WaitForSeconds(0.1f);
        }

        controller.stateMachine.ChangeState(new IdleState(controller));
    }

    IEnumerator StopMovement()
    {
        float decel = controller.decel;
        while (controller.rb.velocity.x > 0.01f || controller.rb.velocity.x < -0.01f)
        {
            if(controller.moveAction.ReadValue<Vector2>() != Vector2.zero)
            {
                yield break;
            }
            Vector3 horizontalKill = Vector3.zero;
            horizontalKill.y = controller.rb.velocity.y;
            controller.rb.velocity = Vector3.Lerp(controller.rb.velocity, horizontalKill, decel);
            //waits for 0.1f seconds before decrementing
            yield return new WaitForSeconds(0.1f);
        }
        controller.rb.velocity = Vector3.zero;
    }
}
