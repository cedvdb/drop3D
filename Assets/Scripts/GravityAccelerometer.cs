using UnityEngine;

[DisallowMultipleComponent]
public class GravityAccelerometer : MonoBehaviour
{
  public float g = 9.8f * 20f; // earth g * units convertion (m to 5cm)
  private Rigidbody rb;
  private bool isPhone;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    isPhone = Application.platform == RuntimePlatform.Android ||
      Application.platform == RuntimePlatform.IPhonePlayer;
  }

  void Update()
  {
    if (isPhone)
    {
      float x = Input.acceleration.x;
      float y = Input.acceleration.y;
      Physics.gravity = new Vector3(x, y, 0) * g;
    }
    else
    {
      float x = Input.GetAxis("Horizontal");
      float y = Input.GetAxis("Vertical");
      Vector2 v = new Vector2(x, y);
      float angleDeg = Vector2.Angle(new Vector2(1, 0), v);
      float angle = angleDeg * Mathf.Deg2Rad;
      x = Mathf.Abs(Mathf.Cos(angle)) * x;
      y = Mathf.Abs(Mathf.Sin(angle)) * y;
      Physics.gravity = new Vector3(x, y, 0) * g;
    }
  }

}
