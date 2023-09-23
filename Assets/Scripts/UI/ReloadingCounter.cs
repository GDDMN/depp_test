using UnityEngine;
using UnityEngine.UI;

public class ReloadingCounter : MonoBehaviour
{
  [SerializeField] private Image _counter;

  public Image Counter => _counter;
}
