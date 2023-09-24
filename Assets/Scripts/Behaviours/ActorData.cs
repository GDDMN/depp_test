using System;
using UnityEngine;

[Serializable]
public struct ActorData
{
  [Range(0f, 1f)]
  public float Health;
  public string Name;
}
