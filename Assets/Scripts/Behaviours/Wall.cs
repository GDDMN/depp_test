using UnityEngine;

public class Wall : MonoBehaviour, IDamageable
{
  public void Deth(Projectile projectile)
  {

  }

  public void TakeDamage(Collision2D collision, Projectile projectile)
  {
    projectile.direction = Vector2.Reflect(projectile.direction, collision.contacts[0].normal).normalized;
    projectile.rigidbody.velocity = projectile.direction * projectile.Speed;

    var particle = Instantiate(projectile.RicochetParticle, projectile.transform.position, Quaternion.identity);
    Destroy(particle, .3f);
  }
}
