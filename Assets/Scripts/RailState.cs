using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RailState : ICoolDogState
{
    CoolDogController controller;
    bool isRight;

    public RailState(CoolDogController controller, bool isRailRight)
    {
        this.controller = controller;
        this.isRight = isRailRight;
    }

    public void EnterState()
    {
        //do anim
    }

    public void ExitState()
    {
        controller.rb.velocity += new Vector3(0, 5, 0);
        controller.StartCoroutine(StopMovement());
    }

    public void HandleInput()
    {
        
    }

    public void UpdateState()
    {
        if (!controller.isOnRail) controller.stateMachine.ChangeState(new IdleState(controller));
        //continue anim?

        float moveDirection = isRight ? 1 : -1;

        if (moveDirection > 0) controller.dogSprite.flipX = false;
        if (moveDirection < 0) controller.dogSprite.flipX = true;
        Vector2 velocity = controller.rb.velocity;

        velocity.x = moveDirection * ((controller.cool / controller.coolNerf) + controller.baseSpeed);

        controller.rb.velocity = velocity;
    }

    IEnumerator StopMovement()
    {
        float decel = controller.decel;
        while (controller.rb.velocity.x > 0.01f || controller.rb.velocity.x < 0.01f)
        {
            controller.rb.velocity = Vector3.Lerp(controller.rb.velocity, Vector3.zero, decel);
            yield return new WaitForSeconds(0.1f);
        }
        controller.rb.velocity = Vector3.zero;
    }
}
