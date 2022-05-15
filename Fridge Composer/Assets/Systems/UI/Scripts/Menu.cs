using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;


    private PauseSwitcher _switcher;

    private void OnEnable()
    {
        _quitButton.onClick.AddListener(() => Application.Quit());
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void OnDisable()
    {
        _quitButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
        _continueButton.onClick.RemoveAllListeners();
    }

    public void Show( PauseSwitcher pauseSwitcher)
    {
        _switcher = pauseSwitcher;
        _continueButton.onClick.AddListener(() => _switcher.ResetPause());
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _continueButton.onClick.RemoveAllListeners();
    }

}
