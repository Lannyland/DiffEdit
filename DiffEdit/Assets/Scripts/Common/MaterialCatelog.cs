using UnityEngine;
using System.Collections;

public class MaterialCatelog : MonoBehaviour {
	
	public Material[] catelog;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Material getMaterial(int index)
	{
		return catelog[index];
	}
}
