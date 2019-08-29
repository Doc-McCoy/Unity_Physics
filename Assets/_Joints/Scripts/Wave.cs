using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float m_Amplitude;
    public float m_SmoothTime;
    private Vector3 m_Offset;

    private void Start()
    {
        m_Offset = transform.position;
    }

    private void Update()
    {
        float y = m_Amplitude * Mathf.Sin(Time.time * m_SmoothTime);
        // O Resultado de um Seno sempre vai de -1 a 1.
        transform.localPosition = m_Offset + Vector3.up * y;
    }
}
