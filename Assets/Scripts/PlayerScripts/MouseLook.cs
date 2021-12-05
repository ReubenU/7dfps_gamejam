
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public Transform playerCamera;

    Transform trans;

    public float lookSens = 5;

    public bool isInverted = false;


    Vector2 input_axes;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    float rotX = 0f;
    void Look()
    {
        // Looking up and down...
        int invert = isInverted ? 1 : -1;

        rotX += input_axes.y * invert * lookSens * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * rotX;

        // Looking left and right...
        float rotY = input_axes.x * -invert * lookSens * Time.deltaTime;

        trans.Rotate(Vector3.up * rotY, Space.Self);
    }

    // Get mouse input axes
    public void MouseAxes(InputAction.CallbackContext context)
    {
        input_axes = context.ReadValue<Vector2>();
    }
}
