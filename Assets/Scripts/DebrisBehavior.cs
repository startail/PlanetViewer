using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisBehavior : MonoBehaviour
{
    Vector3 randomFloating;

    // Start is called before the first frame update
    void Start()
    {
        randomFloating = new Vector3(Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomFloating);
    }
}
