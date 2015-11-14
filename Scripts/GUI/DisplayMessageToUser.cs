using UnityEngine;
using System.Collections;

public class DisplayMessageToUser : MonoBehaviour 
{
	private float timer;
	//time for which the message will be displayed
	private float displayTimer;
	private bool timerIsActive;
	private string displayMessage;

	void StartTimer ()
	{
		timer = 0.0f;
		guiText.text = displayMessage;
		timerIsActive = true;
		displayTimer = 2.0f;
	}


	// Use this for initialization
	void Start () 
	{
		guiText.text = " ";
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (timerIsActive) 
		{
			timer += Time.deltaTime;
			if (timer > displayTimer) 
			{
				timerIsActive = false;
				guiText.text = " ";
			}
		}
	}

	public void displayText (string message)
	{
		displayMessage = message;
		StartTimer();
	}
}
