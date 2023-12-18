using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _pauseMenuRestartButton;
    [SerializeField] private Button _pauseMenuResumeButton;
    [SerializeField] private TextMeshProUGUI _killCounterText;
    private bool _isTextChanged = false;

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartButton);
        _exitButton.onClick.AddListener(ExitButton);
        _pauseButton.onClick.AddListener(() => PauseMenu(true));
        _pauseMenuRestartButton.onClick.AddListener(RestartButton);
        _pauseMenuResumeButton.onClick.AddListener(() => PauseMenu(false));
    }
    private void Update()
    {
        PlayerIsDead();
        if(Input.GetKeyUp(KeyCode.T)) { PauseMenu(true); }
    }
    private void PlayerIsDead() 
    {
        if (PlayerStats.Instance.GetIsPlayerDead()) 
        {
            _deathCanvas.SetActive(true);
            ChangeText();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    private void ChangeText() 
    {
        if (!_isTextChanged) 
        {
            _killCounterText.text = _killCounterText.text + PlayerStats.Instance.GetKillsCounter();
            _isTextChanged = true;
        }
        
    }
    private void RestartButton() 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void ExitButton() 
    {
        Application.Quit();
    }
    private void PauseMenu(bool value) 
    {
        if(value) 
        {
            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else 
        {
            Time.timeScale = 1f;
            _pauseCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }
}
