using UnityEngine;

namespace DesignPatterns.CommandPattern
{
    public class JumpCommand : Command
    {
        public override void Execute()
        {
            base.Execute();
            Debug.Log("执行Jump Command");
        }
    }
}