using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float _speed;
  [Header("Visual")]
  [SerializeField] private ParticleSystem destroyParticle;
  [SerializeField] private ParticleSystem ricochetParticle;
  private Vector2 _direction;
  private Rigidbody2D _rigidbody;

  public void Init(Vector2 direction)
  {
    _direction = direction;
    _rigidbody = GetComponent<Rigidbody2D>();
    _rigidbody.velocity = _direction * _speed;
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    _direction = Vector2.Reflect(_direction, collision.contacts[0].normal).normalized;
    _rigidbody.velocity = _direction * _speed;

    var particle = Instantiate(ricochetParticle, transform.position, Quaternion.identity);
    Destroy(particle, .3f);
  }
}
