using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.CommandPattern
{
    public class CommandHandler : MonoBehaviour
    {
        // 本代码用来处理按钮被按下的时候需要执行对应的操作

        [SerializeField] private Button _buttonA;

        [SerializeField] private Button _buttonX;

        [SerializeField] private Button _buttonY;

        [SerializeField] private Button _buttonB;


        private Command buttonW;
        private Command buttonA;
        private Command _buttonS;
        private Command buttonD;


        private void Start()
        {
            buttonA = new Command();
            buttonW = new Command();
            _buttonS = new Command();
            buttonD = new Command();
        }

        private void Update()
        {
            // if (Input.GetKey())
        }
    }
}