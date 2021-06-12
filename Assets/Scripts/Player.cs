using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;

	public float speed = 600.0f;
	public float turnSpeed = 400.0f;
	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;
	public GameObject startPoint;

	private bool onFloor = true;

	/*Limit -Y*/
	public float donwLimit = -10f;

	void Start () {
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	//wind
	public float randomTime = 40f;
	public float windTime = 5f;
	public float windTimeInitial = 5f;

	void Update ()
	{

		if (Input.GetKey ("w")) {
			anim.SetInteger ("AnimationPar", 1);
		}  else {
			anim.SetInteger ("AnimationPar", 0);
		}

		if(controller.isGrounded){
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			onFloor = true;
		}

        if (Input.GetKeyDown("space") && (onFloor == true))
        {
			onFloor = false;
			moveDirection.x = 7;
			moveDirection.y = 18;

		}
		

		if (transform.position.y <= donwLimit)
		{
			controller.enabled = false;
			transform.position = startPoint.transform.position;
			controller.enabled = true;
		}

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;

		//wind

		//Counts Lost Time
		randomTime -= Time.deltaTime;

		if (randomTime <= 0)
		{

			//Counts the wind time desired
			windTime -= Time.deltaTime;

			speed = 50f;

        }

		else
		{
			speed = speed;
		}


		if ((randomTime <= 0) && (windTime <= 0))
		{

			//If wind time and random time are < 0, this function seeks a new value
			//It randomizes values from 10 to 19 seconds, 20 is not excluded
			Random.Range(3, 20);

		}
	}
	

	private void OnTriggerEnter(Collider collision)
    {
		//mud
		if (collision.CompareTag("Lama"))
		{
			speed = 1f;
		}
		
		//ice
		if (collision.CompareTag("Ice"))
		{
			speed = 15f;
		}
	}

    private void OnTriggerExit(Collider collision)
    {
		//mud
		if (collision.CompareTag("Lama"))
		{
			speed = 5f;
		}

		//ice
		if (collision.CompareTag("Ice"))
		{
			speed = 5f;
		}
	}
}