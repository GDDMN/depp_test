using System;
using UnityEngine;

public abstract class Actor : MonoBehaviour, IDamageable
{
  public event Action<float> OnTakeDamage;
  public event Action OnDeath;

  [Header("Visual")]
  [SerializeField] private ParticleSystem _dethFX;
  [SerializeField] private ParticleSystem _hitFX;

  [Header("Actor data")]
  public ActorData actorData;
  

  private void Start()
  {
    MainWindow.Instance.CreateHealthbar(this, actorData.Name);
  }

  private void Update()
  {
    if (transform.position.y <= -16f)
      Deth();
  }

  public void Deth(Projectile projectile)
  {
    OnDeath?.Invoke();
    projectile.DestroyProgectile();
    var particle = Instantiate(_dethFX, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

  public void Deth()
  {
    OnDeath?.Invoke();
    var particle = Instantiate(_dethFX, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

  public void TakeDamage(Collision2D collision, Projectile projectile)
  {
    actorData.Health -= projectile.Damage;
    OnTakeDamage?.Invoke(actorData.Health);
    var particle = Instantiate(_hitFX, transform.position, Quaternion.identity);

    if (actorData.Health <= 0f)
      Deth(projectile);
  }
}