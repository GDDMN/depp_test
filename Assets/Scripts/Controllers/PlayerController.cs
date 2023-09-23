using UnityEngine;

public class PlayerController : Actor
{
  [SerializeField] private ActorMovements _actorMovements;
  [SerializeField] private ActorShooting _actorShooting;
  private PlayerInput playerInput;
  private float _direction;
  private Vector3 mousePos;

  private void Awake()
  {
    playerInput = new PlayerInput();
  }

  private void OnEnable()
  {
    playerInput.Enable();

    playerInput.Player.Shoot.performed += context => Attack();
    playerInput.Player.Jump.performed += context => Jump();
  }

  private void OnDisable()
  {
    playerInput.Disable();

    playerInput.Player.Shoot.performed -= context => Attack();
    playerInput.Player.Jump.performed -= context => Jump();
  }

  private void FixedUpdate()
  {
    _direction = playerInput.Player.Run.ReadValue<float>();

    Run();
    mousePos = Camera.main.ScreenToWorldPoint(playerInput.Player.Aiming.ReadValue<Vector2>());

    if (_actorMovements.IsJumping)
      _actorMovements.JumpAnimation();
  }


  private void Run()
  {
    _actorMovements.Run(_direction);
    _actorMovements.Rotation(mousePos);
  }

  private void Attack()
  {
    //Vector2 direction = (mousePos - transform.position).normalized;
    _actorShooting.Shoot();
  }

  private void Jump()
  {
    _actorMovements.Jump();
  }
}