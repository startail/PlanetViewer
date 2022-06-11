using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDebris : MonoBehaviour
{
    [SerializeField] public GameObject debrisBase;
    [SerializeField] public GameObject debrisPool;
    [SerializeField] private float interval;
    private float timer;
    [SerializeField] private GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        timer = interval;
    }

    // Update is called once per frame
    void Update()
    {
        if( planet == null ) return ;
        if( debrisBase == null ) return;
        if( debrisPool == null ) return;

        timer -= Time.deltaTime;
        if( timer <= 0 )
        {
            timer += interval;
            GameObject debris = Instantiate(debrisBase,CreateRandomSatelliteOrbitPosition(planet),new Quaternion(0,0,0,1));
            debris.transform.parent = debrisPool.transform;
        }
    }

    private Vector3 CreateRandomSatelliteOrbitPosition(GameObject planet)
    {
        float horizontalAngle = Random.Range(-180,180);
        float verticalAngle = Random.Range(-180,180);
        float radius = planet.transform.localScale.x/2.0f+Random.Range(0.5f,1.0f);
		float x = radius * Mathf.Sin(horizontalAngle * Mathf.Deg2Rad) * Mathf.Cos(verticalAngle * Mathf.Deg2Rad);
		float y = radius * Mathf.Sin(horizontalAngle * Mathf.Deg2Rad) * Mathf.Sin(verticalAngle * Mathf.Deg2Rad);
		float z = radius * Mathf.Cos(horizontalAngle * Mathf.Deg2Rad);
        Vector3 newPosition = new Vector3(planet.transform.position.x+x, planet.transform.position.y+y, planet.transform.position.z+z);
		return newPosition;
    }
}
