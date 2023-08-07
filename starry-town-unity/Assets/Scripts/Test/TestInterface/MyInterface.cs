namespace Test.TestInterface
{
    public interface IMyInterface
    {
        // 本接口为了测试作为bool类型返回值的方法是否可以在继承该接口的类中不被实现
        // 经过测试发现，即使是bool类型返回值的方法，其在被继承的类中也需要实现该方法
        bool IsDestroyed();
        void Function();

        void Method();
    }
}