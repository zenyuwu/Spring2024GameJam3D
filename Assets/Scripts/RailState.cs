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
        Debug.Log("rail enter");

        //activate RailDog
        controller.railDog.SetActive(true);
        controller.normalDog.SetActive(false);
        controller.jumpDog.SetActive(false);
    }

    public void ExitState()
    {
        controller.cool += 10;
        controller.StartCoroutine(StopMovement());
    }

    public void UpdateState()
    {
        if (!controller.isOnRail) controller.stateMachine.ChangeState(new IdleState(controller));
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
