using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{

	private int currentHealth = 45;
	private Texture2D currentColor;
	public GUIStyle style;
	public Texture2D redTexture;
	public Texture2D greenTexture;
	public Texture2D orangeTexture;
	public Texture2D blackTexture;


	void OnGUI()
	{
		if (currentHealth >= 67)
			currentColor = greenTexture;
		else if (currentHealth >= 34)
			currentColor = orangeTexture;
		else
		{
			currentColor = redTexture;
		}

		style.normal.background = blackTexture;
		GUI.Box (new Rect (0,0,100,20), " ", style);
		style.normal.background = currentColor;
		GUI.Box (new Rect(0,0, currentHealth ,20), " ", style);
	}

	public void SetHealth(int newHealthValue)
	{
		currentHealth = newHealthValue;
	}

	public void DecreaseHealth(int damage)
	{
		currentHealth -= damage;
	}

	void Start () 
	{
	}
	
	void Update () 
	{
	}
}
