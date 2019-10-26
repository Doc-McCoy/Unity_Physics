using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKinematics : MonoBehaviour
{
	[Header("Player Controller")]
	public string m_HorizontalAxisName = "Horizontal";
	public string m_VerticalAxisName = "Vertical";
	public string m_BrakeButtonName = "Jump";

	[Header("WheelColliders")]
	public WheelCollider m_WheelColliderFR;
	public WheelCollider m_WheelColliderFL;
	public WheelCollider m_WheelColliderRR;
	public WheelCollider m_WheelColliderRL;

	[Header("WheelMeshes")]
	public Transform m_WheelMeshFR;
	public Transform m_WheelMeshFL;
	public Transform m_WheelMeshRR;
	public Transform m_WheelMeshRL;

	[Header("Steering")]
	public float m_MinSteerAngle = 15.0f;
	public float m_MaxSteerAngle = 25.0f;
	public float m_MaxSpeedToSteerAngle = 60.0f;
	public float m_SmoothSteeringAngle = 10.0f;
	public bool m_UseStabilityCurves = true;

	[Header("Physics")]
	public Transform m_CenterOfGravity;
	public float m_MaxMotorTorque = 1500.0f;
	public float m_MaxReverseTorque = 500.0f;
	public float m_MaxDecelerationForce = 200.0f;
	public float m_BrakeForce = 3000.0f;

	[Header("Drive Mode")]
	public DriveMode m_DriveMode = DriveMode.All;
	public SpeedMode s_SpeedMode = SpeedMode.KilometersPerHours;
	public enum DriveMode { Front, Rear, All };
	public enum SpeedMode { MeterPerSecond, KilometersPerHours, MilesPerHour }

	public bool IsReverse => m_WheelColliderRL.rpm < 0.0f && m_WheelColliderRR.rpm < 0.0f;

	private Rigidbody m_Body;
	private float m_HorizontalInput;
	private float m_VerticalInput;
	private float m_BrakeInput;

	private void Start()
	{
		m_Body = GetComponent<Rigidbody>();
		m_Body.centerOfMass = m_CenterOfGravity.localPosition;
		// A variável acima você pega como localPosition poque deve
		// considerar que ele é filho do kart, e o Rigidbody está no
		// kart.
		// Caso queira deixar o centro de massa dinâmico, coloque isso
		// no Update.
	}

	private void Update()
	{
		m_HorizontalInput = Input.GetAxis(m_HorizontalAxisName);
		m_VerticalInput = Input.GetAxis(m_VerticalAxisName);
		m_BrakeInput = Input.GetAxis(m_BrakeButtonName);
	}

	private void FixedUpdate()
	{
		Steering();
		// Accelerate();
		// Braking();
		// Decelerate();
		// UpdateMeshes();
	}

	private void Steering()
	{
		float steerAngle = 0.0f;
		
		if (m_UseStabilityCurves)
		{
			// float speedFactor = Speed / m_MaxSpeedToSteerAngle;
			float speedFactor = 1.0f / m_MaxSpeedToSteerAngle;
			steerAngle = Mathf.Lerp(m_MaxSteerAngle, m_MinSteerAngle, speedFactor) * m_HorizontalInput;
		}
		else
		{
			steerAngle = m_MaxSteerAngle * m_HorizontalInput;
		}

		m_WheelColliderFL.steerAngle = Mathf.Lerp(m_WheelColliderFL.steerAngle, steerAngle, Time.deltaTime * m_SmoothSteeringAngle);
		m_WheelColliderFR.steerAngle = Mathf.Lerp(m_WheelColliderFR.steerAngle, steerAngle, Time.deltaTime * m_SmoothSteeringAngle);
	}
}