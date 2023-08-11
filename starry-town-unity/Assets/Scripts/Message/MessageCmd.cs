namespace Message
{
    public enum MessageCmd
    {
        OnGameStart,
        OnGameEnd,
        OnGameSave,

        OnUIManagerFinishCreate,


        // 更改输入设置后的请求

        ChangeInputSettingSuccess,

        SettingsUIRefresh,
        // 但是由于输入的配置是excel表中配置的，还不能使用事件中心这一套东西

        // 测试UGUI组件用的命令
        OnTestBtnClicked
    }
}