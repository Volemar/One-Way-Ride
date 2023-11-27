using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMover : MonoBehaviour
{
    private EnemyLooking _looking;
    [SerializeField] float walkingSpeed = 2f;
    [SerializeField] float runningSpeed = 4f;
    private CharacterController controller;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        _looking = GetComponent<EnemyLooking>();
    }

    private void Update()
    {
        //if(_looking.state == EnemyLookingState.Clear) ChasePlayer();
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        //Debug.Log("I GOT YOU!");
        if(_looking.GetLastSawPlayerPosition() == Vector3.positiveInfinity) return;
        if((_looking.GetLastSawPlayerPosition() == transform.position)) //TODO make some threshold
        {
            _looking.SetTheLastSawPlayerPositionToPositiveInfinity();
            return;
        }
        controller.Move((_looking.GetLastSawPlayerPosition() - transform.position) * (runningSpeed * Time.unscaledDeltaTime));
    }
}
