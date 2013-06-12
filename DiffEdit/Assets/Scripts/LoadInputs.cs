using UnityEngine;
using System.Collections;

public class LoadInputs : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
        // Set input fields defualt strings
        UILabel MapFileLoad = GameObject.Find("inputMapFileLoad").GetComponent<UILabel>();
        MapFileLoad.text = Assets.Scripts.ProjectConstants.strMapFileLoad;
        UILabel MapFileSave = GameObject.Find("inputMapFileSave").GetComponent<UILabel>();
        MapFileSave.text = Assets.Scripts.ProjectConstants.strMapFileSave;
        UILabel TerrainImage = GameObject.Find("inputTerrainImage").GetComponent<UILabel>();
        TerrainImage.text = Assets.Scripts.ProjectConstants.strTerrainImage;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
