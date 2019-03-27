using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRaycast : MonoBehaviour
{

    private GameObject Player;
    private GameObject[] Enemys;
    public float ReloadTime = 5f;
    private bool GunIsFull = true;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GunIsFull)
            ReloadTime -= Time.deltaTime;

        if (ReloadTime <= 0)
        {
            GunIsFull = true;
            ReloadTime = 5f;
        }

        FindEnemys();
    }

    public void FindEnemys()
    {
        if (GunIsFull)
            foreach (GameObject e in Enemys)
                if (DistanceTarget(Player.transform.position, e.transform.position) <= Player.GetComponent<Player>().Vision)
                {
                    Player.GetComponent<Player>().Shot(e);
                    GunIsFull = false;
                }
    }

    public void Blowup(Vector3 target, float explosionRadius)
    {
        foreach (GameObject e in Enemys)
            if (DistanceTarget(target, e.transform.position) <= explosionRadius)
                Debug.Log(string.Format("Enemy {0} is dead by explosion !\n {1}", e.name, DistanceTarget(target, e.transform.position)));

    }

    private float DistanceTarget(Vector3 firstVector, Vector3 secondVector)
    {
        return Mathf.Sqrt(Mathf.Pow(secondVector.x - firstVector.x, 2) + Mathf.Pow(secondVector.y - firstVector.y, 2));
    }
}
