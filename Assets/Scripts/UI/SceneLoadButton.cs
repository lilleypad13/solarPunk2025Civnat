using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;

    private void Awake()
    {
        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
