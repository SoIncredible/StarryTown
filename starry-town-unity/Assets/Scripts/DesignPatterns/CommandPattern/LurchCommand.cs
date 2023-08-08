using UnityEngine;

namespace DesignPatterns.CommandPattern
{
    public class LurchCommand : Command
    {
        public override void Execute()
        {
            base.Execute();
            Debug.Log("执行Lurch Command");
        }
    }
}