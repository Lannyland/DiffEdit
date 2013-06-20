using System;
using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Events;

public class MyRotate : MonoBehaviour {
	private bool rotate = false;
	private float speed = 0.0f;
	
	// Use this for initialization
	void Start () {
	    // GameObject.Find("GUIText").GetComponent<GUIText>().text = "Start";
        GetComponent<RotateGesture>().StateChanged += onRotateStateChanged;
	}
	
	// Update is called once per frame
	void Update () {
		if(rotate)
		{
			Camera.main.transform.Translate(Vector3.left * Time.deltaTime * 5f * speed);
		}
	}

    private void onRotateStateChanged(object sender, GestureStateChangeEventArgs e)
    {
		// GameObject.Find("GUIText").GetComponent<GUIText>().text = "State changed.";
		switch (e.State)
        {
            case Gesture.GestureState.Began:
            case Gesture.GestureState.Changed:
                var gesture = (RotateGesture)sender;

                //if (Math.Abs(gesture.LocalDeltaRotation) > 0.01)
                //{
                //    targetRotation = Quaternion.AngleAxis(gesture.LocalDeltaRotation, gesture.WorldTransformPlane.normal) * targetRotation;
                //}
                // GameObject.Find("GUIText").GetComponent<GUIText>().text = "Rotating";
				rotate = true;
				speed = gesture.LocalDeltaRotation;
                break;
			case Gesture.GestureState.Ended:
				rotate = false;
				// GameObject.Find("GUIText").GetComponent<GUIText>().text = "Stopped";
				break;
		}
    }
}
