using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
  [Header("Data")]
  [SerializeField] private float _speed;
  [SerializeField] private float _lifetimeSpeed;
  [Header("Visual")]
  [SerializeField] private ParticleSystem _destroyParticle;
  [SerializeField] private ParticleSystem _ricochetParticle;
  
  private float _lifetime = 1f;
  private float _time = 0;

  [HideInInspector] public Rigidbody2D rigidbody;
  [HideInInspector] public Vector2 direction;

  public float Speed => _speed;
  public ParticleSystem RicochetParticle => _ricochetParticle;

  public void Init(Vector2 direction)
  {
    this.direction = direction;
    rigidbody = GetComponent<Rigidbody2D>();
    rigidbody.velocity = direction * _speed;

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
    var damageble = collision.gameObject.GetComponent<IDamageable>();

    if (damageble == null)
      return;

    damageble.TakeDamage(collision, this);
  }

  private void DestroyProgectile()
  {
    var particle = Instantiate(_destroyParticle, transform.position, Quaternion.identity);
    Destroy(this.gameObject);
  }
}
