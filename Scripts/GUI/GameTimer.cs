using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	private float time;
	private float minutes;
	private float seconds;
	private GameObject guiTimerDisplayText;

	// Use this for initialization
	void Start () 
	{
		guiTimerDisplayText = GameObject.Find ("GUIText_timer");
	}
	
	// Update is called once per frame
	void Update () 
	{
		time = Time.deltaTime + time;
		minutes = time / 60;
		seconds = time % 60;
		int tminutes = (int) minutes;
		int tseconds = (int) seconds;

		guiTimerDisplayText.guiText.text = "Time Elapsed: \n" + tminutes + " min : " + tseconds +" sec";

	}
}
