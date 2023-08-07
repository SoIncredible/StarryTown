namespace Test.TestInterface
{
    public class ImplementInterface : BaseImplementInterface, IMyInterface
    {
        public bool IsDestroyed()
        {
            return true;
        }

        public void Function()
        {
        }
    }
}