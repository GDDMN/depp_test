using UnityEngine;

public class ActorShooting : MonoBehaviour
{
  [SerializeField] private Projectile _projectile;
  [SerializeField] private Transform _shootPosition;
  
  public void Shoot()
  {
    var projectile = Instantiate(_projectile, _shootPosition.position , Quaternion.identity);
    Vector2 direction = _shootPosition.up; 
    Debug.Log(direction);
    projectile.Init(direction);
    Destroy(projectile, .5f);
  }
}
