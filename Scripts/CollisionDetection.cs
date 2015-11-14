/* script based off Getting Started with Unity book "CollisionDetection"
 * 
 * Added:
 * - Start statement to always check if audio source has play on awake disabled.
 * 
 */


using UnityEngine;
using System.Collections;
[RequireComponent (typeof (AudioSource))]

public class CollisionDetection : MonoBehaviour 
{
	public AudioClip audioFile;
	public float audioLevel;

	private bool hasKey ;
	private bool hasGun ;
	private int health ;

	private GameObject guiAmmo;

	public DisplayMessageToUser pickupGUI;
	public HealthBar healthBarUpdate;
	public GameObject crosshairObject;
	public GunBehaviour gunAmmo;
	
	void OnControllerColliderHit(ControllerColliderHit whatWeHit)
	{
		if (whatWeHit.gameObject.tag == "MedPack" || whatWeHit.gameObject.tag == "Gun" || whatWeHit.gameObject.tag == "Key")
		{
			Destroy (whatWeHit.gameObject);
			audio.PlayOneShot(audioFile, audioLevel);

			pickupGUI.displayText(whatWeHit.gameObject.tag + " Collected!");

			if (whatWeHit.gameObject.tag == "MedPack") 
			{
				health = 100;
				healthBarUpdate.SetHealth(health);
			}
			if (whatWeHit.gameObject.tag == "Gun") 
			{
				hasGun = true;
				ChangeGUITexture(true, "Gun");
				crosshairObject.guiTexture.enabled = true;
				gunAmmo.numberBullets += 40;
			}
			if (whatWeHit.gameObject.tag == "Key") 
			{
				hasKey = true;
				ChangeGUITexture(true, "Key");
			}
		}


		//when you touch the exit door, can you leave?
		if (whatWeHit.gameObject.tag == "Exit_Door")
		{
			if (hasKey)
				whatWeHit.gameObject.animation.Play("open_door");
			else
				pickupGUI.displayText("Sorry, you need the key to open this door");
		}
	}


	//to display inventory
	//called by collision above
	public void ChangeGUITexture(bool toBeDisplayed, string label)
	{
		GameObject.Find ("GUITexture_" + label).guiTexture.enabled = toBeDisplayed;
	}

			                                                 
	void Start () 
	{
		hasGun = false;
		hasKey = false;
		health = 1;
	
		audio.playOnAwake = false;

		pickupGUI = GameObject.Find ("GUIText_displaymessagetouser").GetComponent<DisplayMessageToUser>();
		healthBarUpdate = GameObject.Find ("healthbar").GetComponent<HealthBar>();
		guiAmmo = GameObject.Find ("GUIText_ammo");
		crosshairObject = GameObject.Find ("GUITexture_crossHair");
		gunAmmo = gameObject.GetComponent<GunBehaviour>();

		ChangeGUITexture(false, "Gun");
		ChangeGUITexture(false, "Key");
	}

	void Update () 
	{
		if (hasGun)
			guiAmmo.guiText.text = "Ammo: " + GetComponent<GunBehaviour>().numberBullets;
	}
}
