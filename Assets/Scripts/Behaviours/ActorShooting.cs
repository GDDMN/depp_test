using UnityEngine;

public class ActorShooting : MonoBehaviour
{
  [SerializeField] private Projectile _projectile;
  [SerializeField] private float _speed;

  public void Shoot(Vector2 direction)
  {
    var projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
    projectile.GetComponent<Rigidbody2D>().velocity = _speed * direction;
  }
}
