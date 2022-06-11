using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class SampleRotate : MonoBehaviour
{
  public enum Mode
  {
    Auto,
    Touch,
    Drag
  }
  public Mode mode = Mode.Auto;
  private InputAction clickAction, pointAction;
  private Camera mainCamera;
  Vector2 lastPosition;
  Vector2 pointingPosition;
  Vector2 centralPosition;
  Vector2 rotationSpeed = new Vector2(50f, 50f);
  bool pressed = false;
  bool moving = false;
  Vector3 lastTouchNormal;
  Vector3 centralNormal;
  float interpolantValue;
  float goalAngle;
  float deltaAngle;
  bool isPlus = true;

  public GameObject mainPicker;

  void Start()
  {
    PlayerInput input = GetComponent<PlayerInput>();
    InputActionMap actionMap = input.currentActionMap;
    clickAction = actionMap["Click"];
    pointAction = actionMap["Point"];

    mainCamera = Camera.main;
    centralPosition = new Vector2(
      Camera.main.gameObject.GetComponent<PixelPerfectCamera>().refResolutionX / 2, Camera.main.gameObject.GetComponent<PixelPerfectCamera>().refResolutionY / 2);
  }

  void Update()
  {
    switch (mode)
    {
      case Mode.Auto:
        transform.Rotate(Camera.main.transform.up, 2.0f * Mathf.PI * Time.deltaTime, Space.World);
        break;
      case Mode.Drag:
        if (clickAction.ReadValue<float>() != 0)
        {
          if (!pressed)
          {
            pressed = true;
            lastPosition = pointAction.ReadValue<Vector2>();
            return;
          }
          pointingPosition = pointAction.ReadValue<Vector2>();
          float rotX = (pointingPosition.x - lastPosition.x) * rotationSpeed.x * Mathf.Deg2Rad;
          float rotY = (pointingPosition.y - lastPosition.y) * rotationSpeed.y * Mathf.Deg2Rad;
          transform.Rotate(Camera.main.transform.up, -rotX, Space.World);
          transform.Rotate(Camera.main.transform.right, rotY, Space.World);
          lastPosition = pointingPosition;
        }
        else
        {
          pressed = false;
        }
        break;
      case Mode.Touch:
        if (moving)
        {
          deltaAngle += 2 * Mathf.PI * Time.deltaTime;
          if (isPlus)
          {
            if (deltaAngle >= goalAngle) moving = false;
          }
          else
          {
            if (deltaAngle <= goalAngle) moving = false;
          }
          transform.Rotate(Vector3.Cross(lastTouchNormal, centralNormal), 2 * Mathf.PI * Time.deltaTime, Space.World);
        }
        if (clickAction.ReadValue<float>() != 0)
        {
          pointingPosition = pointAction.ReadValue<Vector2>();
          Ray ray = mainCamera.ScreenPointToRay(pointingPosition);
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit))
          {
            if (hit.collider.gameObject.name.Contains("Earth"))
            {
              lastTouchNormal = hit.normal;
              centralNormal = -Camera.main.transform.forward;
              
              // for Planet Rotation
              goalAngle = Vector3.SignedAngle(lastTouchNormal, centralNormal, Vector3.Cross(lastTouchNormal, centralNormal));
              isPlus = (goalAngle >= 0) ? true : false;
              deltaAngle = 0;
              moving = true;

              // for Picker Rotation
              float r = Mathf.Sqrt(hit.point.x * hit.point.x + hit.point.y * hit.point.y);
              float rad = Mathf.Asin( hit.point.y / r );
              float deg = rad * Mathf.Rad2Deg;
              if( hit.point.x >= 0 )
              {
                deg += (-90);
              }
              else
              {
                deg = 90 - deg;
              }
              mainPicker.transform.rotation = Quaternion.Euler(0.0f, 0.0f, deg);
            }
          }
        }
        break;
    }
  }
}
