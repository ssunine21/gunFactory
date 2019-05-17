using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;
	public float m_TurretSpeed = 100f;
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;
	public Transform m_Turret;
	public float m_PitchRange = 0.2f;

	private string m_MovementAxisName;
    private string m_TurnAxisName;
	private Transform m_Transform;
	private Rigidbody m_Rigidbody;
	private Vector3 m_mousePosition;
	private float m_MovementInputValue;
	private float m_TurnInputValue;
	private float m_OriginalPitch;

	private Ray ray;
	private RaycastHit hit;


	private void Awake() {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_Transform = GetComponent<Transform>();
	}


	private void OnEnable () {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable () {
        m_Rigidbody.isKinematic = true;
    }


    private void Start() {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update() {
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

		EngineAudio();
    }


    private void EngineAudio() {
		if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f ) {
			if(m_MovementAudio.clip == m_EngineDriving ) {
				m_MovementAudio.clip = m_EngineIdling;

				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
		else {
			if(m_MovementAudio.clip == m_EngineIdling ) {
				m_MovementAudio.clip = m_EngineDriving;

				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
    }


    private void FixedUpdate() {
		Move();
		Turn();

		MoveTurret();
    }


    private void Move() {
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn() {
		if ( m_MovementInputValue < 0f ) m_TurnInputValue *= -1;

		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

	private void MoveTurret() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 300f, Color.green);

		Physics.Raycast(ray, out hit, 300f);

		m_mousePosition = hit.point - m_Transform.position;
		m_mousePosition.y = m_Transform.position.y;

		Quaternion rotTurret = Quaternion.LookRotation(m_mousePosition);
		m_Turret.rotation = Quaternion.Slerp(m_Transform.rotation, rotTurret, m_TurretSpeed * Time.deltaTime);
	}
}