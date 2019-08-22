using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int m_MaxLife = 5;
    public Color m_MinColor = Color.blue;
    public Color m_MaxColor = Color.red;

    private int m_Life;
    private Renderer m_Renderer;

    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Life = Random.Range(1, m_MaxLife + 1);
        ChangeColor();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        m_Life--;
        if (m_Life < 1) Destroy(gameObject);
        ChangeColor();
    }

    private void ChangeColor()
    {
        Color color = Color.Lerp(m_MinColor, m_MaxColor, (m_Life / (float)m_MaxLife));
        m_Renderer.material.SetColor("_Color", color);
    }

}
