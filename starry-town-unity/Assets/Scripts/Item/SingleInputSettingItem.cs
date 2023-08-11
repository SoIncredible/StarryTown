using Config;
using Listener;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    public class SingleInputSettingItem : BaseItem
    {
        [SerializeField] private Text inputCommandText;
        [SerializeField] private Button bindButton;
        [SerializeField] private Text bindButtonText;
        [SerializeField] private Button alternateBindButton;
        [SerializeField] private Text alternateBindButtonText;

        public override void OnEnter(params object[] args)
        {
            if (args.Length == 3)
            {
                inputCommandText.text = args[0] as string;
                bindButtonText.text = args[1] as string;
                alternateBindButtonText.text = args[2] as string;
            }
            else
            {
                Debug.LogError("传入的参数不正确！");
            }
        }

        public override void AddEvent()
        {
            bindButton.onClick.AddListener(OnChangeBindButton);
            alternateBindButton.onClick.AddListener(OnChangeAlternateBindButton);
        }

        public override void RemoveEvent()
        {
            bindButton.onClick.RemoveAllListeners();
            alternateBindButton.onClick.RemoveAllListeners();
        }


        private void OnChangeBindButton()
        {
            bindButtonText.text = "";

            KeyBoardListenerManager.Instance.TurnOnKeyBoardInputListening(
                ConfigManager.Instance.GetOneConfig(inputCommandText.text), true);
        }

        private void OnChangeAlternateBindButton()
        {
            alternateBindButtonText.text = "";
            KeyBoardListenerManager.Instance.TurnOnKeyBoardInputListening(
                ConfigManager.Instance.GetOneConfig(inputCommandText.text), false);
        }
    }
}