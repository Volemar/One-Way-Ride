using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DoorStatus
{
    HalfOpened,
    FullOpened,
    Closed
}
//[RequireComponent(typeof(Locker))]
public class Door : MonoBehaviour, IInteractable
{
    //private Locker locker;
    public float doorOpenAngle = 90.0f;
    public float doorCloseAngle = 0.0f;
    public float doorAnimSpeed = 2f;
    private Quaternion doorOpen = Quaternion.identity;
    private Quaternion doorFullOpen = Quaternion.identity;
    private Quaternion doorClose = Quaternion.identity;
    private DoorStatus doorStatus;
    [SerializeField] Transform UIPlaceholder;
    private bool doorGo = false;
    public bool isExplorable => false;
    public Vector3 UIPosition => UIPlaceholder.position;

    bool IInteractable.hasCloseInteraction => throw new System.NotImplementedException();

    bool IInteractable.hasToggleCloseInteraction => throw new System.NotImplementedException();
    
    private void Start()
    {
        doorStatus = DoorStatus.Closed;
        doorOpen = Quaternion.Euler(0, 0, 20);
        doorFullOpen = Quaternion.Euler(0, 0, 60);
        doorClose = Quaternion.Euler(0, 0, -80);
        //locker = GetComponent<Locker>();
    }
    public void ShowUI()
    {
    }
    private IEnumerator moveDoor(Quaternion dest, DoorStatus nextStatus)
    {
        doorGo = true;

        Quaternion turn = transform.localRotation * dest;
        while (Quaternion.Angle(transform.localRotation, turn) > .1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, turn, doorAnimSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = turn;
        doorGo = false;
        doorStatus = nextStatus;
        yield return null;
    }

    public void Interact()
    {
        Debug.Log("Shit");
        if (doorGo) return;
        switch (doorStatus)
        {
            case DoorStatus.Closed:
                StartCoroutine(this.moveDoor(doorOpen, DoorStatus.HalfOpened));
                break;
            case DoorStatus.HalfOpened:
                StartCoroutine(this.moveDoor(doorFullOpen, DoorStatus.FullOpened));
                break;
            case DoorStatus.FullOpened:
                StartCoroutine(this.moveDoor(doorClose, DoorStatus.Closed));
                break;
            default:
                break;
        }
    }

    public void CloseInteraction()
    {
        
    }

    public void StopCloseInteraction()
    {
        
    }

    public void StopInteracting()
    {
        
    }
}