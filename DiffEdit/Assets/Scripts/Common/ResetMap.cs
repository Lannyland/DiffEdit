using UnityEngine;
using System.Collections;

public class ResetMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
    void OnMouseUpAsButton() {
        // Set everything to easy on plane
		MeshFilter meshFilter = GameObject.Find("Plane").GetComponent<MeshFilter>();
		Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        int index = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[index].y = 0;
			colors[index] = Color.white;
            index++;
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();		
		Debug.Log (Time.time);			
		
		// Reset collider as well
		// meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
		if(meshFilter && meshFilter.GetComponent<MeshCollider>())
		{
			Debug.Log (Time.time);			
			Debug.Log("Applying mesh collider.");
			meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
			meshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
			Debug.Log (Time.time);			
		}		
	}

}
