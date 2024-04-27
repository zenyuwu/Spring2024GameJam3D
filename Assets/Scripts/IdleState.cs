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
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void HandleInput(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        
    }
}
