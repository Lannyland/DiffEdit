using System;
using UnityEngine;
using System.Collections;

public class PlayGround : MonoBehaviour {
	
	public Transform projector;		
		
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit = new RaycastHit();
	  	if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
			//Debug.Log ("hit.point is " + hit.point.ToString() + " Projector.transform.position is " + projector.transform.position.ToString());
			projector.transform.position = hit.point + new Vector3(0,5,0);
		}
	}
}
