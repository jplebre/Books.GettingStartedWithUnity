using UnityEngine;
using System.Collections;

public class ZombieControl : MonoBehaviour 
{
	public bool walking = false;
	public Animator anim;
	public AnimatorStateInfo currentState;
	public int walkForwardState = Animator.StringToHash("Base Layer.WalkForward");
	public int idleState = Animator.StringToHash("Base Layer.Idle");
	public int hitState = Animator.StringToHash ("Base Layer.Hit");
	public int deadState = Animator.StringToHash ("Base Layer.Dead");
	public int attackState = Animator.StringToHash ("Base Layer.Attack");
	public int patrolState = Animator.StringToHash ("Base Layer.Patrol");

	private int wayPointIndex = 1;

	public GameObject[] zombies;

	private Transform playerTransform;
	private RaycastHit hit;

	public int damage;
	public bool hasAttacked = false;


	void Start () 
	{
		anim = gameObject.GetComponent<Animator>();
		playerTransform = GameObject.FindWithTag ("Player").transform;
		zombies = GameObject.FindGameObjectsWithTag ("Zombies");
		damage = 0;
	}
	
	void Update () 
	{
		currentState = anim.GetCurrentAnimatorStateInfo(0);

		transform.position = new Vector3(transform.position.x, 0, transform.position.z);

		/*avoids zombie to spin around the player
		 * only works in Java, C# needs a variable?
		 * transform.position.y = -0.5;
		 * transform.rotation.x = 0.0;
		 * transform.rotation.z = 0.0;
		 */

		//Debug.Log (currentState);
		//Debug.Log (currentState.nameHash);
		//Debug.Log (walkForwardState);
		//Debug.Log (idleState);

		//calculating the distance zombie-player
		float distance = Vector3.Distance(gameObject.transform.position, playerTransform.position);

		//calculating if enemy is close enough for an attack
		if (distance < 1.5)
			anim.SetBool ("withinReach", true);
		else
			anim.SetBool ("withinReach", false);

		switch (currentState.nameHash)
		{
		case 961959321:
			hasAttacked = false;
			foreach (GameObject zombie in zombies)
			{
				if (Vector3.Distance (transform.position, zombie.transform.position) < 8.0f)
					zombie.GetComponent<ZombieControl>().SetWalking(true);				
			}
			transform.LookAt(playerTransform);
			break;

		case 1432961145:
			if ((Physics.Raycast (new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), transform.forward, out hit, 20) && hit.collider.gameObject.tag == "Player") || distance < 2.0f)
			{
				anim.SetBool("walking", true);
			}			
			break;
		
		case -1005802792:
			if (anim.GetBool ("hit")) {damage++;}
			if (damage >= 5)
			    anim.SetBool ("dead", true);
			anim.SetBool("hit", false);
			break;

		case 1684585544:
			Destroy(gameObject, 6.0f);
			break;

		case 1130333774:
			if (!hasAttacked)
			{
				ApplyDamage();
				hasAttacked = true;
			}
			anim.SetBool ("withinReach", false);
			break;

		case 258246660:
			transform.LookAt (GameObject.Find ("wayPoint"+wayPointIndex).transform);
			float distanceToWayPoint;
			distanceToWayPoint = Vector3.Distance (transform.position, GameObject.Find("wayPoint"+wayPointIndex).transform.position);
			if (distanceToWayPoint < 1.0f)
				wayPointIndex++ ;
			if(wayPointIndex>4)
				wayPointIndex = 1;
			break;

		default:
			break; 
		}

	}

	public void SetWalking(bool startWalking)
	{
		anim.SetBool ("walking", startWalking);
	}

	public void ApplyDamage()
	{
		GameObject.Find ("healthbar").GetComponent<HealthBar>().DecreaseHealth(5);
	}
}
