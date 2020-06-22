using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStats
{
	public float m_Speed;
	public float m_Strength;
	public float m_Defense;
	public float m_Health;
}

public class Character : MonoBehaviour
{
	//Atributes
	[Header("Stats")]
	
	public PlayerStats m_PlayerStats;
	private PlayerStats m_SavedStats;
	//public float speed;
	//public float runSpeed;
	//public float strength;
	//public float health;
	//public float defense;
	public float jumpForce;

	
	[HideInInspector] public bool isPossessed;
	[HideInInspector] public PlayerController M_Pc;
	[Header("Base")]
	public Team team;
	public float gravity = -9.81f;
	public interactable m_CurrentItem;
	public Transform weaponLoc;
	public Animator m_Animator;

	[Header("Camera Targets")]
	public Transform camTarget;
	public Transform camSelectTarget;
	public Transform springTarget;
	public Transform aimTarget;

	[Header("Selection UI")]
	public GameObject selectUI;

	private Vector3 m_Velocity;
	private CharacterController m_CharCtrl;
	public List<PowerUp> currentPowerUps = new List<PowerUp>();
	


	void Awake()
	{
		m_CharCtrl = GetComponent<CharacterController>();
		m_SavedStats = m_PlayerStats;

		for (int i = 0; i < 3; i++)
		{
			currentPowerUps.Add(null);
		}
		
	}

	void Update()
	{
		Movement();
		if (isPossessed)
		{
			Jump();
			Aim();
			UsePowerUps();
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
			float currentSpeed = m_PlayerStats.m_Speed;

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
					hitInfo.collider.gameObject.GetComponent<Character>().m_PlayerStats.m_Health -= m_PlayerStats.m_Strength;
					hitInfo.collider.gameObject.GetComponent<Character>().CheckIsAlive();
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
		Ray camRay = new Ray(M_Pc.m_CameraManager.playerCamera.transform.position, M_Pc.m_CameraManager.playerCamera.transform.forward * 100);

		if (Physics.Raycast(camRay, out RaycastHit hit, 500))
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
			M_Pc.m_CameraManager.SetCameraTarget(aimTarget);
		}
		else
		{
			M_Pc.m_CameraManager.SetCameraTarget(camTarget);
		}
	}

	void UsePowerUps()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			
			if (currentPowerUps[0] != null)
			{
				Debug.Log("Trying to Use PowerUp 1");
				currentPowerUps[0].Use(this);
				currentPowerUps[0] = null;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			
			if (currentPowerUps[1] != null)
			{
				Debug.Log("Trying to Use PowerUp 2");
				currentPowerUps[1].Use(this);
				currentPowerUps[1] = null;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			
			if (currentPowerUps[2] != null)
			{
				Debug.Log("Trying to Use PowerUp 3");
				currentPowerUps[2].Use(this);
				currentPowerUps[2] = null;
			}
		}
	}

	public void CheckIsAlive()
	{
		if (m_PlayerStats.m_Health <= 0)
		{
			Destroy(this.gameObject);
			if (team == Team.Red)
			{
				GameMode.m_TeamRed.Remove(this.gameObject);
			}
			else if (team == Team.Blue)
			{
				GameMode.m_TeamBlue.Remove(this.gameObject);
			}
		}
	}

	public void AddPowerUp(PowerUp pwrUp, int index)
	{
			currentPowerUps[index] = pwrUp;
	}

	public IEnumerator ResetStats(PowerUpType PUType, int Seconds)
	{

		yield return new WaitForSeconds(Seconds);
		switch (PUType)
		{
			case PowerUpType.Rage:
				m_PlayerStats.m_Strength = m_SavedStats.m_Strength;

				break;
			case PowerUpType.TimeMachine:
				M_Pc.timerPaused = false;
				break;
			case PowerUpType.Adrenaline:
				m_PlayerStats.m_Speed = m_SavedStats.m_Speed;
				break;
			default:
				break;
		}


	}

}