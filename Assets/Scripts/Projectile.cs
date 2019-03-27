using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private const float g = 9.82f;      // Ускорение свободного падения
    private const int MaxBounce = 3;
    private float ThrowAngle = 0.785f;  // Угол запуска снаряда

    public float TimeFlyScale = 0.15f;  // Скорость полета снаряда
    private bool TimeDelay = false;
    private float DeltaTime = 0;
    private float CurrentTimeFly = 0;

    private bool IsLaunch = false;

    private Vector3 StartVector;
    private Vector3 TargetVector;
    private GameObject Explosion;

    private int CurrentBounce = 0;
    private float Speed = 0; 

    // Use this for initialization
    void Start()
    {
        StartVector = GameObject.FindGameObjectWithTag("Player").transform.position;
        Explosion = GameObject.FindGameObjectWithTag("Explosion");
        CurrentBounce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DeltaTime >= TimeFlyScale)
        {
            DeltaTime = 0;
            TimeDelay = false;
        }

        if (IsLaunch)
        {
            DeltaTime += Time.deltaTime;

            if (!TimeDelay)
            {
                transform.position = new Vector3(transform.position.x + SpeedX(Speed, ThrowAngle, StartVector.x > TargetVector.x ? -1f : 1f),
                    transform.position.y + SpeedY(Speed, ThrowAngle, CurrentTimeFly, 1));
                CurrentTimeFly += Time.deltaTime;
                TimeDelay = true;
            }
        }
        else
        {
            StartVector = GameObject.FindGameObjectWithTag("Player").transform.position; //  Если снаряд не запущен - перемещается за персонажем
            transform.position = StartVector;
        }

        if ((transform.position.x >= TargetVector.x - 0.5f && transform.position.x <= TargetVector.x + 0.5f) &&
            (transform.position.y >= TargetVector.y - 0.5f && transform.position.y <= TargetVector.y + 0.5f))
        {
            CurrentBounce++;
            Debug.Log("Boom");
            Explosion.transform.position = TargetVector;
            Explosion.GetComponent<Explosion>().Blowup();
            StartVector = TargetVector;
            Launch(new Vector3(TargetVector.x + (CurrentBounce + 1f)/CurrentBounce, TargetVector.y, 0));
            CurrentTimeFly = 0;
        }

        if (CurrentBounce == MaxBounce)
        {
            IsLaunch = false;
            CurrentBounce = 0;
        }
    }

    public void Launch(Vector3 target)
    {
        TargetVector = target;
        Speed = Mathf.Sqrt((BounceLenght(StartVector, target) * g) / Mathf.Sin(2 * ThrowAngle)) / 8.2f;
        IsLaunch = true;
    }

    private float BounceLenght(Vector3 firstVector, Vector3 secondVector)
    {
        return Mathf.Sqrt(Mathf.Pow(secondVector.x - firstVector.x, 2) + Mathf.Pow(secondVector.y - firstVector.y, 2));
    }

    private float SpeedX(float startSpeedX, float angle, float side)
    {
        return (startSpeedX * Mathf.Cos(angle)) * side;
    }

    private float SpeedY(float startSpeedY, float angle, float currentTime, float side)
    {
        return ((startSpeedY * Mathf.Sin(angle)) - (g * currentTime)) * side;
    }
}
