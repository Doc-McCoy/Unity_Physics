using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Vector3 m_Direction = Vector3.up;
    public float m_Force = 50.0f;
    public float m_Time = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            Character character = other.gameObject.GetComponent<Character>();
            StartCoroutine(Shoot(character));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            StopAllCoroutines();
        }
    }

    private IEnumerator Shoot(Character character)
    {
        yield return new WaitForSeconds(m_Time);

        Vector3 velocity = character.m_Velocity;
        velocity.x = m_Direction.x != 0 ? 0 : m_Direction.x;
        velocity.y = m_Direction.y != 0 ? 0 : m_Direction.y;
        velocity.z = m_Direction.z != 0 ? 0 : m_Direction.z;

        character.m_Velocity += m_Direction * m_Force;
    }
}
