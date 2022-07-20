using Code.Heroes;
using Code.Spawners;
using Code.Windows;
using TMPro;
using UnityEngine;

namespace Code.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] 
        private Hero _hero;

        [SerializeField] 
        private TextMeshProUGUI _scoreText;

        [SerializeField] 
        private EntitySpawner _entitySpawner;

        [SerializeField] 
        private StartWindow _startWindow;

        private SessionDataService _sessionDataService;
        
        private void Start()
        {
            _sessionDataService = new SessionDataService(_scoreText);
            _hero.Init();
            _entitySpawner.Init(_sessionDataService, _hero);
            _entitySpawner.StartSpawn();

            if (FindObjectsOfType<Bootstrapper>().Length > 1)
                Destroy(gameObject);
            else
            {
                DontDestroyOnLoad(gameObject);
                _hero.enabled = false;
                _startWindow.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}