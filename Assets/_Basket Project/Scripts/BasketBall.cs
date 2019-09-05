using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public Rigidbody m_BallPrefab;
    public float m_Force;
    public Camera m_Camera;


    private void Update()
    {   
        if(Input.GetButtonDown("Fire1"))
        {
            Rigidbody ball = Instantiate<Rigidbody>(m_BallPrefab);
            ball.transform.position = transform.position;
            // Direção que a câmera ta olhando * Vetor 'pra frente' * força
            ball.AddForce(m_Camera.transform.rotation * Vector3.forward * m_Force, ForceMode.VelocityChange);
        }

    }

}
