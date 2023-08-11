using System.Collections;
using Config;
using Data;
using Message;
using Settings;
using UI.Core;
using UnityEngine;

namespace Logic
{
    public class Launcher : MonoBehaviour
    {
        private IEnumerator Start()
        {
            ConfigManager.Create();

            yield return null;

            InputSettingsManager.Creat();

            yield return null;

            DataManager.Create();

            yield return null;

            UIManager.Instance.OpenPage(UIType.MainMenu);
        }
    }
}