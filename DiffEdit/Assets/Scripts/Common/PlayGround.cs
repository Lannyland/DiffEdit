using System;
using UnityEngine;
using System.Collections;

public class PlayGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        TestColor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Method to clip terrain to a 100x100 grid (really just hide extra part)
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
                if (i % 2 == 0)
                {
                    //vertices[index].y = 1; 
                }
                if (i == 25 || i==45 || i==80)
                {
                    Debug.Log("Something");
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


}
