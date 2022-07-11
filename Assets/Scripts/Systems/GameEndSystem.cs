using System;
using System.Collections;
using EventClass;
using HackMan.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems
{
    public class GameEndSystem : Singleton<GameEndSystem>
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private float FadeOutTime = 0.5f;
        [SerializeField] private float FadeInTime = 1f;
        
        private void Start()
        {
            GetComponent<Image>().enabled = true;
        
            _canvasGroup = GetComponent<CanvasGroup>();

            StartCoroutine(FadeIn(FadeInTime));
        }

        private void OnEnable()
        {
            NewEventSystem.Instance.Subscribe<GameEndEvent>(OnGameEnd);
        }

        private void OnDisable()
        {
            NewEventSystem.Instance.Unsubscribe<GameEndEvent>(OnGameEnd);
        }

        private void OnGameEnd(GameEndEvent gameEndArgs)
        {
            if (gameEndArgs.IsGameOver)
            {
                GameOver();    
            }
        }

        private void GameOver()
        {
            StartCoroutine(EndGameTransition());
        }

        private IEnumerator EndGameTransition()
        {
            yield return FadeOut(FadeOutTime);

            yield return new WaitForSeconds(1.5f);
            
            yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        
        #region Fade

        public IEnumerator FadeOut(float time)
        {
            while (_canvasGroup.alpha < 1f)
            {
                _canvasGroup.alpha += Time.deltaTime / time;

                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (_canvasGroup.alpha > 0f)
            {
                _canvasGroup.alpha -= Time.deltaTime / time;

                yield return null;
            }
        }

        #endregion
    }
}