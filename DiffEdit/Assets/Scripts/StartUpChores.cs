using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class StartUpChores : MonoBehaviour {

    public Terrain terrain;
    public Plane plane;

    // Use this for initialization
    void Start()
    {
        // Load terrain image file
        LoadTerrain();
        
        // Scene Initialization
        SceneInit();

        // Test color map
        TestColor();
    }

    // Method to load terrain material into Projector
    IEnumerator LoadTerrain()
    {
        WWW www = new WWW(@"file:///" + ProjectConstants.strTerrainImage);
        yield return www;

        Projector go = GameObject.Find("ProjectorTerrain").GetComponent<Projector>();
        go.material.mainTexture = www.texture;
    }

    // Initialize all assets parameters to set scene ready
    private void SceneInit()
    {
        
    }

    // Method to test color map
    private void TestColor()
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
                if (i < 50)
                {
                    vertices[index].y = i * 2 / 100.0f;
                }
                else
                {
                    vertices[index].y = (50 - (i - 50)) / 50.0f;
                }

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
