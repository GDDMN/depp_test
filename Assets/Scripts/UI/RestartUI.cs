using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUI : MonoBehaviour
{
  public void Click()
  {
    Debug.Log("CLick");
    SceneManager.LoadScene("game");
  }
}
