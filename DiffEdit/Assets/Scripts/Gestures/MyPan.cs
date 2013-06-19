using System;
using TouchScript.Events;
using TouchScript.Gestures;
using UnityEngine;
using System.Collections;

public class MyPan : MonoBehaviour
{

	private bool pan = false;
	private float speed = 10.0f;
	private Vector3 targetPosition; 
	
    private void Start()
    {
			GameObject.Find("GUIText").GetComponent<GUIText>().text = "Start";

        GetComponent<PanGesture>().StateChanged += OnStateChanged;
    }

    private void Update()
    {
		if(pan)
		{			           	
	         Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPosition, speed * Time.deltaTime);
		}
    }

    private void OnStateChanged(object sender, GestureStateChangeEventArgs e)
    {
        switch (e.State)
        {
            case Gesture.GestureState.Began:
            case Gesture.GestureState.Changed:
                var gesture = sender as PanGesture;
                GameObject.Find("GUIText").GetComponent<GUIText>().text = "Panning";
				pan = true;
				targetPosition = Camera.main.transform.localPosition - gesture.LocalDeltaPosition;
                break;	
			case Gesture.GestureState.Ended:
				pan = false;
				GameObject.Find("GUIText").GetComponent<GUIText>().text = "Stopped";
				break;
			
        }
    }

}