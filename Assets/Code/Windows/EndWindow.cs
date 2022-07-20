using Code.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Windows
{
    public class EndWindow : MonoBehaviour
    {
        [SerializeField]
        private Button _restartButton;
        
        [SerializeField] 
        private Button _exitButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(Restart);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(Restart);
            _exitButton.onClick.RemoveListener(Exit);
        }

        private void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void Exit() => 
            Application.Quit();
    }
}