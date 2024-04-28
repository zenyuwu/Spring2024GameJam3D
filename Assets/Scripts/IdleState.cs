using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : ICoolDogState
{
    private CoolDogController controller;

    public IdleState(CoolDogController controller)
    {
        this.controller = controller;
    }

    public void EnterState()
    {
        controller.railDog.SetActive(false);
        controller.normalDog.SetActive(true);
        controller.jumpDog.SetActive(false);
        Debug.Log("idle enter");
    }

    public void ExitState()
    {

    }

    public void HandleInput()
    {
        //if(!controller.isGrounded)
        //{
        //    controller.stateMachine.ChangeState(new FallState(controller));
        //}
        //else
        if(controller.moveAction.ReadValue<Vector2>().x != 0)
        {
            controller.stateMachine.ChangeState(new MoveState(controller));
        }
    }

    public void UpdateState()
    {

    }
}
