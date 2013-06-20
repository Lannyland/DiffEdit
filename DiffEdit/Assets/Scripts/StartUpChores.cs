using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class StartUpChores : MonoBehaviour {

    public Plane plane;
	public WWW www;
	// public Vector3[] vertices;

    // Use this for initialization
    void Start()
    {
        // Load terrain image file
        StartCoroutine(LoadMesh());
        
        // Scene Initialization
        SceneInit();

        // Test color map
        // MeshInit();
    }

    // Method to load terrain material into Projector
    IEnumerator LoadMesh()
    {
		// Sets material catelog TerrainImage texture to something so at least it's not empty.
		GameObject go = GameObject.Find("MaterialHolder");		
		go.GetComponent<MaterialCatelog>().catelog[4].mainTexture = go.GetComponent<MaterialCatelog>().catelog[5].mainTexture;
		
		// Deal with web vs. local files
		string fileLoc = ProjectConstants.strTerrainImage;
		if(fileLoc.Substring(0, 7) != "http://")
		{
			fileLoc = @"file:///" + fileLoc;				
		}
		
		// Load file
		www = new WWW(fileLoc);
	    yield return www;
	
	    // Stored loaded texture to a material in material catelog
		if(www.isDone && www !=null)
		{
			go.GetComponent<MaterialCatelog>().catelog[4].mainTexture = www.texture;
		}
		else
		{
			Debug.Log("Error loading file.");
		}
    }

    // Initialize all assets parameters to set scene ready
    private void SceneInit()
    {
        Camera.main.transform.GetComponent<DrawFreeLine>().enabled = false;
    }

    // Method to test color map
    private void MeshInit()
    {
        Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        float t = 0.0f;
        int index = 0;

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
//                if (i < 50)
//                {
//                    vertices[index].y = i * 2 / 100.0f;
//                }
//                else
//                {
//                    vertices[index].y = (50 - (i - 50)) / 50.0f;
//                }

                vertices[index].x = Convert.ToSingle(j)/10-5;
				vertices[index].y = 0;
				vertices[index].z = Convert.ToSingle(i)/10-5;
				
				HSLColor hslc = new HSLColor(i * 2.40f, 1f, 0.5f);
                Color c = hslc.ToRGBA();
                colors[index] = c;
                index++;
            }
            t += 0.1f;
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }	

	// Update is called once per frame
	void Update () {
	}
}
