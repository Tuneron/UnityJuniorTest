using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public readonly float Vision = 4f;  // Радиус обзора персонажа
    public float Speed = 0.07f;         // Скорость перемещения персонажа

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("d") && transform.position.x <= 6f)
            transform.position = new Vector2(transform.position.x + Speed, transform.position.y);
        if (Input.GetKey("a") && transform.position.x >= 1.3f)
            transform.position = new Vector2(transform.position.x - Speed, transform.position.y);

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void Shot(GameObject enemy)
    {
        Debug.Log(string.Format("Was shot for the {0}\n{1}", enemy.name, System.DateTime.Now.ToString("h:mm:ss tt")));
        GameObject.FindGameObjectWithTag("Projectile").GetComponent<Projectile>().Launch(enemy.transform.position);
    }
}
