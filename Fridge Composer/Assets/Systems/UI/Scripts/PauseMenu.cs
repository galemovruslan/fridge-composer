using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TextMeshProUGUI _title;

    private PauseSwitcher _switcher;


    private const string _pauseTitleText = "Pause";
    private const string _pauseWinText = "You Win";
    private const string _pauseLoseText = "You Lose";

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

    public void ShowPause(PauseSwitcher pauseSwitcher)
    {
        _title.text = _pauseTitleText;
        Show(pauseSwitcher, true);
    }

    public void ShowWin(PauseSwitcher pauseSwitcher)
    {
        _title.text = _pauseWinText;
        Show(pauseSwitcher, false);
    }

    public void ShowLose(PauseSwitcher pauseSwitcher)
    {
        _title.text = _pauseLoseText;
        Show(pauseSwitcher, false);
    }

    private void Show(PauseSwitcher pauseSwitcher, bool hasUnpauseButton )
    {
        
        _switcher = pauseSwitcher;
        _continueButton.gameObject.SetActive(hasUnpauseButton);
        _continueButton.onClick.AddListener(() => _switcher.ResetPause());
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _continueButton.onClick.RemoveAllListeners();
    }

}
