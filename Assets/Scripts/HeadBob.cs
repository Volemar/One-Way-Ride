using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
public class HeadBob : MonoBehaviour
{
    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
	private PlayerControls _input;
    private FirstPersonController _controller;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponentInParent<PlayerControls>();
        defaultPosY = transform.localPosition.y;
    }

    public void SetDefaultPositionForCamera(float y)
    {
        defaultPosY = y;
    }
    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(_input.GetPlayerMovement.x) > 0.1f || Mathf.Abs(_input.GetPlayerMovement.y) > 0.1f)
        {
            //Player is moving
            timer += Time.unscaledDeltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
