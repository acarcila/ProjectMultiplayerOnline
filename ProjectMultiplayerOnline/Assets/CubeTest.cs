using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("eureka");
    }
}
