using UnityEngine;
using System.Collections;

public class ShowHideTerrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // When clicked
    void OnClick()
    {
        // First change label back and forth
		// Then change material on plane to show or hide terrain image		
        UILabel label = GameObject.Find("lblShowTerrain").GetComponent<UILabel>(); 
		GameObject go = GameObject.Find("MaterialHolder");
		MeshFilter mf = GameObject.Find("Plane").GetComponent<MeshFilter>();					
		
        if (label.text == "Show Terrain")
        {
            label.text = "Hide Terrain";
			mf.renderer.material = go.GetComponent<MaterialCatelog>().catelog[4];
        }
        else
        {
            label.text = "Show Terrain";
			mf.renderer.material = go.GetComponent<MaterialCatelog>().catelog[3];
        }        
    }
}
