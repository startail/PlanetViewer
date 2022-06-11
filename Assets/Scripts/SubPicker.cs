using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPicker : MonoBehaviour
{
  public GameObject planet;
  [TooltipAttribute("Degree")] public float latitudeSpeed;
  [TooltipAttribute("Degree")] public float longitudeSpeed;
  [TooltipAttribute("Radian")] public float latitude;
  [TooltipAttribute("Radian")] public float longitude;
  private bool initiate = false;

  // Start is called before the first frame update
  void Start()
  {
    latitudeSpeed = Random.Range(-1.0f, 1.0f)*30;
    longitudeSpeed = Random.Range(-1.0f, 1.0f)*30;
    if( planet == null )
    {
      initiate = false;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if( !initiate )
    {
      Vector2 initialAngles = GetAngleOnSphere(planet, transform.position);
      latitude = initialAngles.x;
      longitude = initialAngles.y;
      initiate = true;
      return;
    }
    latitude += Time.deltaTime * latitudeSpeed * Mathf.Deg2Rad;
    longitude += Time.deltaTime * longitudeSpeed * Mathf.Deg2Rad;
    transform.position = GetPositionOnSphere(planet, longitude, latitude);
  }

  public void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.name.Contains("Debris"))
    {
      Destroy(other.gameObject);
    }
  }

  public Vector3 GetPositionOnSphere(GameObject sphere, float longitude, float latitude)
  {
    float r = sphere.transform.localScale.x/2.0f + 0.5f;
    float x = r * Mathf.Sin(longitude) * Mathf.Cos(latitude);
    float y = r * Mathf.Sin(longitude) * Mathf.Sin(latitude);
    float z = r * Mathf.Cos(longitude);
    Vector3 newPosition = new Vector3(sphere.transform.position.x+x, sphere.transform.position.y+y, sphere.transform.position.z+z);
    return newPosition;
  }

  public Vector2 GetAngleOnSphere(GameObject sphere, Vector3 position)
  {
    float r = sphere.transform.localScale.x/2.0f + 0.5f;
    Vector3 zeroBasePosition = new Vector3( position.x-sphere.transform.position.x, position.y-sphere.transform.position.y, position.z-sphere.transform.position.z);
    
    float clampLongiRad = Mathf.Clamp(zeroBasePosition.z / r, -1.0f, 1.0f);
    float clampLatiRad = Mathf.Clamp(zeroBasePosition.x / (r * Mathf.Sin(clampLongiRad)+0.0000001f), -1.0f, 1.0f);

    float longi = Mathf.Acos( clampLongiRad );
    float lati = Mathf.Acos( clampLatiRad );
  
    return new Vector2(lati, longi);
  }
}
