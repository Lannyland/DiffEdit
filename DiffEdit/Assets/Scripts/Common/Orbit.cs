using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Keyboard Orbit")]

public class Orbit : MonoBehaviour
{
	public enum Gesture { Pan, Rotate }	
	public enum InputDevice { Keyboard, Mouse, Finger }
	
	public Transform target;
	public InputDevice inputDevice = InputDevice.Mouse;
	public Gesture gesture = Gesture.Pan;
	public float distanceMin = 1.0f;
	public float distanceMax = 15.0f;
	public float distanceInitial = 12.5f;
	public float scrollSpeed = 2.0f;
	
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	
	public int yMinLimit = 0;
	public int yMaxLimit = 80;
	
	private float x = 0.0f;
	private float y = 0.0f;
	private float distanceCurrent = 0.0f;
	
	private float h = 0.0f;
	private float v = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
	    Vector3 angles = transform.eulerAngles;
	    x = angles.y;
	    y = angles.x;
	
		distanceCurrent = distanceInitial;
	
		// Make the rigid body not change rotation
	   	if (rigidbody)
			rigidbody.freezeRotation = true;

	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (target) 
		{
	 		distanceCurrent -= GetInput(inputDevice, "Mouse ScrollWheel") * scrollSpeed;

			distanceCurrent = Mathf.Clamp(distanceCurrent, distanceMin, distanceMax);
			
			if(gesture == Gesture.Rotate)
			{
				x += GetInput(inputDevice, "Horizontal") * xSpeed * 0.02f;
		        y -= GetInput(inputDevice, "Vertical") * ySpeed * 0.02f;
			}
			else
			{
				h -= GetInput(inputDevice, "Horizontal") * xSpeed * 0.0001f * distanceCurrent;
				v -= GetInput(inputDevice, "Vertical") * ySpeed * 0.0001f * distanceCurrent;
			}			
			
	 		y = ClampAngle(y, yMinLimit, yMaxLimit);
	 		       
	        Quaternion rotation = Quaternion.Euler(y, x, 0);
	        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distanceCurrent) + target.position;
			
	        transform.rotation = rotation;
	        transform.position = position;
			transform.Translate(h, v, 0);
	    }
	}
	
	float GetInput(InputDevice id, string s)
	{
		float result = 0.0f;
		switch(id)
		{
		case InputDevice.Keyboard:
			result = Input.GetAxis(s);
			break;
		case InputDevice.Mouse:
			float scale = 1.0f;
			if(gesture == Gesture.Rotate)
			{
				scale = 0.5f;
			}
			else
			{
				scale = 1.0f;
			}
			switch (s)
			{
			case "Horizontal":
				result = Input.GetAxis("Mouse X") * scale * distanceCurrent;
				break;
			case "Vertical":
				result = Input.GetAxis("Mouse Y") * scale * distanceCurrent;
				break;
			case "Mouse ScrollWheel":
				result = Input.GetAxis(s);
				break;
			}
			break;
		case InputDevice.Finger:
			// To be replaced by gesture finger location
			if(s == "Horizontal")
			{
				result = Input.GetAxis("Mouse X") ;
			}
			if(s == "Vertical")
			{
				result = Input.GetAxis("Mouse Y") ;
			}
			break;
		}
		return result;						
	}
	
	static float ClampAngle (float angle, float min, float max) 
	{
		if (angle < -360f)
			angle += 360f;
		if (angle > 360f)
			angle -= 360f;
		return Mathf.Clamp (angle, min, max);
	}
}

