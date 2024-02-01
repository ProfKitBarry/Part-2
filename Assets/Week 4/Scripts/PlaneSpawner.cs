using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject plane;
    float timer;

    // Start is called before the first frame update
    void Start()
    {

        Instantiate(plane);
        timer = Random.Range(1, 5);

      
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Instantiate(plane);
            timer = Random.Range(1, 5 * Time.deltaTime);
        }
        else
        {
            timer -=Time.deltaTime;
        }
    }
}
