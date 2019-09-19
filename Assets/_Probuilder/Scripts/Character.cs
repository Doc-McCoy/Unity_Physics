using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Inputs Name")]
    public string m_JumpButtonName = "Fire1";
    public string m_DashButtonName = "Fire2";
    public string m_HorizontalAxisName = "Horizontal";
    public string m_VerticalAxisName = "Vertical";

    [Header("Abilities")]
    public float m_Speed = 5.0f;
    public float m_JumpHeight = 2.0f;
    public float m_DashDistance = 5.0f;
    public bool m_UseDoubleJump = true;
    
    [Header("Dynamic")]
    public float m_Gravity = -9.18f;
    public Vector3 m_Drag = new Vector3(8.0f, 0.0f, 8.0f);

    [Header("Ground")]
    public Transform m_GroundChecker;
    public float m_GroundDistance = 0.1f;
    public LayerMask m_GroundLayer;

    private Vector3 m_Velocity;
    private bool m_CanDoubleJump;
    private bool m_IsGrounded;
    private CharacterController m_Controller;

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        m_IsGrounded = Physics.CheckSphere(
            m_GroundChecker.position, // Posição do checker
            m_GroundDistance, // Raio do checker
            m_GroundLayer, // Camada de colisão
            QueryTriggerInteraction.Ignore // Deixar isso como ignore pra evitar falsos positivos
        );

        if (m_IsGrounded && m_Velocity.y < 0.0f) m_Velocity.y = 0.0f;

        // Verificar inputs de movimento
        float horizontal = Input.GetAxis(m_HorizontalAxisName);
        float vertical = Input.GetAxis(m_VerticalAxisName);
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);

        m_Controller.Move(movement * m_Speed * Time.deltaTime);

        // Setar a frente do player sempre na direção que ele está andando (caso ele esteja andando)
        if (movement != Vector3.zero) transform.forward = movement;

        // Verificar inputs de pulo
        bool jump = Input.GetButtonDown(m_JumpButtonName);
        if (jump)
        {
            if (m_IsGrounded)
            {
                // Eu não faço a mínima idéia do que ele faz aqui, mas pula
                m_Velocity.y += Mathf.Sqrt(m_JumpHeight * -2.0f * m_Gravity);
                m_CanDoubleJump = true;
            }
            else if (m_UseDoubleJump && m_CanDoubleJump)
            {
                m_Velocity.y = 0.0f; // Matar a velocidade vertical para evitar bug
                m_Velocity.y += Mathf.Sqrt(m_JumpHeight * -2.0f * m_Gravity);
                m_CanDoubleJump = false;
            }
        }

        // Verificar inputs de dash
        bool dash = Input.GetButtonDown(m_DashButtonName);
        if (dash)
        {
            // Se eu nao entendi aquela porra la em cima, imagina essa, mas da dash
            m_Velocity += Vector3.Scale(
                transform.forward,
                m_DashDistance * new Vector3(
                    Mathf.Log(1.0f / (Time.deltaTime * m_Drag.x + 1.0f)) / -Time.deltaTime,
                    0.0f,
                    Mathf.Log(1.0f / (Time.deltaTime * m_Drag.z + 1.0f)) / -Time.deltaTime
                )
            );
        }

        // Aplicar a gravidade
        m_Velocity.y += m_Gravity * Time.deltaTime;

        // Aplicar resistência quando o personagem não estiver se movimentando
        m_Velocity.x /= 1.0f + m_Drag.x * Time.deltaTime;
        m_Velocity.y /= 1.0f + m_Drag.y * Time.deltaTime;
        m_Velocity.z /= 1.0f + m_Drag.z * Time.deltaTime;

        m_Controller.Move(m_Velocity * Time.deltaTime);
    }
}
