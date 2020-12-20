using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeSize : MonoBehaviour
{
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(0.25f, 0.75f), Random.Range(0.15f, 0.4f), Random.Range(0.25f, 0.75f));
    }
}
