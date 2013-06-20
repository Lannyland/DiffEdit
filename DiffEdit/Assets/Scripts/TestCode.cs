using UnityEngine;
using System.Collections;

public class TestCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    // Build vertices
        Vector2[] vertices = new Vector2[10000];
        int index = 0;
        for (float y = -5.0f; y < 4.9f; y += 0.1f)
        {
            for (float x = -5.0f; x < 4.9f; x += 0.1f)
            {
                vertices[index] = new Vector2(x, y);
                index++;
            }
        }
        Debug.Log("index=" + index);

        Vector2[] polygonPoints = new Vector2[3];
        polygonPoints[0] = new Vector2(-5, -5);
        polygonPoints[1] = new Vector2(-5, -4);
        polygonPoints[2] = new Vector2(-4, -5);

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 curVertex = new Vector2(vertices[i].x, vertices[i].y);

            if (Assets.Scripts.Common.MISCLib.PointInPolygon(curVertex, polygonPoints))
            {
                Debug.Log("Point " + curVertex.ToString() + "inside polygon.");
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
