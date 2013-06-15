using UnityEngine;
using System.Collections;

public class TerrainLayer : MonoBehaviour {

    public string url = @"file:///C:/Users/Lannyl/Pictures/pics/Funny%20Pics/yulu.jpg";

    // Use this for initialization
    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;

        //Projector go = GameObject.Find("Grid Projector").GetComponent<Projector>();
        ////        go.material.mainTexture = www.texture;
        //go.material.mainTextureScale = new Vector2(1, 1);
        //Color c = go.material.color;
        //c.a = 0.5f;
        //go.material.color = c;
        //Debug.Log("color: " + go.material.color[0] + "," + go.material.color[1] + "," + go.material.color[2] + "," + go.material.color[3]);


        //GameObject go2 = GameObject.Find("Plane");
        //go2.renderer.material.mainTextureScale = new Vector2(99, 99);
        //Color c2 = go2.renderer.material.color;
        //c2.a = 0.5f;
        //go2.renderer.material.color = c2;
        //Debug.Log("color2: " + go2.renderer.material.color[0] + "," + go2.renderer.material.color[1] + "," + go2.renderer.material.color[2] + "," + go2.renderer.material.color[3]);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
