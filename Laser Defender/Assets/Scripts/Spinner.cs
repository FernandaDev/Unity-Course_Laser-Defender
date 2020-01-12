using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speedRotation = 180f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, speedRotation * Time.deltaTime));
    }
}
