using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
namespace SeanTest
{
    public class TransitionManager : MonoBehaviour
    {
        private bool isFade;
        [SerializeField] private bool canTransition;
        public CanvasGroup fadeCanvasGroup;
        [SerializeField] float fadeDuration;

        public event Action OnBeforeSceneUnloadEvent;

        private void Start()
        {
            canTransition = true;
            //示範
            var MainMenuData = new MainMenu_PassSceneData()
            {
                playerName = "林軒宇",
                playerLevel = 997,
                playerHeight = 177.75f,
                isMan = true
            };
            TransitionToMainMenu(MainMenuData);
           
        }
        #region -- 傳送場景 --
        public void Transition(string fromSceneName, string toSceneName)
        {
            if (canTransition)
            {
                StartCoroutine(TransitionToSceneCoroutine(fromSceneName, toSceneName));
            }
        }
        public void Transition(string toSceneName)
        {
            if (canTransition)
            {
                StartCoroutine(TransitionToSceneCoroutine(toSceneName));
            }
        }
        private IEnumerator TransitionToSceneCoroutine(string toSceneName)
        {
            canTransition = false;
            yield return FadeToAlpha(1);
            var asyncLoad = SceneManager.LoadSceneAsync(toSceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            //設置新場景為激活場景
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
            yield return FadeToAlpha(0);
            canTransition = true;
        }
        private IEnumerator TransitionToSceneCoroutine(string fromSceneName, string toSceneName)
        {
            canTransition = false;
            yield return FadeToAlpha(1);
            yield return SceneManager.UnloadSceneAsync(fromSceneName);
            var asyncLoad = SceneManager.LoadSceneAsync(toSceneName, LoadSceneMode.Additive);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            //設置新場景為激活場景
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
            yield return FadeToAlpha(0);
            canTransition = true;
        }
        /// <summary>
        /// 場景轉換黑幕效果
        /// </summary>
        /// <param name="targetAlpha">0~1 0是透明 1是黑色</param>
        /// <returns></returns>
        private IEnumerator FadeToAlpha(float targetAlpha)
        {
            isFade = true;
            fadeCanvasGroup.blocksRaycasts = true;
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;
            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }
            fadeCanvasGroup.blocksRaycasts = false;
            isFade = true;
        }
        #endregion

        #region -- 轉換到指定場景 --
        public void TransitionToMainMenu(MainMenu_PassSceneData data)
        {
            CrossSceneInfoManager.instance.AddPassSceneData(SceneName.MainMenuSceneName, data);
            Transition(SceneName.MainMenuSceneName);
        }
      
        #endregion
    }
}