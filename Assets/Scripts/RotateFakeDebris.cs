using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFakeDebris : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Camera.main.transform.forward, Mathf.PI * Time.deltaTime, Space.World);
    }
}
