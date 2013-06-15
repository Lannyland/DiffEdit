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
        UILabel label = GameObject.Find("lblShowTerrain").GetComponent<UILabel>(); 
		
        if (label.text == "Show Terrain")
        {
            label.text = "Hide Terrain";
        }
        else
        {
            label.text = "Show Terrain";
        }        
    }
}
