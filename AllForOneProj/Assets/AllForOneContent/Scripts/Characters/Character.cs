using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	//Atributes
	[Header("Stats")]
	public float speed;
	public float runSpeed;
	public float strength;
	public float health;
	public float defense;
	public float jumpForce;

	[Header("Base")]
	public bool isPossessed;
	public PlayerController M_Pc;
	public Team team;
	public float gravity = -9.81f;
	public interactable m_CurrentItem;
	public Transform weaponLoc;

	[Header("Camera Targets")]
	public Transform camTarget;
	public Transform camSelectTarget;
	public Transform springTarget;
	public Transform aimTarget;

	[Header("Selection UI")]
	public GameObject selectUI;
	

	private Vector3 m_Velocity;
	private CharacterController m_CharCtrl;

	void Awake()
	{
		m_CharCtrl = GetComponent<CharacterController>();
	}

	void Update()
	{
		Movement();
		if (isPossessed)
		{ 
			Jump();
			Aim();
			if (Input.GetMouseButtonDown(0))
			{
				Attack();
			}
			if (Input.GetKey(KeyCode.E))
			{
				PickUp();
			}

			
		}
	}

	void Movement()
	{
		if (m_CharCtrl.isGrounded && m_Velocity.y < 0)
		{
			m_Velocity.y = -2;
		}

		if (isPossessed && GameMode.currentFlowState == FlowState.Round_Fight)
		{
			float currentSpeed;

			if (Input.GetKey(KeyCode.LeftShift))
			{
				currentSpeed = runSpeed;
			}
			else
			{
				currentSpeed = speed;
			}

			float mouseX = Input.GetAxis("Mouse X");
			float mouseY = Input.GetAxis("Mouse Y");

			transform.eulerAngles += transform.up * mouseX;
			springTarget.eulerAngles -= Vector3.right * mouseY;


			float hAxis = Input.GetAxis("Horizontal");
			float vAxis = Input.GetAxis("Vertical");
			Vector3 movement = transform.right * hAxis + transform.forward * vAxis;
			m_CharCtrl.Move(movement.normalized * currentSpeed * Time.deltaTime);

		}
		m_Velocity.y += gravity * Time.deltaTime;
		m_CharCtrl.Move(m_Velocity * Time.deltaTime);
	}

	void Jump()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (m_CharCtrl.isGrounded)
			{
				m_Velocity.y *= -1 * jumpForce;
			}
		}
	}

	void Attack()
	{
		if (!m_CurrentItem)
		{
			//Punch Enemy
			Ray punchRay = new Ray(transform.position, (transform.forward * 2));
			Debug.DrawRay(transform.position, (transform.forward * 2), Color.red, 1);
			if (Physics.Raycast(punchRay, out RaycastHit hitInfo, 4))
			{
				if (hitInfo.collider.gameObject)
				{
					hitInfo.collider.gameObject.GetComponent<Character>().health -= strength;
				}
			}
		}
		else
		{
			//Use Weapon
			m_CurrentItem.Use(this);
		}
	}

	void Fortify()
	{

	}

	void PickUp()
	{
		Ray camRay = new Ray(M_Pc.cameraManager.playerCamera.transform.position, M_Pc.cameraManager.playerCamera.transform.forward * 100);
		
		if(Physics.Raycast(camRay,out RaycastHit hit, 500))
		{
			if (hit.collider.GetComponent<interactable>())
			{
				hit.collider.GetComponent<interactable>().Pickup(this);
			}
		}
	}

	void Aim()
	{
		if (Input.GetMouseButton(1))
		{
			M_Pc.cameraManager.SetCameraTarget(aimTarget);
		}
		else
		{
			M_Pc.cameraManager.SetCameraTarget(camTarget);
		}
	}



}
