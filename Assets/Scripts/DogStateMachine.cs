using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogStateMachine : MonoBehaviour
{
    private ICoolDogState currentState;
    [SerializeField] CoolDogController controller;

    private void Awake()
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
}

public interface ICoolDogState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}