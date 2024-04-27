using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogStateMachine : MonoBehaviour
{
    private ICoolDogState currentState;
    private CoolDogController controller;

    public DogStateMachine()
    {
        currentState = new IdleState(controller);
    }

    public void ChangeState(ICoolDogState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    public void Update()
    {
        currentState.UpdateState();
    }

    public void HandleInput(InputAction.CallbackContext context)
    {
        currentState.HandleInput(context);
    }
}

public interface ICoolDogState
{
    void EnterState();
    void UpdateState();
    void ExitState();
    void HandleInput(InputAction.CallbackContext context);
}