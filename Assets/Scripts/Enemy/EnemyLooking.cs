using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public enum EnemyLookingState
{
    No,
    Barely,
    Clear
}

public class EnemyLooking : MonoBehaviour
{
    [SerializeField] private float _enemyFOV = 80f;
    [SerializeField] private float _enemyLookDistance = 20f;
    [SerializeField] private float _enemyFarLookDistance = 30f;
    [SerializeField] private Vector3 _lastSawPlayerPosition;
    [SerializeField] private Transform _currentPlayerTransform;
    [SerializeField] private Transform _eyes;
    public EnemyLookingState state;
    [SerializeField]private FirstPersonController playerController;
    
    private LayerMask _layerMask;
    private const string RaycastingLayer = "Character";
    private void Awake()
    {
        _layerMask = LayerMask.GetMask(RaycastingLayer);
        state = EnemyLookingState.No;
        playerController = FindObjectOfType<FirstPersonController>();
    }
    private void Update()
    {
        RaycastForPlayer();
    }

    public void SetTheLastSawPlayerPositionToPositiveInfinity()
    {
        _lastSawPlayerPosition = Vector3.positiveInfinity;
    }

    private void RaycastForPlayer()
    {
        state = CheckForPlayerInView();
        RaycastHit hit;
        if(Physics.Linecast(_eyes.position, _currentPlayerTransform.position, out hit))
        {
            if(!hit.transform.GetComponent<FirstPersonController>()) 
            {
                state = EnemyLookingState.No;
                return;
            }
        }
        switch (state)
        {
            case EnemyLookingState.No:
            {
                //Debug.Log("I can't see you, come here, honey!");
            }
            break;
            case EnemyLookingState.Barely:
            {
                _lastSawPlayerPosition = playerController.transform.position;
                //Debug.Log("I almost see you, honey!");
            }
            break;
            case EnemyLookingState.Clear:
            {
                _lastSawPlayerPosition = playerController.transform.position;
                Debug.Log("Updating last player position");
            }
            break;
            default:break;
        }
    }

    public Vector3 GetLastSawPlayerPosition()
    {
        return _lastSawPlayerPosition;
    }
    
    private EnemyLookingState CheckForPlayerInView()
    {
        if(Vector3.Distance(_eyes.position, _currentPlayerTransform.position) > _enemyLookDistance) return EnemyLookingState.No;

        if(Vector3.Angle(_eyes.forward, _currentPlayerTransform.position - _eyes.position) > _enemyFOV) return EnemyLookingState.No;

        if(Vector3.Distance(_eyes.position, _currentPlayerTransform.position) > _enemyFarLookDistance) return EnemyLookingState.Barely;

        //TODO Add something like dependency on darkness around you or smth

        return EnemyLookingState.Clear;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_eyes.position, _currentPlayerTransform.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(_eyes.position, _eyes.forward);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_eyes.position, _lastSawPlayerPosition);
    }

}
