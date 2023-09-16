using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMovements : MonoBehaviour
{
  [SerializeField] private Transform _actorObject;

  [SerializeField] private AnimationCurve _jumpCurve;

  [SerializeField] private float _walkSpeed;

  [SerializeField] private float _jumpSpeed;
  [SerializeField] private float _jumpForce;

  public Transform _groundCheckPoint;

  private bool _onGround = false;
  private Vector3 _startPosition;
  private float _progress = 0.0f;

  public bool OnGround => _onGround;
  public bool IsJumping { get; private set; }

  private void Update()
  {
    //OnGroundCheck();
  }

  public void Run(float direction)
  {
    Vector3 startPosition = _actorObject.position;

    //float xPos = Mathf.Clamp(startPosition.x + direction * _walkSpeed * Time.deltaTime, -11.0f, 21.0f);
    float xPos = startPosition.x + direction * _walkSpeed * Time.deltaTime;
    _actorObject.position = new Vector3(xPos, startPosition.y, startPosition.z);
  }

  public void Jump()
  {
    if (!_onGround)
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

  private void OnGroundCheck()
  {
    float distance = 1.5f;

    Ray ray = new Ray(_groundCheckPoint.position, Vector3.down);
    RaycastHit hit;

    _onGround = false;
    if (Physics.Raycast(ray, out hit, distance))
    {
      if (hit.collider.gameObject.layer == 12)
        _onGround = true;
        IsJumping = false;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    IsJumping = false;
  }
}
