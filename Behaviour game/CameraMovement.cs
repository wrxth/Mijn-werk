using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float BuffModifier = 2f;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float BaseZoomSpeed = 10f;
    [SerializeField] private float BaseRotationSpeed = 10f; 
    private float Speed = 5f;
    private float ZoomSpeed = 10f;
    private float RotationSpeed = 10f;

    [SerializeField] private float MinHeight = 4;
    [SerializeField] private float MaxHeight = 40;

    private Vector2 p1;
    private Vector2 p2;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = baseSpeed * BuffModifier;
            ZoomSpeed = BaseZoomSpeed * BuffModifier;
            RotationSpeed = BaseRotationSpeed * BuffModifier;
        }
        else
        {
            Speed = baseSpeed;
            ZoomSpeed = BaseZoomSpeed;
            RotationSpeed = BaseRotationSpeed;
        }
        float horizontal = transform.position.y * Speed * Input.GetAxisRaw("Horizontal");
        float vertical = transform.position.y * Speed * Input.GetAxisRaw("Vertical");
        float scrollSpeed = Mathf.Log(transform.position.y) * ZoomSpeed * Input.GetAxisRaw("Mouse ScrollWheel");

        if (transform.position.y >= MaxHeight && scrollSpeed > 0)
        {
            scrollSpeed = 0;
        }
        else if (transform.position.y <= MinHeight && scrollSpeed < 0)
        {
            scrollSpeed = 0;
        }

        if ((transform.position.y + scrollSpeed) > MaxHeight)
        {
            scrollSpeed = MaxHeight - transform.position.y;
        }
        else if((transform.position.y + scrollSpeed) < MinHeight)
        {
            scrollSpeed = MinHeight - transform.position.y;
        }

        Vector3 verticalMove = new Vector3(0, scrollSpeed, 0);
        Vector3 lateralMove = horizontal * transform.right;
        Vector3 forwardMove = transform.forward;

        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vertical;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;

        GetCameraRotation();
    }

    private void GetCameraRotation()
    {
        if (Input.GetMouseButtonDown(2))
        {
            p1 = Input.mousePosition;
        } 
        if (Input.GetMouseButton(2))
        {
            p2 = Input.mousePosition;

            float dx = (p2 - p1).x * RotationSpeed;
            float dy = (p2 - p1).y * RotationSpeed;

            //dy = Mathf.Clamp(dy,-90f, 90f);
           
            transform.rotation *= Quaternion.Euler(new Vector3(0, dx, 0));
            transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(dy, 0, 0));

            p1 = p2;
        }
    }
}
