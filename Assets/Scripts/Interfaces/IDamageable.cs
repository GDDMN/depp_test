using UnityEngine;

public interface IDamageable
{
  public abstract void TakeDamage(Collision2D collision, Projectile projectile);

  public abstract void Deth(Projectile projectile);
}
