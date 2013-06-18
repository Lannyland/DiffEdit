using UnityEngine;
using System;
using System.Collections;

public class PaintSurface : MonoBehaviour {

    public float radius = 10;
    public float pull = 100f;
	private float sigma = 0.0f;
    private MeshFilter unappliedMesh;
	
    public enum FallOff { Gauss, Linear, Needle, Disc }
    public FallOff fallOff = FallOff.Disc;

    public enum EditMode { Raise, Lower, Erase }
    public EditMode editMode = EditMode.Raise;

    static float GaussFalloff(float x, float z, float mu_x, float mu_z, float sigma)
    { 
        // return Mathf.Clamp01(Mathf.Pow(360.0f, -Mathf.Pow(distance / inRadius, 2.5f) - 0.01f));
        float sigma_2 = sigma*sigma;
		float part1 = 1.0f / (360.0f * sigma_2);
        float part2 = -(Mathf.Pow(x-mu_x,2)+Mathf.Pow(z-mu_z,2))/(2*sigma_2);
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

    static float DiscFalloff()
    {
		return Convert.ToSingle(Assets.Scripts.ProjectConstants.diffLevel-1);
    }
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	    // When no button is pressed we update the mesh collider
	    if (!Input.GetMouseButton (0))
	    {
		    ApplyMeshCollider();
		    return;
	    }
		
		// When mouse button is pressed
        // First set brush size
        switch (fallOff)
		{
		case FallOff.Gauss:
			this.gameObject.GetComponent<PaintSurface>().sigma = Assets.Scripts.ProjectConstants.brushSize/30.0f;
            this.gameObject.GetComponent<PaintSurface>().pull = Mathf.Pow(Convert.ToSingle(Assets.Scripts.ProjectConstants.brushSize), 2.0f) / 2;
			this.gameObject.GetComponent<PaintSurface>().radius = 15.0f;	
			break;
		case FallOff.Linear:
			radius = Assets.Scripts.ProjectConstants.brushSize/20.0f;			
			break;
		case FallOff.Needle:
			radius = Assets.Scripts.ProjectConstants.brushSize/20.0f;						
			break;
		default:
			radius = Assets.Scripts.ProjectConstants.brushSize/20.0f;						
			break;
		}

        Debug.Log(Assets.Scripts.ProjectConstants.brushSize.ToString());	
        // Did we hit the surface?
	    RaycastHit hit;
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (Physics.Raycast (ray, out hit, 100))
	    {
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
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
                Debug.Log("relativePoint=" + relativePoint.x + "," + relativePoint.y + "," + relativePoint.z);
                DeformMesh(filter.mesh, relativePoint, pull * Time.deltaTime, radius);
		    }
	    }
	}

    void ApplyMeshCollider()
    {
        if (unappliedMesh && unappliedMesh.GetComponent<MeshCollider>())
        {
            unappliedMesh.GetComponent<MeshCollider>().sharedMesh = null;
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

//        // Calculate averaged normal of all surrounding vertices	
//        var averageNormal = Vector3.zero;
//        for (int i = 0; i < vertices.Length; i++)
//        {
//            sqrMagnitude = (vertices[i] - position).sqrMagnitude;
//            // Early out if too far away
//            if (sqrMagnitude > sqrRadius)
//                continue;
//
//            distance = Mathf.Sqrt(sqrMagnitude);
//            switch (fallOff)
//            {
//                case FallOff.Gauss:
//                    falloff = GaussFalloff(distance, inRadius);
//                    break;
//                case FallOff.Needle:
//                    falloff = NeedleFalloff(distance, inRadius);
//                    break;
//                default:
//                    falloff = LinearFalloff(distance, inRadius);
//                    break;
//            }
//            averageNormal += falloff * normals[i];
//        }
//        averageNormal = averageNormal.normalized;

        // Deal with different modes
        float sign = 1.0f;
        switch (editMode)
        {
            case EditMode.Lower:
                sign = -1.0f;
                break;
            case EditMode.Erase:
                sign = 0.0f;
                break;
            default:
                sign = 1.0f;
                break;
        }

        // Get max height of mesh for color map purposes.
        Color[] colors = mesh.colors;
        Color c = Color.blue;
        float max = 0.0f;
        if (fallOff != FallOff.Disc)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].y > max)
                {
                    max = vertices[i].y;
                }
            }
        }
        // Deform vertices along averaged normal
        for (int i = 0; i < vertices.Length; i++)
        {
            sqrMagnitude = (vertices[i].x - position.x) * (vertices[i].x - position.x) + (vertices[i].z - position.z) * (vertices[i].z - position.z);
            // Early out if too far away
            if (sqrMagnitude > sqrRadius)
                continue;

            distance = Mathf.Sqrt(sqrMagnitude);
            switch (fallOff)
            {
                case FallOff.Gauss:
                    falloff = GaussFalloff(vertices[i].x, vertices[i].z, position.x, position.z, sigma);
                    break;
                case FallOff.Needle:
                    falloff = NeedleFalloff(distance, inRadius);
                    break;
                case FallOff.Linear:
                    falloff = LinearFalloff(distance, inRadius);
                    break;
                default:
                    falloff = DiscFalloff();
                    break;
            }
            // vertices[i] += averageNormal * falloff * power;				
			
			// Set color for vertex

            if (editMode == EditMode.Erase)
            {
                vertices[i].y = 0;
                c = Color.blue;
            }
            else if (fallOff == FallOff.Disc)
            {
                vertices[i].y = falloff;
                c = Assets.Scripts.Common.MISCLib.HeightToDiffColor(vertices[i].y);
            }
            else
            {
                float temp = vertices[i].y + falloff * power * sign;
                vertices[i].y = Mathf.Clamp(temp, 0, 10.0f);
                c = Assets.Scripts.Common.MISCLib.HeightToDistColor(vertices[i].y, max);
            }
            colors[i] = c;
        }
        mesh.colors = colors;			
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

}
