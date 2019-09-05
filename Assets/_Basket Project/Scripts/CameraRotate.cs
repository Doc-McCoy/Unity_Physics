using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float m_Speed;
    public Camera m_Camera;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_Speed;
        float mouseY = Input.GetAxis("Mouse Y") * m_Speed;

        transform.localRotation *= Quaternion.Euler(0, mouseX, 0);
        m_Camera.transform.localRotation *= Quaternion.Euler(-mouseY, 0, 0);

    }

}
