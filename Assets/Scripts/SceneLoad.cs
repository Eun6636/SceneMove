using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void LoadMainScene()
    {
        Debug.Log("¿Ö¾ÈµÅ");
        SceneManager.LoadScene("MainScene");
    }
}