using UnityEngine;
using System.Collections;
using rtwmatrix;
using Assets.Scripts;
using Assets.Scripts.Common;

public class MapLoadingHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		if(this.gameObject.name=="btnNew")
		{
			NewButtonHandler();
		}
		if(this.gameObject.name=="btnLoad")
		{
			LoadButtonHandler();
		}
		if(this.gameObject.name=="btnSave")
		{
			SaveButtonHandler();
		}		
	}
	
	void NewButtonHandler()
	{
		// Set everything to easy on plane
		int width = ProjectConstants.intMapWith;
		int height = ProjectConstants.intMapHeight;
		
        Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        float t = 0.0f;
        int index = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[index].y = 0;
			colors[index] = Color.blue;
            index++;
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();		
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