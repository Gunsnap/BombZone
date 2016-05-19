﻿using UnityEngine;
using System.Collections;

public class hero : MonoBehaviour {

	public float heroSpeed = 1f;

	public bool Run;
	public bool PutBomb;
	public float BombRate = 0.5f;
	private bool AllowBomb;

	private Vector3 movement;
	private float nextBomb = 0.0f;

	static int SekTidGemt;

	private Rigidbody rb;
	Animator am;
	private Spawner sp; //= new Spawner();

	void Start(){
		rb = GetComponent<Rigidbody> ();
		am = GetComponent<Animator> ();
		sp = GetComponent<Spawner>();
		sp.SpawnSomethingAwesome ();
		SekTidGemt = 5;

	}

	void Update(){

		float Tid = Time.fixedTime;
		int SekTid = (int)Tid;

		// Movement
		float movementHorisontal = -Input.GetAxis ("Horizontal");
		float movementVertical = Input.GetAxis ("Vertical");
		movement = new Vector3 (movementVertical * heroSpeed, 0F, movementHorisontal * heroSpeed);

		if (movement != Vector3.zero) {
			Run = true;
			rb.AddForce (movement, ForceMode.Acceleration);
		} else {
			Run = false;
			rb.velocity = Vector3.zero;
		}

		// Bomb
		if (SekTidGemt < SekTid) {
			AllowBomb = true;
			SekTidGemt = SekTid;
		} else {
			//AllowBomb = false;
		}

		if (AllowBomb) {
			if (Input.GetKeyDown (KeyCode.Space) && Tid > nextBomb) {
				nextBomb = Tid + BombRate;
				PutBomb = true;
				Debug.Log ("Smid en bombe");
				//sp.SpawnSomethingAwesome ();
				AllowBomb = false;
			} else {
				// Der er ikke trykket
				PutBomb = false;
				Debug.Log("Der må smides - Men der er ikke trykket");
			}
		} else {
			//PutBomb = false;
			Debug.Log("Du må IKKE smide en bombe");
		}

	} // Lukker update
	
	// Update is called once per frame
	void FixedUpdate () {
		am.SetBool ("Run", Run);
		am.SetBool ("PutBomb", PutBomb);
	} // Lukker FixedUpdate
} // Lukker class