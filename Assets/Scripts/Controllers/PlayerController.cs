using UnityEngine;

public class PlayerController : Actor
{
  [SerializeField] private ActorMovements _actorMovements;
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
  }

  private void Update()
  {
    _direction = playerInput.Player.Run.ReadValue<float>();
    Run();

    //Run();
    //Jump();
    //Attack();
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
    //if (!_actorMovements.IsJumping && _actorMovements.OnGround && Mathf.Abs(_direction) < .01f)
    //{
    //  if (Input.GetButtonDown("Fire1"))
    //    actorAttack.StartAttack();
    //}
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