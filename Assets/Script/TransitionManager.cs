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
            //�ܽd
            var MainMenuData = new MainMenu_PassSceneData()
            {
                playerName = "�L�a�t",
                playerLevel = 997,
                playerHeight = 177.75f,
                isMan = true
            };
            TransitionToMainMenu(MainMenuData);
           
        }
        #region -- �ǰe���� --
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
            //�]�m�s�������E������
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
            //�]�m�s�������E������
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
            yield return FadeToAlpha(0);
            canTransition = true;
        }
        /// <summary>
        /// �����ഫ�¹��ĪG
        /// </summary>
        /// <param name="targetAlpha">0~1 0�O�z�� 1�O�¦�</param>
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

        #region -- �ഫ����w���� --
        public void TransitionToMainMenu(MainMenu_PassSceneData data)
        {
            CrossSceneInfoManager.instance.AddPassSceneData(SceneName.MainMenuSceneName, data);
            Transition(SceneName.MainMenuSceneName);
        }
      
        #endregion
    }
}