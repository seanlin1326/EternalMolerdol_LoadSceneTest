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
            Debug.Log("�W�[");
            passSceneDataDictionary[dataKey] = dataValue;
        }
        /// <summary>
        ///�q�������Ʀr�夤�����ơA�ñN��Ʋ����r��
        /// </summary>
        public IPassSceneData GetPassSceneDataAndRemoveFromDictionary(string dataKey)
        {
            Debug.Log("ahah");
            if (passSceneDataDictionary.ContainsKey(dataKey))
            {
                Debug.Log("���F");
                var getDara = passSceneDataDictionary[dataKey];
                passSceneDataDictionary.Remove(dataKey);
                return getDara;
            }
            else
            {
                Debug.LogError("�䤣����");
                return null;
            }
        }
        
    }
}