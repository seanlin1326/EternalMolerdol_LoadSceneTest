using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeanTest
{
    public class CrossSceneInfoManager : MonoBehaviour
    {
        #region -- Singleton -- 
        public static CrossSceneInfoManager instance;
   
        private void SingletonInit()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
        }
        #endregion
        public void Awake()
        {
            SingletonInit();
        }

        private Dictionary<string, IPassSceneData> passSceneDataDictionary = new Dictionary<string, IPassSceneData>();

        public void AddPassSceneData(string dataKey, IPassSceneData dataValue)
        {
            Debug.Log("增加");
            passSceneDataDictionary[dataKey] = dataValue;
        }
        /// <summary>
        ///從跨場景資料字典中獲取資料，並將資料移除字典
        /// </summary>
        public IPassSceneData GetPassSceneDataAndRemoveFromDictionary(string dataKey)
        {
            Debug.Log("ahah");
            if (passSceneDataDictionary.ContainsKey(dataKey))
            {
                Debug.Log("找到了");
                var getDara = passSceneDataDictionary[dataKey];
                passSceneDataDictionary.Remove(dataKey);
                return getDara;
            }
            else
            {
                Debug.LogError("找不到資料");
                return null;
            }
        }
        
    }
}