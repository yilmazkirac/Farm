using UnityEngine;

public class JoystickController : MonoBehaviour
{
  [SerializeField] private RectTransform joystickOutline;
  [SerializeField] private RectTransform joystickButton;
  [SerializeField] private float moveFactor;
  private bool _isControlJoystick;
  private Vector3 _tabPosition;
  private Vector3 _move;

  private void Start()
  {
    HideJoystick();
  }

  public void TappedOnJoystickZone()
  {
    _tabPosition = Input.mousePosition;
    joystickOutline.position = _tabPosition;
    ShowJoystick();
  }
  private void ShowJoystick()
  {
    joystickOutline.gameObject.SetActive(true);
    _isControlJoystick = true;
  }

  private void HideJoystick()
  {
    joystickOutline.gameObject.SetActive(false);
    _isControlJoystick = false;
    _move = Vector3.zero;
  }

  public void ControlJoystick()
  {
    Vector3 currentPosition = Input.mousePosition;
    Vector3 direction = currentPosition-_tabPosition;

    float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
    float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;

    float joyistickOutlineHalfWidth = joystickOutline.rect.width / 2;
    float newWidth = joyistickOutlineHalfWidth * canvasYScale;

    moveMagnitude = Mathf.Min(moveMagnitude, newWidth);
    
    _move = direction.normalized * moveMagnitude;
    
    Vector3 targetPos = _tabPosition + _move;
    joystickButton.position = targetPos;
    
    if (Input.GetMouseButtonUp(0))
    {
      HideJoystick();
    }
  }

  public Vector3 GetMovePosition()
  {
    return _move;
  }
  private void Update()
  {
    if (_isControlJoystick)
    {
      ControlJoystick();
    }
  }
}
