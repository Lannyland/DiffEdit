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
        }
        else
        {
            label.text = "Paint";
			Assets.Scripts.ProjectConstants.editMode = 1;
        }        
    }	
}
