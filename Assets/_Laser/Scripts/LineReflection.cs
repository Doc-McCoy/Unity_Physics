using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]

public class LineReflection : MonoBehaviour
{
    public int m_MaxReflectionCount = 3;
    public float m_MaxDistance = 200.0f;
    public float m_SpeedTextureOffset = 5.0f;
    public bool m_Debug = true;

    private LineRenderer m_Renderer;

    private void Awake()
    {
        m_Renderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 position, Vector3 direction, float remaining, int index)
    {
        if (remaining <= 0.0f) return;

        Vector3 origin = position;
        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        // Verificar se o laser bateu em algo
        if (Physics.Raycast(ray, out hit, remaining))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * remaining;
        }

        if (!m_Debug)
        {
            if (index == 0)
            {
                m_Renderer.SetPosition(index, origin);
                index++;
            }

            m_Renderer.positionCount = index + 1;
            m_Renderer.SetPosition(index, position);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, position);
        }

        remaining -= Vector3.Distance(origin, position);

        DrawLine(position, direction, remaining, index + 1);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.ArrowHandleCap(0, transform.position, transform.rotation, 2.0f, EventType.Repaint);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        DrawLine(transform.position, transform.forward, m_MaxDistance, 0);
    }

    private void Update()
    {
        if (m_Debug) return;
        
        float offset = Time.time * m_SpeedTextureOffset;
        m_Renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.0f));


        DrawLine(transform.position, transform.forward, m_MaxDistance, 0);
    }
}
