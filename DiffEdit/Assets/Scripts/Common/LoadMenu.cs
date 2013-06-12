using UnityEngine;
using System.Collections;

public class LoadMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // When Cancel button from Settings scene is clicked
    void OnClick()
    {
        Application.LoadLevel("Menu");
    }

}
