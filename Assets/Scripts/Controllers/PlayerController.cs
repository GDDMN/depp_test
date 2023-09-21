using UnityEngine;

public class PlayerController : Actor
{
  [SerializeField] private ActorMovements _actorMovements;
  [SerializeField] private ActorShooting _actorShooting;
  private PlayerInput playerInput;
  private float _direction;

  private void Awake()
  {
    playerInput = new PlayerInput();
    playerInput.Player.Jump.performed += context => Jump();
  }

  private void OnEnable()
  {
    playerInput.Enable();
  }

  private void OnDisable()
  {
    playerInput.Disable();
  }

  private void Start()
  {
    playerInput.Player.Shoot.performed += context => Attack();
  }

  private void Update()
  {
    _direction = playerInput.Player.Run.ReadValue<float>();
    Run();
  }

  private void FixedUpdate()
  {
    if(_actorMovements.IsJumping)
      _actorMovements.JumpAnimation();
  }


  private void Run()
  {
    _direction = (Input.GetAxis("Horizontal"));
    _actorMovements.Run(_direction);
  }

  private void Attack()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(playerInput.Player.Aiming.ReadValue<Vector2>());
    Vector2 direction = mousePos - transform.position;
    _actorShooting.Shoot(direction);
  }

  private void Jump()
  {
    if (Input.GetButtonDown("Jump"))
      _actorMovements.Jump();
  }

  public override void Death()
  {
    //if (vertex != null)
    //  vertex.RemoveActorAction(this);
    OnDeath.Invoke();
    Destroy(gameObject);
  }
}