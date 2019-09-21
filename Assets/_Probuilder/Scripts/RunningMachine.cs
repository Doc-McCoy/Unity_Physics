using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMachine : MonoBehaviour
{
    public Vector3 m_Direction = Vector3.forward;
    public float m_Speed = 50.0f;
    private List<Character> m_Character;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            m_Character.Add(other.gameObject.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

    private void Update()
    {
        foreach (Character character in m_Character)
        {
            character.Velocity += m_Direction * m_Speed;
        }
    }
}
