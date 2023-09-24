using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
  [SerializeField] private Image _counter;
  [SerializeField] private float _weightOffset;
  [SerializeField] private TextMeshProUGUI _name;
  private RectTransform _rectTransform;
  private Actor _observable;
  private Canvas canvas;

  public Image Counter => _counter;

  public void Init(Actor actor)
  {
    _observable = actor;
    _rectTransform = GetComponent<RectTransform>();
    canvas = FindObjectOfType<Canvas>();

    actor.OnTakeDamage += SetValue;
    actor.OnDeath += DestroyBar;
  }

  private void LateUpdate()
  {
    transform.position = GetCanvasPosition();
  }

  private Vector2 GetCanvasPosition()
  {
    Vector3 offset = new Vector2(0f, _weightOffset);
    return canvas.worldCamera.WorldToScreenPoint(_observable.gameObject.transform.position + offset);
  }

  private void SetValue(float value)
  {
    _counter.fillAmount = value;
  }

  public void SetName(string name)
  {
    _name.text = name;
  }

  private void DestroyBar()
  {
    _observable.OnTakeDamage -= SetValue;
    _observable.OnDeath -= DestroyBar;
    Destroy(gameObject);
  }
}
