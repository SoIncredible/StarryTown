using System;
using UI.Core;
using UnityEngine;

namespace Logic
{
    public class Launcher : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Creat(gameObject);
        }
    }
}