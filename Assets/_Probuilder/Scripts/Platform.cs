using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform[] m_Points;
    public float m_Speed;
    public float m_Accuracy;
    public float m_Delay;

    private int m_Index;
    private float m_Time;
    private bool m_Waiting;

    private void Update()
    {
        if(m_Waiting)
        {
            if(Time.time - m_Time >= m_Delay)
            {
                m_Waiting = false;
                m_Index = ++m_Index % m_Points.Length;
            }
            return;
        }

        if (Vector3.Distance(m_Points[m_Index].position, transform.position) <= m_Accuracy)
        {
            m_Waiting = true;
            m_Time = Time.time;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, m_Points[m_Index].position, m_Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
