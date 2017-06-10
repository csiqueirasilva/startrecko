using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTiro : MonoBehaviour {
	private Rigidbody2D rgdbTiro;
	private Collider2D capsuleCollider;

	// Use this for initialization
	void Start () {
		rgdbTiro = GetComponent<Rigidbody2D>();
		capsuleCollider = GetComponent<Collider2D>();
		MainGameGlobals globals = transform.parent.GetComponent<MainGameGlobals>();
		Transform nave = globals.nave;
		Collider2D c2 = (nave.GetComponent<Collider2D>());
		Physics2D.IgnoreCollision(capsuleCollider, c2);
	}
	
	// Update is called once per frame
	void Update () {
		rgdbTiro.position = rgdbTiro.position + new Vector2 (0.25f, 0);	
	}



}
