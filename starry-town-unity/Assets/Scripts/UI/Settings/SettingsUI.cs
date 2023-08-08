using System.Collections.Generic;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public class SettingsUI : BaseUI
    {
        [SerializeField] private List<Text> _settingsTexts = new List<Text>();
        [SerializeField] private List<Button> _settingsBindButonList = new List<Button>();
    }
}