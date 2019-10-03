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

    private Character m_Character;

    private void Update()
    {
        if(m_Waiting)
        {
            if (m_Character)
            {
                m_Character.m_ExternalMovement = Vector3.zero;
            }

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
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, m_Points[m_Index].position, m_Speed * Time.deltaTime);
            Vector3 movement = nextPosition - transform.position;
            transform.position = nextPosition;

             if (m_Character)
            {
                m_Character.m_ExternalMovement = movement;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_Character = other.GetComponent<Character>();
            // other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Character.m_ExternalMovement = Vector3.zero;
            m_Character = null;
            // other.transform.parent = null;
        }
    }
}
