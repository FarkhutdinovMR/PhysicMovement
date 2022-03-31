using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicBroadsaster : MonoBehaviour
{
    public event Action<GameObject> Collised;

    private void OnCollisionEnter(Collision collision)
    {
        Collised?.Invoke(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collised?.Invoke(other.gameObject);
    }
}