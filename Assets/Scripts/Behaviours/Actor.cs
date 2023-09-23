using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ActorData
{
  [Range(0f, 1f)]
  public float Health;
}

public abstract class Actor : MonoBehaviour, IDamageable
{
  public event Action OnTakeDamage;  

  [Header("Actor data")]
  public ActorData actorData;

  [HideInInspector] public UnityAction groundedOnPlatform;
  [HideInInspector] public UnityAction getHurt;
  [HideInInspector] public UnityAction OnDeath;
  [HideInInspector] public bool Hurt;
  
  public void Deth()
  {
    OnDeath.Invoke();
    Destroy(gameObject);
  }

  public void TakeDamage(Collision2D collision, Projectile projectile)
  {
    actorData.Health -= projectile.Damage;
    OnTakeDamage?.Invoke();

    if (actorData.Health <= 0f)
      Deth();
  }
}