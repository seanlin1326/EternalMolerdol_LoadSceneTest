using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SeanTest
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Text playerNameText;
        [SerializeField] private Text playerLevelText;
        [SerializeField] private Text playerHeightText;
        [SerializeField] private Text playerIsMan;
         private MainMenu_PassSceneData initData;
        private void OnEnable()
        {
            Debug.Log("OnEnable");
            GetInitData();
            UpdateUIByPassData();
        }
        private void GetInitData()
        {
            var data = CrossSceneInfoManager.instance.GetPassSceneDataAndRemoveFromDictionary(SceneName.MainMenuSceneName);
            if (data is MainMenu_PassSceneData mainMenuData)
            {
                initData = mainMenuData;
            }
            else
            {
                Debug.LogError("抓不到資料");
                return;
            }
        }
        private void UpdateUIByPassData()
        {
            playerNameText.text = initData.playerName;
            playerLevelText.text = initData.playerLevel.ToString();
            playerHeightText.text = initData.playerHeight.ToString();
            playerIsMan.text = initData.isMan.ToString();
        }
    }
}