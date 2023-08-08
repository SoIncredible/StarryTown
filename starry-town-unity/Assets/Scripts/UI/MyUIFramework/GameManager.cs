using System;
using UnityEngine;

namespace UI.MyUIFramework
{
    public class GameManager : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float a = 0.5f;


        public float A
        {
            get => a;
            set => a = value;
        }


        [SerializeField] private MyData _myData;


        private void Start()
        {
            _myData = new MyData();
            _myData.DataName = "TestJson";
            _myData.DataLevel = 6;
            _myData.DataHealth = 75.0f;


            string jsonData = JsonUtility.ToJson(_myData);
            Debug.Log("Serialized JSON Data" + jsonData);

            MyData myDataTest = JsonUtility.FromJson<MyData>(jsonData);


            Debug.Log("Deserialize Data Name:" + myDataTest.DataName);
            Debug.Log("Deserialized Data Level:" + myDataTest.DataLevel);
            Debug.Log("Deserialized Data Health:" + myDataTest.DataHealth);
        }
    }

    [Serializable]
    public class MyData
    {
        public string DataName;
        public int DataLevel;
        public float DataHealth;
    }
}