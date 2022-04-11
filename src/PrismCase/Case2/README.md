# Case2

窗口跳转的几个实现思路

## 使用Prism的Region

1. Shell1为主窗口，继承自Window类。

可以设置`SizeToContent="WidthAndHeight"`和`ResizeMode="NoResize"`，让窗口的尺寸随显示内容变化。
缺点是内容控件尺寸是固定的，如果需要变化就不能这样设置。

在Shell1ViewModel中，把Login1和Main1添加到Region，代码如下：

```csharp
    private void ExecuteLoaded()
    {
        _mainContentRegion = _regionManager.Regions["MainContentRegion"];
        _mainContentRegion.Add(_loginView);
        _mainContentRegion.Add(_mainView);
    }
```
订阅LoginEvent消息，根据消息切换到不同的View

构造函数：

```csharp
_eventAggregator.GetEvent<LoginEvent>().Subscribe(LoginMessageHandler);
```
消息处理：

```csharp
    private void LoginMessageHandler(bool loginState)
    {
        if (loginState)
        {
            _mainContentRegion.Activate(_mainView);
        }
        else
        {
            _mainContentRegion.Activate(_loginView);
        }
    }
```

2. Login1是登录窗口

在Login1ViewModel中，点击登录发送登录成功的消息
```csharp
    private void ExecuteLogin()
    {
        _eventAggregator.GetEvent<LoginEvent>().Publish(true);
    }
```

3. Main1是主窗口

在Main1ViewModel中，处理点击退出发送退出的消息

```csharp
    private void Exit()
    {
        _eventAggregator.GetEvent<LoginEvent>().Publish(false);
    }
```

