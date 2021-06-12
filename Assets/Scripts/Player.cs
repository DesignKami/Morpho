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

	/*Limite -Y*/
	public float donwLimit = -10f;

	void Start () {
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

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
	}


    private void OnTriggerEnter(Collider collision)
    {
		//LAMA
		if (collision.CompareTag("Lama"))
		{
			speed = 1f;
		}
	}

    private void OnTriggerExit(Collider collision)
    {
		//LAMA
		if (collision.CompareTag("Lama"))
		{
			speed = 5f;
		}
	}
}
