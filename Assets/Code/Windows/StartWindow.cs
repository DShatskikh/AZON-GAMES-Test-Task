using Code.Heroes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Windows
{
    public class StartWindow : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;
        
        [SerializeField] 
        private Button _exitButton;

        [SerializeField] 
        private Hero _hero;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(Exit);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(Exit);
        }

        private void StartGame()
        {
            Time.timeScale = 1f;
            _hero.enabled = true;
            gameObject.SetActive(false);
        }

        private void Exit() => 
            Application.Quit();
    }
}