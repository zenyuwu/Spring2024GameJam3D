using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
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
        Debug.Log("idle enter");
        controller.railDog.SetActive(false);
        controller.normalDog.SetActive(true);
        controller.jumpDog.SetActive(false);
    }

    public void ExitState()
    {

    }

    public void UpdateState()
    {

        if (controller.moveAction.ReadValue<Vector2>().x != 0)
        {
            controller.stateMachine.ChangeState(new MoveState(controller));
        }
    }
}