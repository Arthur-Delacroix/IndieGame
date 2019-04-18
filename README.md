# Unity3D iPad UDP 远程控制
![Release](https://img.shields.io/badge/Release-Ver1.0.0-green.svg) ![license](https://img.shields.io/badge/License-MIT-blue.svg) ![Platform](https://img.shields.io/badge/Platform-iOS丨Android-blueviolet.svg)

---

##### 计划功能
- [ ] UDP基础启动
- [ ] UDP传输，传输16进制编码或者字符串
- [ ] 与UE4双端验证
- [ ] 配置根据JSON定义
- [ ] 鼠标位置传输
- [ ] 按钮层级自动生成
- [ ] 按钮自动生成
- [ ] iPad iPhone Android

##### 基本思路
- 传输层和逻辑层的代码分开，意思就是。比如Unity中点击一个按钮，传输信息到UE4，这个顺序是先从逻辑层到传输层，再由传输层通过网络到UE4。UE4信息传来，先由传输层处理，通知虚幻4确实收到了这个信息，然后交由逻辑层，通过命令列表出租哦相应处理
- IP地址登录问题，程序打开时只有一个默认的IP登录窗口，通过“+”可以新增其他设备的IP地址，当用户新建一个设备输入好IP和端口号之后，点击“连接”，该地址会被记录到JSON中，下次打开APP也会有。也可以通过“-”删除一个设备的连接信息。

##### 基本功能
- 填写设备IP和端口，登录
- 沙盘鸟瞰
- 园林漫游
- 周边配套
- 宣楼
- 选户型
- 户型内漫游
- 户型鸟瞰
- 一房一景，选楼层

##### 模块划分
- JSON读取写入

##### 已完成模块
- [x] 无

---

```java
//一般方法、类的注释格式
    /// <summary>
    /// 填写这个方法的文字简介
    /// </summary>
    /// <param name="_str">参数说明</param>
    /// <returns>返回值说明</returns>
    /// <remarks> 
    /// 该方法详细文字说明 <see cref="Lb.Model.Dto.DtoBanner"/>
    /// </remarks>
    /// <example>
    /// 代码示例说明
    /// <code>
    /// class TestClass 
    /// {
    ///     static int Main() 
    ///     {
    ///         return GetZero();
    ///     }
    /// }
    /// </code>
    /// </example>
    public static int GetZero()
    {
        return 0;
    }

```

---

##### 传输的类及其对应的字符串名称
|Class|ClassType|
|---|---|
|ButtonInfo.class|ButtonInfo|
|OneTouch.class|OneTouch|

```java
//基础传输类
public class BaseClass
{
    //传输的数据类型
    //此字段为判断data使用哪个类来接收/传输的重要依据
    public string classType;

    //传输的数据，不同的数据接收的类也不同
    public object data;
}

//存储单个按钮的信息
public class ButtonInfo
{
    //在Unity上显示的按钮文字
    public string buttonName;

    //按钮所触发的动作名称
    //该值由UE4传到Unity，当按钮在Unity中被点击后，会将这个值传回UE4，具体触发的功能由UE4来决定
    public string actionName;
}

//单指触摸，Unity发送数据给UE4
//对应漫游控制视角方向 和 站立720
public class OneTouch
{
    //单指在屏幕上移动时，X和Y的delta值
    public float deltaX;
    public float deltaY;
}
```