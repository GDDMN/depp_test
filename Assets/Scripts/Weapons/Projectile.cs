using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
  [Header("Data")]
  [SerializeField] private float _speed;
  [SerializeField] private float _lifetimeSpeed;
  [Header("Visual")]
  [SerializeField] private ParticleSystem destroyParticle;
  [SerializeField] private ParticleSystem ricochetParticle;
  
  private float _lifetime = 1f;
  private float _time = 0;

  private Vector2 _direction;
  private Rigidbody2D _rigidbody;

  public void Init(Vector2 direction)
  {
    _direction = direction;
    _rigidbody = GetComponent<Rigidbody2D>();
    _rigidbody.velocity = _direction * _speed;

    StartLifeTimeCoroutine();
  }

  private void StartLifeTimeCoroutine()
  {
    StartCoroutine(LifeTime());
  }

  private IEnumerator LifeTime()
  {
    while(_time < _lifetime)
    {
      _time += _lifetimeSpeed * Time.deltaTime;
      yield return null;
    }

    DestroyProgectile();
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    _direction = Vector2.Reflect(_direction, collision.contacts[0].normal).normalized;
    _rigidbody.velocity = _direction * _speed;

    var particle = Instantiate(ricochetParticle, transform.position, Quaternion.identity);
    Destroy(particle, .3f);
  }

  private void DestroyProgectile()
  {
    var particle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
    Destroy(this.gameObject);
  }
}
