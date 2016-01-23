using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

	//mouse movement
	Vector2 _mouseAbsolute;
	Vector2 _smoothMouse;

	public Vector2 clampInDegrees = new Vector2(360, 180); //limit y rotation
	public bool lockCursor; //should cursor be locked to screen center
	public Vector2 sensitivity = new Vector2(2, 2); //how fast it should move
	public Vector2 smoothing = new Vector2(3, 3); //smoothing amount
	public Vector2 targetDirection;
	public Vector2 targetCharacterDirection;

	//movement
	public float base_speed; //base forward speed
	public float strafe_speed; //base sideways speed
	public float crawl_mod; //modify to crawl
	public float run_mod; //modify to run
	public float jump_speed;

	private bool jumping;

	private CharacterController controller;

	// Use this for initialization
	void Start () {
		targetDirection = transform.localRotation.eulerAngles;
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Handle_Input();
		Handle_Ground();
	}

	void Handle_Ground()
	{
		if(!controller.isGrounded)
		{
			float grav_speed = 9.81f * Time.deltaTime;
			controller.Move(new Vector3(0, -grav_speed, 0));
		}
	}

	void Handle_Input()
	{
		if (controller.isGrounded)
		{
			//movement
			//vertical axis (W S)
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
			{
				controller.Move(transform.forward * ((Input.GetAxis("Vertical") * base_speed) * Time.deltaTime));
			}
			//horizontal axis (A D)
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
			{
				controller.Move(transform.right * ((Input.GetAxis("Horizontal") * strafe_speed) * Time.deltaTime));
			}
		}
		//mouse
		Screen.lockCursor = lockCursor;

		// Allow the script to clamp based on a desired target value.
		var targetOrientation = Quaternion.Euler(targetDirection);
		var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

		// Get raw mouse input for a cleaner reading on more sensitive mice.
		var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		// Scale input against the sensitivity setting and multiply that against the smoothing value.
		mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

		// Interpolate mouse movement over time to apply smoothing delta.
		_smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
		_smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

		// Find the absolute mouse movement value from point zero.
		_mouseAbsolute += _smoothMouse;

		// Clamp and apply the local x value first, so as not to be affected by world transforms.
		if (clampInDegrees.x < 360)
			_mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

		var xRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right);
		transform.localRotation = xRotation;

		// Then clamp and apply the global y value.
		if (clampInDegrees.y < 360)
			_mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

		transform.localRotation *= targetOrientation;


		var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
		transform.localRotation *= yRotation;
	}
}
