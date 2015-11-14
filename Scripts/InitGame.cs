using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour 
{

	void Start () 
	{
		GameObject.Find ("GUIText_ammo").guiText.text = " ";
		GameObject.Find ("GUITexture_crossHair").guiTexture.enabled = false;
		GameObject.Find ("GUIText_displaymessagetouser").guiText.text = " ";
	}
	
	void Update () 
	{
	
	}
}
