using UnityEngine;
using System.Collections;

public class SetDiffLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		// Based on who is clicked, set diff level accordingly
		// Also set projector material for brush color
	    GameObject go = GameObject.Find("MaterialHolder");
		Projector projector = GameObject.Find("BrushProjector").GetComponent<Projector>();

		switch (this.gameObject.name)
		{
		case "chkEasy":
			Assets.Scripts.ProjectConstants.diffLevel = 1;
			projector.material = go.GetComponent<MaterialCatelog>().catelog[0];
			break;
		case "chkMedium":
			Assets.Scripts.ProjectConstants.diffLevel = 2;
			projector.material = go.GetComponent<MaterialCatelog>().catelog[1];
			break;
		case "chkHard":
			Assets.Scripts.ProjectConstants.diffLevel = 3;
			projector.material = go.GetComponent<MaterialCatelog>().catelog[2];
			break;
		}
	}
}
