using UnityEngine;

namespace Listener;

public class KeyBoardListenerManager : MonoBehaviour
{
    public static KeyBoardListenerManager Instance;


    public bool IsOnListening { get; set; }

    public static void Creat()
    {
        if (Instance == null)
        {
            Instance = new KeyBoardListenerManager();
        }
    }


    public void StartListenKeyBoardInputForSettings()
    {
        // 为更改按键设置添加监听事件
        // 按键的监听是在Update中调用的，
    }

    public void TurnOnKeyBoardInputListening()
    {
    }

    public void ListenKeyBoardInput()
    {
        // 监听键盘输入并且将输入的值记录下来传给item
    }
}