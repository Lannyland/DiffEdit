using UnityEngine;
using System.Collections;

public class HSLTest : MonoBehaviour {
	HSLColor hsl;
	public bool HSLToRGB;
	public float h;
	public float s;
	public float l;
	
	public Color rgbCol;
	
	public float r;
	public float g;
	public float b;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (HSLToRGB) {
			hsl = new HSLColor(h, s, l);
			rgbCol = (Color) hsl;
			
			renderer.material.color = rgbCol;
			
			r = rgbCol.r;
			g = rgbCol.g;
			b = rgbCol.b;
		} else {
			hsl = rgbCol;
			Color temp = hsl;
			
			renderer.material.color = temp;
			
			h = hsl.h;
			s = hsl.s;
			l = hsl.l;
			r = temp.r;
			g = temp.g;
			b = temp.b;
		}
	}
}
