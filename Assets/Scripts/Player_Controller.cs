﻿using UnityEngine;
using System.Collections;

public class Player_Controller : MonoBehaviour {

    public Texture2D crosshair;

	public grapple_hook_control grapple_hook;
    PlayerHealth health;

	//mouse movement
	Vector2 _mouseAbsolute;
	Vector2 _smoothMouse;

	public Vector2 clampInDegrees = new Vector2(360, 180); //limit y rotation
	public bool lockCursor; //should cursor be locked to screen center
	public Vector2 sensitivity = new Vector2(2, 2); //how fast it should move
	public Vector2 smoothing = new Vector2(3, 3); //smoothing amount
	public Vector2 targetDirection;
	public Vector2 targetCharacterDirection;
    CursorLockMode wanted_mode;
    
	//movement
	public float base_speed; //base forward speed
	public float strafe_speed; //base sideways speed
	public float crawl_mod; //modify to crawl
	public float run_mod; //modify to run
	public float initial_jump_force; //how hard does the player jump
	private float current_jump_force;
	private float gravity = 9.81f;
    public float launch_velocity;

	private CharacterController controller;

	// Use this for initialization
	void Start () {
        wanted_mode = CursorLockMode.Locked;
		targetDirection = transform.localRotation.eulerAngles;
		controller = GetComponent<CharacterController>();
        health = GetComponent<PlayerHealth>();
        Go_To_Spawn();
	}
	
	// Update is called once per frame
	void Update () {
		Handle_Input();
		Handle_Ground();
        if(health.getHealth() <= 0)
        {
            Go_To_Spawn();
            health.setHealth(100);
        }
	}

	void Handle_Ground()
	{
       if (!grapple_hook.hook_shot)
        {
            if (!controller.isGrounded)
            {
                float grav_speed = gravity * Time.deltaTime;
                controller.Move(new Vector3(0, -grav_speed, 0));
            }
            else
            {
                current_jump_force = 0.0f;
            }
        }
	}

	void Handle_Input()
	{
		//movement
		if (controller.isGrounded)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				current_jump_force = initial_jump_force;
			}
		}

        if(Input.GetKey(KeyCode.Escape))
        {
            wanted_mode = CursorLockMode.None;
        }

		//vertical axis (W S)
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
		{
			float current_speed = base_speed;
			if (controller.isGrounded && Input.GetKey(KeyCode.LeftControl))
			{
				current_speed += crawl_mod;
			}
			else if (controller.isGrounded && Input.GetKey(KeyCode.LeftShift))
			{
				current_speed += run_mod;
			}
			//use transform.forward without y axis
			Vector3 movement = transform.forward * ((Input.GetAxis("Vertical") * current_speed) * Time.deltaTime);
			movement.y = 0;
			controller.Move(movement);
		}
		//horizontal axis (A D)
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			float current_strafe_speed = strafe_speed;
			if (controller.isGrounded && Input.GetKey(KeyCode.LeftControl))
			{
				current_strafe_speed += crawl_mod;
			}
			else if (controller.isGrounded && Input.GetKey(KeyCode.LeftShift))
			{
				current_strafe_speed += run_mod;
			}
			//use transform.right without y axis
			Vector3 movement = transform.right * ((Input.GetAxis("Horizontal") * current_strafe_speed) * Time.deltaTime);
			movement.y = 0;
			controller.Move(movement);
		}

		float up_amount = current_jump_force * Time.deltaTime;
        if (!grapple_hook.hook_shot || grapple_hook.grabbed_object == null)
        {
            controller.Move(new Vector3(0, up_amount, 0));
        }
        else
        {
            current_jump_force = launch_velocity;
        }
		current_jump_force -= (gravity * Time.deltaTime);


		
		//mouse
        Cursor.lockState = wanted_mode;

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

    void Go_To_Spawn()
    {
        transform.position = GameObject.FindGameObjectWithTag("Spawn_Point").transform.position;
    }

    void OnGUI()
    {
        float x = (Screen.width / 2) - (crosshair.width / 2);
        float y = (Screen.height / 2) - (crosshair.height / 2);
        GUI.DrawTexture(new Rect(x, y, crosshair.width, crosshair.height), crosshair);
    }
}
