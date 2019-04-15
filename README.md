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