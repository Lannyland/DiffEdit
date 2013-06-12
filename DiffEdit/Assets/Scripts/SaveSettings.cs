using UnityEngine;
using System.Collections;

public class SaveSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Save settings and return to Menu scene
    void OnClick()
    {
        // Save settings
        UILabel MapFileLoad = GameObject.Find("inputMapFileLoad").GetComponent<UILabel>();
        Assets.Scripts.ProjectConstants.strMapFileLoad = MapFileLoad.text;
        UILabel MapFileSave = GameObject.Find("inputMapFileSave").GetComponent<UILabel>();
        Assets.Scripts.ProjectConstants.strMapFileSave = MapFileSave.text;
        UILabel TerrainImage = GameObject.Find("inputTerrainImage").GetComponent<UILabel>();
        Assets.Scripts.ProjectConstants.strTerrainImage = TerrainImage.text;

        // Load Menu scene
        Application.LoadLevel("Menu");
    }
}
