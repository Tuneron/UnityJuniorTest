using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float Radius = 3f;
    CustomRaycast raycast;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(Radius, Radius, 0);
        raycast = GameObject.FindGameObjectWithTag("Player").GetComponent<CustomRaycast>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Blowup()
    {
        raycast.Blowup(transform.position, Radius);
    }
}
