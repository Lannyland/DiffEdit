using UnityEngine;
using Vectrosity;
using System.Collections;
using System;

public class DrawFreeLine : MonoBehaviour {

    public Material lineMaterial;
    public int maxPoints = 1000;
    public float lineWidth = 4.0f;
    public int minPixelMove = 1;	// Must move at least this many pixels per sample for a new segment to be recorded
    public bool useEndCap = false;
    public Texture2D capTex;
    public Material capMaterial;

    private Vector2[] linePoints;
    private VectorLine line;
    private int lineIndex = 0;
    private Vector2 previousPosition;
    private int sqrMinPixelMove;
    private bool canDraw = false;

	// Use this for initialization
	void Start () {
        if (useEndCap)
        {
            VectorLine.SetEndCap("cap", EndCap.Mirror, capMaterial, capTex);
            lineMaterial = capMaterial;
        }

        linePoints = new Vector2[maxPoints];
        line = new VectorLine("DrawnLine", linePoints, lineMaterial, lineWidth, LineType.Continuous, Joins.Weld);
        if (useEndCap)
        {
            line.endCap = "cap";
        }

        sqrMinPixelMove = minPixelMove * minPixelMove;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            line.ZeroPoints();
            line.minDrawIndex = 0;
            line.Draw();
            previousPosition = linePoints[0] = mousePos;
            lineIndex = 0;
            canDraw = true;
        }
        else if (Input.GetMouseButton(0) && (mousePos - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw)
        {
            previousPosition = linePoints[++lineIndex] = mousePos;
            line.minDrawIndex = lineIndex - 1;
            line.maxDrawIndex = lineIndex;
            if (useEndCap) line.drawEnd = lineIndex;
            if (lineIndex >= maxPoints - 1) canDraw = false;
            line.Draw();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            // Paint line enclosed area
            Debug.Log("Mouse button released");
            PaintPolygon();
            //line.ZeroPoints();
        }
	}


    // Paint line enclosed area using the current brush color.
    private void PaintPolygon()
    {
        Debug.Log("Let's do paint");

        // First create copy of line points
        Vector2[] polygonPoints = new Vector2[line.maxDrawIndex + 1];
        Array.Copy(linePoints, polygonPoints, line.maxDrawIndex + 1);

        // Convert screen coordinates to object local coordinates
        for (int i = 0; i < polygonPoints.Length; i++)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(linePoints[i]);
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
                MeshFilter filter = hit.collider.GetComponent<MeshFilter>();
                Vector3 relativePoint = filter.transform.InverseTransformPoint(hit.point);
                polygonPoints[i].x = relativePoint.x;
                polygonPoints[i].y = relativePoint.z;
            }
        }

        //// Create test line point set
        //Vector2[] polygonPoints = new Vector2[3];
        //polygonPoints[0] = new Vector2(-2, -2);
        //polygonPoints[1] = new Vector2(0, 2);
        //polygonPoints[2] = new Vector2(2, -2);

        //// Draw these line points to see what's going on.
        //Mesh mesh2 = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
        //Vector3[] vertices2 = mesh2.vertices;
        //Color[] colors2 = mesh2.colors;

        //// Let's first fix all the vertices x and z values
        //int index2 = 0;
        //for (int i = 0; i < 100; i++)
        //{
        //    for (int j = 0; j < 100; j++)
        //    {
        //        vertices2[index2].x = j/10.0f-5.0f;
        //        vertices2[index2].z = i/10.0f-5.0f;
        //        index2++;
        //    }
        //}
        //mesh2.vertices = vertices2;
        //mesh2.colors = colors2;
        //mesh2.RecalculateNormals();
        //mesh2.RecalculateBounds();

        //// Now draw polygon
        //for (int i = 0; i < polygonPoints.Length; i++)
        //{
        //    int index = (int)Math.Round((polygonPoints[i].x + 5f) * 10) + (int)Math.Round((polygonPoints[i].y + 5f) * 10) * 100;
        //    if (index > 9999)
        //    {
        //        index = 9999;
        //    }
        //    vertices2[index].y = 2;
        //    colors2[index] = Color.red;
        //}
        //mesh2.vertices = vertices2;
        //mesh2.colors = colors2;
        //mesh2.RecalculateNormals();
        //mesh2.RecalculateBounds();

        // Shift line point sets
        //Vector2 offset = new Vector2(10, 10);
        //for (int i = 0; i < polygonPoints.Length; i++)
        //{
        //    polygonPoints[i] += offset;
        //}
        Debug.Log("Line has " + polygonPoints.Length + " points");
        
        // Paint enclosed area with default color
        Mesh mesh = GameObject.Find("Plane").GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Color[] colors = mesh.colors;        
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 curVertex = new Vector2(vertices[i].x, vertices[i].z);

            if (Assets.Scripts.Common.MISCLib.PointInPolygon(curVertex, polygonPoints))
            {
                Debug.Log("Point " + curVertex.ToString() + "inside polygon.");
                Color c = Color.blue;
                switch (Assets.Scripts.ProjectConstants.diffLevel)
                {
                    case 1:
                        c = Color.blue;
                        break;
                    case 2:
                        c = Color.green;
                        break;
                    case 3:
                        c = Color.red;
                        break;
                    default:
                        break;
                }
                colors[i] = c;
                vertices[i].y = Assets.Scripts.ProjectConstants.diffLevel - 1;
            }
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Next get rid of the line on screen
        line.ZeroPoints();
        line.minDrawIndex = 0;
        line.Draw();
    }
}
