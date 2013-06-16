using UnityEngine;
using System.Collections;
using System;

public class BrushSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// When value changes
	void OnSliderChange(float value)
	{
		// Change display text
		UILabel label = GameObject.Find("lblBrushSize").GetComponent<UILabel>();
		int v = Convert.ToInt16(value*10);
		label.text = v.ToString();
		
		// Remember current brush size
		Assets.Scripts.ProjectConstants.brushSize = v;
		
		// Chagne brush size
		Projector projector = GameObject.Find("BrushProjector").GetComponent<Projector>();
		projector.orthoGraphicSize = value;
	}
}
