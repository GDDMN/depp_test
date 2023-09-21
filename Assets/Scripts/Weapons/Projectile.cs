using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float _speed;
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
    _rigidbody.velocity = Vector2.Reflect(_direction, collision.contacts[0].normal) * _speed;
  }
}
