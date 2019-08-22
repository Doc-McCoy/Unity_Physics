using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BreakoutBall : MonoBehaviour
{
    public float m_Force = 1000.0f;
    private Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        Vector3 direction = Vector3.forward;
        direction.x = Random.Range(-1.0f, 1.0f);
        m_Rigidbody.AddForce(direction * m_Force);
    }
}
