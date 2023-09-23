using UnityEngine;
using System;
using System.Collections;

public class ActorShooting : MonoBehaviour
{
  public event Action<int> OnShoot;
  public event Action<float> OnReloading;

  [SerializeField] private Projectile _projectile;
  [SerializeField] private Transform _shootPosition;
  [SerializeField] private float _reloadingSpeed;

  private bool _canShoot = true;
  private int _lostAmmos = 0;
  private int _ammos = 5;

  private float _time = 0f;
  private float _reloadingTime = 1f;

  private void Start()
  {
    OnShoot += Header.Instance.Baraban.BulletRemove;
    OnReloading += Header.Instance.Baraban.SetCounterValue;
  }

  public void Shoot()
  {
    if (!_canShoot)
      return;

    OnShoot?.Invoke(_lostAmmos);
    var projectile = Instantiate(_projectile, _shootPosition.position , Quaternion.identity);
    Vector2 direction = _shootPosition.up; 
    Debug.Log(direction);
    projectile.Init(direction);

    if(_lostAmmos > _ammos-1)
    {
      _canShoot = false;
      StartReloadingCoroutine();
      return;
    }
    _lostAmmos++;
  }

  private void StartReloadingCoroutine()
  {
    StartCoroutine(Reloading());
  }
  
  private IEnumerator Reloading()
  {
    while(_time <= _reloadingTime)
    {
      _time += _reloadingSpeed * Time.deltaTime;
      Debug.Log(_time);
      OnReloading?.Invoke(_time);
      yield return null;
    }

    OnReloading?.Invoke(_reloadingTime);
    Header.Instance.Baraban.ReloadBullets();

    _time = 0f;
    _canShoot = true;
    _lostAmmos = 0;
  }
}
