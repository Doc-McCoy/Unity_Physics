using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float m_Speed;
    public Vector3 m_Axis;
    public Transform m_Point;

    void Update()
    {
        transform.RotateAround(m_Point.position, m_Axis, m_Speed * Time.deltaTime);
    }
}
