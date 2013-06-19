using System;
using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Events;

public class MyScale : MonoBehaviour {
	
	private bool scale = false;
	private float speed = 0.0f;
	
	// Use this for initialization
	void Start () {
	GameObject.Find("GUIText").GetComponent<GUIText>().text = "Start";
        GetComponent<ScaleGesture>().StateChanged += onScaleStateChanged;
	}
	
	// Update is called once per frame
	void Update () {
		if(scale)
		{
			if(speed>1)
			{
				Camera.main.transform.Translate(Vector3.forward * Time.deltaTime * 5f * speed);
			}
			else
			{
				Camera.main.transform.Translate(Vector3.back * Time.deltaTime * 5f * speed);
			}
		}
	}
	
	private void onScaleStateChanged(object sender, GestureStateChangeEventArgs e)
    {
		GameObject.Find("GUIText").GetComponent<GUIText>().text = "State changed.";
		switch (e.State)
        {
            case Gesture.GestureState.Began:
            case Gesture.GestureState.Changed:
                var gesture = (ScaleGesture)sender;

                //if (Math.Abs(gesture.LocalDeltaRotation) > 0.01)
                //{
                //    targetRotation = Quaternion.AngleAxis(gesture.LocalDeltaRotation, gesture.WorldTransformPlane.normal) * targetRotation;
                //}
                GameObject.Find("GUIText").GetComponent<GUIText>().text = "Scaling";
				scale = true;
				speed = gesture.LocalDeltaScale;
				break;
			case Gesture.GestureState.Ended:
				scale = false;
				GameObject.Find("GUIText").GetComponent<GUIText>().text = "Stopped";
				break;
		}
    }	
}
