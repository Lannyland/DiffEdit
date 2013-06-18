using UnityEngine;
using System.Collections;
using rtwmatrix;
using Assets.Scripts;
using Assets.Scripts.Common;

public class MapLoadingHandler : MonoBehaviour {

	private MeshFilter unappliedMesh;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		Debug.Log ("here!");
		Debug.Log (Time.time);			

		if(this.gameObject.name=="btnLoad")
		{
			LoadButtonHandler();
		}
		if(this.gameObject.name=="btnSave")
		{
			SaveButtonHandler();
		}		
	}
	
	void LoadButtonHandler()
	{
		RtwMatrix mapIn = MISCLib.LoadMap(ProjectConstants.strMapFileLoad);
		Vector3[] vertices =  Assets.Scripts.Common.MISCLib.MatrixToArray(Assets.Scripts.Common.MISCLib.FlipTopBottom(mapIn));
		// Camera.main.GetComponent<StartUpChores>().vertices = vertices;
        Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
		mesh.colors = Assets.Scripts.Common.MISCLib.ApplyDiffColorMap(vertices);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();		
	}
	
	void SaveButtonHandler()
	{
		Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
		RtwMatrix mapOut = Assets.Scripts.Common.MISCLib.ArrayToMatrix(mesh.vertices);
		MISCLib.SaveMap(ProjectConstants.strMapFileSave, Assets.Scripts.Common.MISCLib.FlipTopBottom(mapOut));
	}
	
}