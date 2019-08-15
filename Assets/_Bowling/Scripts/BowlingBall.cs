using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    public float m_Force = 100.0f;
    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = (position - Input.mousePosition).normalized;
        direction.y = 0.0f;

        m_Rigidbody.AddForce(direction * m_Force);
    }
}
