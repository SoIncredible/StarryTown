using UnityEngine;

namespace DesignPatterns.CommandPattern
{
    public class SwapWeaponCommand : Command
    {
        public override void Execute()
        {
            base.Execute();
            Debug.Log("执行Swap Weapon Command");
        }
    }
}