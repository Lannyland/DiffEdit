using UnityEngine;
using System.Collections;

public class ResetMap : MonoBehaviour {

	private MeshFilter unappliedMesh;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//	    // When no button is pressed we update the mesh collider
//	    if (!Input.GetMouseButton (0))
//	    {
//		    ApplyMeshCollider();
//		    return;
//	    }
//
//		
//		// Set everything to easy on plane
//		MeshFilter meshFilter = GameObject.Find("Plane").GetComponent<MeshFilter>();
//		unappliedMesh = meshFilter;
//		Mesh mesh = meshFilter.mesh;
//        Vector3[] vertices = mesh.vertices;
//        Color[] colors = new Color[vertices.Length];
//        int index = 0;
//
//        for (int i = 0; i < vertices.Length; i++)
//        {
//            vertices[index].y = 0;
//			// colors[index] = Color.blue;
//            index++;
//        }
//        mesh.vertices = vertices;
//        // mesh.colors = colors;
//        mesh.RecalculateBounds();
//        mesh.RecalculateNormals();		
//		Debug.Log (Time.time);			
//		
////		// Reset collider as well
////		// meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
////		if(meshFilter && meshFilter.GetComponent<MeshCollider>())
////		{
////			Debug.Log (Time.time);			
////			Debug.Log("Applying mesh collider.");
////			meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
////			meshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
////			Debug.Log (Time.time);			
////		}		
//	}
//	
//    void ApplyMeshCollider()
//    {
//        if (unappliedMesh && unappliedMesh.GetComponent<MeshCollider>())
//        {
//			Debug.Log (Time.time);
//			Debug.Log("Applying mesh collider.");
//			unappliedMesh.GetComponent<MeshCollider>().sharedMesh = null;
//            unappliedMesh.GetComponent<MeshCollider>().sharedMesh = unappliedMesh.mesh;
//			Debug.Log (Time.time);
//        }		
//        unappliedMesh = null;
//    }
	}
	
    void OnMouseUpAsButton() {
        // Set everything to easy on plane
		MeshFilter meshFilter = GameObject.Find("Plane").GetComponent<MeshFilter>();
		unappliedMesh = meshFilter;
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
        // mesh.colors = colors;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();		
		Debug.Log (Time.time);			
		
		// Reset collider as well
		// meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
		if(meshFilter && meshFilter.GetComponent<MeshCollider>())
		{
			Debug.Log (Time.time);			
			Debug.Log("Applying mesh collider.");
			// meshFilter.GetComponent<MeshCollider>().sharedMesh = null;
			meshFilter.GetComponent<MeshCollider>().sharedMesh = mesh;
			Debug.Log (Time.time);			
		}		
	}

}
