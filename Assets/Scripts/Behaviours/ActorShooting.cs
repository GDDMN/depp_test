using UnityEngine;

public class ActorShooting : MonoBehaviour
{
  [SerializeField] private Projectile _projectile;
  [SerializeField] private Transform _shootPosition;
  
  public void Shoot(Vector2 direction)
  {
    var projectile = Instantiate(_projectile, _shootPosition.position , Quaternion.identity);
    projectile.Init(direction);
  }
}
