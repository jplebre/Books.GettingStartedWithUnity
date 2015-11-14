using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour 
{
	public int numberBullets = 0;
	public float timeToReload = 0.2f;
	public float timeForNextShot = 0.0f;

	public AudioClip shotFiredSound;
	public GameObject sparks;


	void Start ()
	{
		Screen.showCursor = false;
	}


	void Update () 
	{
		if(Input.GetButton ("Fire1") && Time.time >= timeForNextShot)
		{
			if (numberBullets >= 1)
			{
				//Debug.Log ("BANG!");
				RaycastHit rayHit;
				Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width/2, Screen.height/2));
				if (Physics.Raycast (ray, out rayHit, 100))
				{
					audio.PlayOneShot(shotFiredSound, 0.5f);

					if (rayHit.collider.gameObject.tag == "Zombies")
						rayHit.collider.gameObject.GetComponent<Animator>().SetBool("hit",true);

					GameObject sparkClone = Instantiate(sparks, rayHit.point, Quaternion.identity) as GameObject;
				}
				--numberBullets;
			}
			timeForNextShot = Time.time + timeToReload;
		}
	}
}
