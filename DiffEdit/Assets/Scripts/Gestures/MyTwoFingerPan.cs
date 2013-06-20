using System;
using TouchScript.Events;
using TouchScript.Gestures;
using UnityEngine;
using System.Collections;

public class MyTwoFingerPan : MonoBehaviour
{
    public Vector2 gestureInput;
	public bool pan = false;
	public float speed = 50.0f;
	private Vector3 targetPosition; 
	
    private void Start()
    {
        GetComponent<TwoFingerPanGesture>().StateChanged += OnStateChanged;
    }

    private void Update()
    {
        //if(pan)
        //{			           	
        //     Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, speed * Time.deltaTime);
        //}
    }

    private void OnStateChanged(object sender, GestureStateChangeEventArgs e)
    {
        switch (e.State)
        {
            case Gesture.GestureState.Began:
            case Gesture.GestureState.Changed:
                var gesture = sender as TwoFingerPanGesture;
                // GameObject.Find("GUIText").GetComponent<UILabel>().text = "Panning";
				pan = true;
				// targetPosition = Camera.main.transform.localPosition - gesture.LocalDeltaPosition;
                gestureInput = gesture.FingerPositionEnd - gesture.FingerPositionBegin;
                // GameObject.Find("GUIText2").GetComponent<UILabel>().text = "Now: " + gesture.FingerPositionEnd.ToString();
                // GameObject.Find("GUIText").GetComponent<UILabel>().text = "Previous: " + gesture.FingerPositionBegin.ToString();
                break;	
			case Gesture.GestureState.Ended:
				pan = false;
                // GameObject.Find("GUIText").GetComponent<UILabel>().text = "Stopped";
				break;
			
        }
    }

}