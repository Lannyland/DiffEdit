using UnityEngine;
using System.Collections;
using System;

public class CountDownTimer : MonoBehaviour {
	
	public int timeTotal = 300;
	private float timeLeft;
	UILabel timeRemain;
	
	// Use this for initialization
	void Start () {
		timeLeft = timeTotal;
		timeRemain = GameObject.Find("lblTime").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = timeLeft - Time.deltaTime;
		if((int)timeLeft < 0)
		{
			GameOver();
		}
		else
		{			
			timeRemain.text = ((int)timeLeft).ToString();	
		}
	}
	
	// Something to do with time is up
	void GameOver()
	{		
	}
}
