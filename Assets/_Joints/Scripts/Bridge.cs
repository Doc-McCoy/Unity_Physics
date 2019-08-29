using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private HingeJoint m_Joint;
    public float[] m_Targets;
    private int m_Index = 0;

    private void Awake()
    {
        m_Joint = GetComponent<HingeJoint>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            JointSpring spring = m_Joint.spring;
            
            m_Index = ++m_Index % m_Targets.Length;
            spring.targetPosition = m_Targets[m_Index];

            m_Joint.spring = spring;
        }
    }
}
