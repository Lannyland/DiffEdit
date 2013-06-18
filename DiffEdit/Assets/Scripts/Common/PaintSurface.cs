using UnityEngine;
using System;
using System.Collections;

public class PaintSurface : MonoBehaviour {

    public float radius = 10;
    public float pull = 10f;
    private MeshFilter unappliedMesh;

    public enum FallOff { Gauss, Linear, Needle }
    public FallOff fallOff = FallOff.Gauss;

    public enum MapType { Diff, Dist }
    public MapType mapType = MapType.Diff;

    static float GaussFalloff(float distance, float inRadius)
    {
        // return Mathf.Clamp01(Mathf.Pow(360.0f, -Mathf.Pow(distance / inRadius, 2.5f) - 0.01f));
        float part1 = 1.0f / (inRadius * Mathf.Pow(360.0f, 0.5f));
        float part2 = -(distance * distance) / (2.0f * inRadius * inRadius);
        return part1 * Mathf.Pow(Convert.ToSingle(Math.E), part2);
    }

    static float LinearFalloff (float distance, float inRadius) 
    {
	    return Mathf.Clamp01(1.0f - distance / inRadius);
    }

    static float NeedleFalloff(float distance, float inRadius)
    {
        return Mathf.Clamp01(1.0f - (distance * distance) / (inRadius * inRadius));
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	    // When no button is pressed we update the mesh collider
	    if (!Input.GetMouseButton (0))
	    {
		    // Apply collision mesh when we let go of button
		    ApplyMeshCollider();
		    return;
	    }

        // When mouse button is pressed
        // First set brush size
        this.gameObject.GetComponent<PaintSurface>().radius = Assets.Scripts.ProjectConstants.brushSize/2;

        // Did we hit the surface?
	    RaycastHit hit;
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (Physics.Raycast (ray, out hit))
	    {
            Debug.Log("hit something");
		    MeshFilter filter = hit.collider.GetComponent<MeshFilter>();
		    if (filter)
		    {
			    // Don't update mesh collider every frame since physX
			    // does some heavy processing to optimize the collision mesh.
			    // So this is not fast enough for real time updating every frame
			    if (filter != unappliedMesh)
			    {
				    ApplyMeshCollider();
				    unappliedMesh = filter;
			    }
			
			    // Deform mesh
			    Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
			    DeformMesh(filter.mesh, relativePoint, pull * Time.deltaTime, radius);
		    }
	    }
	}

    void ApplyMeshCollider()
    {
        if (unappliedMesh && unappliedMesh.GetComponent<MeshCollider>())
        {
            unappliedMesh.GetComponent<MeshCollider>().sharedMesh = unappliedMesh.mesh;
        }
        unappliedMesh = null;
    }

    void DeformMesh(Mesh mesh, Vector3 position, float power, float inRadius)
    {
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        float sqrRadius = inRadius * inRadius;
        float sqrMagnitude, distance, falloff;

        // Calculate averaged normal of all surrounding vertices	
        var averageNormal = Vector3.zero;
        for (int i = 0; i < vertices.Length; i++)
        {
            sqrMagnitude = (vertices[i] - position).sqrMagnitude;
            // Early out if too far away
            if (sqrMagnitude > sqrRadius)
                continue;

            distance = Mathf.Sqrt(sqrMagnitude);
            switch (fallOff)
            {
                case FallOff.Gauss:
                    falloff = GaussFalloff(distance, inRadius);
                    break;
                case FallOff.Needle:
                    falloff = NeedleFalloff(distance, inRadius);
                    break;
                default:
                    falloff = LinearFalloff(distance, inRadius);
                    break;
            }
            averageNormal += falloff * normals[i];
        }
        averageNormal = averageNormal.normalized;

        // Deform vertices along averaged normal
        for (int i = 0; i < vertices.Length; i++)
        {
            sqrMagnitude = (vertices[i] - position).sqrMagnitude;
            // Early out if too far away
            if (sqrMagnitude > sqrRadius)
                continue;

            distance = Mathf.Sqrt(sqrMagnitude);
            switch (fallOff)
            {
                case FallOff.Gauss:
                    falloff = GaussFalloff(distance, inRadius);
                    break;
                case FallOff.Needle:
                    falloff = NeedleFalloff(distance, inRadius);
                    break;
                default:
                    falloff = LinearFalloff(distance, inRadius);
                    break;
            }

            // vertices[i] += averageNormal * falloff * power;
            vertices[i].y += falloff * power;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

}
