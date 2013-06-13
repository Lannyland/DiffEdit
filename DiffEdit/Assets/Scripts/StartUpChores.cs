using UnityEngine;
using System.Collections;

public class StartUpChores : MonoBehaviour {

    public Terrain terrain;
    public Plane plane;

    // Use this for initialization
    void Start()
    {
        // Clip Terrain to only who 100x100 grid
        ClipTerrain();
    }

    // Method to clip terrain to a 100x100 grid (really just hide extra part)
    private void ClipTerrain()
    {
        float[, ,] alphaMap = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);
        for (int x = 0; x < terrain.terrainData.alphamapHeight; x++)
        {
            for (int y = 0; y < terrain.terrainData.alphamapWidth; y++)
            {
                alphaMap[x, y, 0] = 0;
                alphaMap[x, y, 1] = 1;
            }

        }

        terrain.terrainData.SetAlphamaps(0, 0, alphaMap);
        //Mesh mesh = GameObject.Find("TestPlane").GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices = mesh.vertices;
        //Color[] colors = new Color[vertices.Length];
        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    // colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);
        //    if(i%3==0)
        //    {
        //        colors[i] = Color.red;
        //    }
        //    if(i%3==1)
        //    {
        //        colors[i] = Color.blue;
        //    }
        //    if(i%3==2)
        //    {
        //        colors[i] = Color.green;
        //    }

        //}
        //mesh.colors = colors;
        
    }
	
	// Update is called once per frame
	void Update () {
	}
}