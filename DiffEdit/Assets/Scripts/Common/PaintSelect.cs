using UnityEngine;
using System.Collections;

public class PaintSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// When clicked
    void OnClick()
    {
        // First change label back and forth
        UILabel label = GameObject.Find("lblPaintSelect").GetComponent<UILabel>(); 
		
        if (label.text == "Paint")
        {
            label.text = "Select";
			Assets.Scripts.ProjectConstants.editMode = 2;
            // Camera.main.transform.GetComponent<DrawLinesMouse>().enabled = true;
            Camera.main.transform.GetComponent<PaintSurface>().enabled = false;
            GameObject.Find("BrushProjector").SetActive(false);
        }
        else
        {
            label.text = "Paint";
			Assets.Scripts.ProjectConstants.editMode = 1;
            // Camera.main.transform.GetComponent<DrawLinesMouse>().enabled = false;
            Camera.main.transform.GetComponent<PaintSurface>().enabled = true;
            GameObject.Find("BrushProjector").SetActive(true);

        }        
    }	
}
