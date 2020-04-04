using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
}
