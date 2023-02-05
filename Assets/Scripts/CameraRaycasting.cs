using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycasting : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private LayerMask _layerMask;
    private const string RaycastingLayer = "Raycasting";
    private RaycastHit _hitInfo;
    public float range = 10f;
    private bool _isHitting = false;

    public RaycastHit HitInfo => _hitInfo;
    public bool IsHitting => _isHitting;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask(RaycastingLayer);
    }

    private void Update()
    {
        InteractRaycast();
    }

    private void InteractRaycast()
    {
        _isHitting = false;
        var cameraTransform = _mainCamera.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out _hitInfo, range, _layerMask))
        {
            _isHitting = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward);
    }
}
