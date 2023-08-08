using UnityEngine;

namespace DesignPatterns.CommandPattern
{
    public class FireCommand : Command
    {
        public override void Execute()
        {
            base.Execute();
            Debug.Log("执行Fire Command");
        }
    }
}