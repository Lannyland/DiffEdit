using UnityEngine;
using System.Collections;

public class LoadSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // When Settings button in Menu scene is clicked
    void OnClick()
    {
        Application.LoadLevel("Settings");
    }
}
