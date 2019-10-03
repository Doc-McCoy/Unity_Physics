using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningMachine : MonoBehaviour
{
    public Vector3 m_Direction = Vector3.forward;
    public float m_Speed = 3.0f;
    private List<Character> m_Character = new List<Character>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Character.Add(other.gameObject.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var character = other.gameObject.GetComponent<Character>();
            character.m_ExternalMovement = Vector3.zero;
            m_Character.Remove(character);
        }
    }

    private void Update()
    {
        foreach (Character character in m_Character)
        {
            character.m_ExternalMovement = m_Direction * m_Speed * Time.deltaTime;
        }
    }
}
