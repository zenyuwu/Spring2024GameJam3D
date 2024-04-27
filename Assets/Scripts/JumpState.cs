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
        controller.stateMachine.ChangeState(new IdleState(controller));
        //controller.StartCoroutine(IsJumping());
        Debug.Log("jump enter");
    }

    public void ExitState()
    {
        
    }

    public void HandleInput()
    {
        
    }

    public void UpdateState()
    {
        
    }


    IEnumerator IsJumping()
    {
        yield return new WaitForSeconds(1);
        controller.stateMachine.ChangeState(new IdleState(controller));
    }
}
