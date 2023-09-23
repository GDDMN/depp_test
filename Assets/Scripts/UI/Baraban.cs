using UnityEngine;
using UnityEngine.UI;

public class Baraban : MonoBehaviour
{
  [SerializeField] private Image[] _bullets = new Image[6];
  [SerializeField] private ReloadingCounter _counter;

  public void BulletRemove(int index)
  {
    _bullets[index].gameObject.SetActive(false);
  }

  public void ReloadBullets()
  {
    foreach (var bullet in _bullets)
      bullet.gameObject.SetActive(true);
  }

  public void SetCounterValue(float value)
  {
    _counter.Counter.fillAmount = value;
  }
}
