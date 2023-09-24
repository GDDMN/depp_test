using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : Singleton<MainWindow>
{ 
  public Header Header;
  
  [SerializeField] private HealthBar _healthBar;

  public void CreateHealthbar(Actor actorPosition, string name)
  {
    var bar = Instantiate(_healthBar, actorPosition.transform.position + Vector3.up, Quaternion.identity);
    bar.transform.SetParent(transform);
    bar.Init(actorPosition);
    bar.SetName(name);
  }
}
