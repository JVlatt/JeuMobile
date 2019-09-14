using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{

    public int ID;
    public int difficulty;
    public bool isTransition;

    public float Lenght;
    public Vector3 startY;
    public Vector3 endY;


    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
