using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float lookSensitivity = 250f;
    [SerializeField] private Transform playerBody;

    [SerializeField] private float rayDistance = 2f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float xLook = Input.GetAxisRaw("Mouse X") * lookSensitivity * Time.deltaTime;
        float yLook = Input.GetAxisRaw("Mouse Y") * lookSensitivity * Time.deltaTime;

        xRotation -= yLook;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * xLook);
    }
}
