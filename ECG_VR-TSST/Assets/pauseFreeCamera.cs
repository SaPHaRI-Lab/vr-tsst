using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class pauseFreeCamera : MonoBehaviour {
    
    float turnSpeed = 2.0f;		// Speed of camera turning when mouse moves in along an axis
    float panSpeed = 1.0f;		// Speed of the camera when being panned
    float zoomSpeed = 1.0f;		// Speed of the camera going back and forth

    private Vector2 mouseOrigin;	// Position of cursor when mouse dragging starts
    private bool isPanning;		// Is the camera being panned?
    private bool isRotating;	// Is the camera being rotated?
    private bool isZooming;		// Is the camera zooming?

    [HideInInspector]
    public bool usingPauseFreeCamera = false;
    [HideInInspector]
    public bool usingSlowMotion = false;

    public MonoBehaviour[] cameraScriptToDisable;

    float originalTimeScale;

    Vector3 originalCameraPos;
    Quaternion originalCameraRotation;

    public Key toggleScreenshotModeKey = Key.F10;
    public Key nextFrameKey = Key.F11;
    public Key toggleSlowMotionModeKey = Key.F12;

    Transform originalParent;

    private void switchCameraMode()
    {
        if (usingPauseFreeCamera)
        {
            usingPauseFreeCamera = false;
            Time.timeScale = 1;

            foreach (var script in cameraScriptToDisable)
            {
                script.enabled = true;
            }
            transform.localPosition = originalCameraPos;
            transform.localRotation = originalCameraRotation;
            transform.parent = originalParent;
        }
        else
        {
            foreach (var script in cameraScriptToDisable)
            {
                script.enabled = false;
            }
            originalParent = transform.parent;
            transform.parent = null;
            originalCameraRotation = transform.localRotation;
            originalCameraPos = transform.localPosition;
            Time.timeScale = 0;
            usingPauseFreeCamera = true;
        }
    }

    IEnumerator AdvanceOneFrame()
    {
        Time.timeScale = 0.9f;
        yield return null;
        Time.timeScale = 0;
    }

    void Update()
        {
            if (Keyboard.current[nextFrameKey].wasPressedThisFrame)
            {
                StartCoroutine(AdvanceOneFrame());
            }

            if(Keyboard.current[toggleScreenshotModeKey].wasPressedThisFrame) {
                switchCameraMode();
            }

            if (Keyboard.current[Key.F12].wasPressedThisFrame)
            {
                usingSlowMotion = !usingSlowMotion;
                if (usingSlowMotion == false)
                    Time.timeScale = 1;
            }

            if (usingSlowMotion)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame)
                {
                    usingSlowMotion = false;
                    if (!usingPauseFreeCamera)
                        switchCameraMode();
                }
                if (Mouse.current.scroll.magnitude > 0.1f)
                {
                    Time.timeScale = Mathf.Clamp(Time.timeScale + 0.35f, 0.1f, 1);
                }
                else if (Mouse.current.scroll.magnitude < -0.1f)
                {
                    Time.timeScale = Mathf.Clamp(Time.timeScale - 0.35f, 0.1f, 1);
                }
            }

            if (!usingPauseFreeCamera)
                return;
	
		if(Mouse.current.leftButton.wasPressedThisFrame)
		{
			mouseOrigin = Mouse.current.position.ReadValue();
			isRotating = true;
		}
		
		if(Mouse.current.rightButton.wasPressedThisFrame)
		{
			mouseOrigin = Mouse.current.position.ReadValue();
			isPanning = true;
		}
		
		if(Mouse.current.middleButton.wasPressedThisFrame)
		{
			mouseOrigin = Mouse.current.position.ReadValue();
			isZooming = true;
		}
		
		if (!Mouse.current.leftButton.wasPressedThisFrame) isRotating=false;
		if (!Mouse.current.rightButton.wasPressedThisFrame) isPanning=false;
		if (!Mouse.current.middleButton.wasPressedThisFrame) isZooming=false;
		
		if (isRotating)
		{
	        	Vector3 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - mouseOrigin);
                transform.RotateAround(transform.localPosition, transform.right, -pos.y * turnSpeed);
                transform.RotateAround(transform.localPosition, Vector3.up, pos.x * turnSpeed);
		}

		if (isPanning)
		{
	        	Vector3 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - mouseOrigin);
	        	Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
	        	transform.Translate(move, Space.Self);
		}

		if (isZooming)
		{
	        	Vector3 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue() - mouseOrigin);
 
	        	Vector3 move = pos.y * zoomSpeed * transform.forward; 
	        	transform.Translate(move, Space.World);
		}
    }
}