using UnityEngine;

public class ActorMovements : MonoBehaviour
{
  [SerializeField] private Transform _actorObject;

  [SerializeField] private AnimationCurve _jumpCurve;

  [SerializeField] private float _walkSpeed;

  [SerializeField] private float _jumpSpeed;
  [SerializeField] private float _jumpForce;
  
  [Header("Visual")]
  [SerializeField] private Animator _animator;

  public Transform _groundCheckPoint;

  private Vector3 _startPosition;
  private float _progress = 0.0f;
  private bool _IsRuning = false;

  private float groundCheckDistance = 0.3f;
  public bool IsJumping { get; private set; }


  //private void OnDrawGizmos()
  //{
  //  Gizmos.DrawLine(_groundCheckPoint.position, _groundCheckPoint.position + new Vector3(0f, -groundCheckDistance, 0f));
  //}

  public void Run(float direction)
  {
    Vector3 startPosition = _actorObject.position;

    float xPos = startPosition.x + direction * _walkSpeed * Time.deltaTime;
    _actorObject.position = new Vector3(xPos, startPosition.y, startPosition.z);

    if(direction != 0f)
      _IsRuning = true;
    else
      _IsRuning = false;

    _animator.SetBool("IsRunning", _IsRuning);
  }

  public void Rotation(Vector2 position)
  {
    Vector2 direction = new Vector2(position.x - transform.position.x, position.y - transform.position.y);
    transform.up = direction;
  }

  public void Jump()
  {
    Debug.Log(OnGroundCheck() + " , " + IsJumping);
    if (!OnGroundCheck())
      return;

    IsJumping = true;

    _progress = 0.0f;

    _startPosition = _actorObject.position;
  }

  public void JumpAnimation()
  {
    if (!IsJumping)
      return;

    _progress += _jumpSpeed * Time.deltaTime;
    float jumpEvaluation = _jumpCurve.Evaluate(_progress);
    float deltaYPos = _startPosition.y + (jumpEvaluation * _jumpForce);

    _actorObject.position = new Vector3(_actorObject.position.x,
                                        deltaYPos,
                                        _actorObject.position.z);

    if (_progress >= 1.0f)
      IsJumping = false;
  }

  public bool OnGroundCheck()
  {
    RaycastHit2D hit;
    hit = Physics2D.Raycast(_groundCheckPoint.position, Vector2.down, groundCheckDistance);

    if (hit.collider != null)
    {
      return true;
    }

    return false;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    IsJumping = false;
  }
}
