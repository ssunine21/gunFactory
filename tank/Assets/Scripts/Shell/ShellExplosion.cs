using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;
	public float m_Duration = 3f;
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
		Collider[] colliders = Physics.OverlapSphere(this.transform.position, m_ExplosionRadius, m_TankMask);

		foreach (var collider in colliders ) {
			Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();

			if ( !targetRigidbody ) continue;

			targetRigidbody.AddExplosionForce(m_ExplosionForce, this.transform.position, m_ExplosionRadius);
			TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

			if ( !targetHealth ) continue;

			float damage = CalculateDamage(targetRigidbody.position);

			targetHealth.TakeDamage(damage);
		}

		m_ExplosionParticles.transform.parent = null;
		m_ExplosionParticles.Play();

		Destroy(m_ExplosionParticles.gameObject, m_Duration);
		Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
		Vector3 explosionToTarget = targetPosition - this.transform.position;

		float explosionDistance = explosionToTarget.magnitude;
		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

		float damege = relativeDistance * m_MaxDamage;

		damege = Mathf.Max(0f, damege);
		return damege;
    }
}