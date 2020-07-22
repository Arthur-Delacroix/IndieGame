### 相关知识点记录
- 导入角色资源
pixels per unit 表示一个Unity内的单位，可以容纳一张贴图的几个像素

- Filter Mode 当该纹理由于3D变换进行拉伸时，它将如何被过滤插值。共有三种选择
	- Point 单点插值，纹理将变得块状化（blocky up close）
	- Bilinear 双线性插值，纹理将变得模糊（blurry up close）
	- Trilinear 三线性插值，类似Bilinear，但是纹理还会在不同的mip水平之间（between the different mip levels）进行模糊

- 在控制人物移动的时候，不要强行控制人物的position，而是控制刚体组件，这样就不会存在被物体挡住产生抖动的情况了

- Animator 相关
	- 一个物体有多个animation clip的情况下，会使用到Animator进行各个动画之间的切换
	- 在两个动画之间的transition，选中后，inspector面板中关闭has Exit Time（无退出等待时间，立即开启下一动画）
	- translation duration 两段动画融合过渡的时长

- 可以在Animator的Parameters标签中创建变量
	- 例如创建了bool类型的变量，在代码中可以通过anim.SetBool("isMoving", true);语句为该变量赋值

- OnBecameInvisible 当gameobject消失在屏幕内触发的事件

- Random.Range参数，min包含，max排除

- Layers之间的的忽略关系，在Edit -> Project Settings -> Physics2D -> Layer Collision Matrix 中进行设置

- UGUI中的文字outline和shadow，可以多次叠加，描边线是基于上一次次的轮廓继续描边，影子是基于上一次影子的外边缘添加影子
	- 利用此特性可以实现有立体感的文字等效果

- 当animation录制的时候，在Inspector面板上，在Transform的Position问字处右键，会弹出一个菜单，点击“Add key”就可以在当前点创建关键帧

- Animator中，trigger和bool的区别
	- bool只根据你代码设置确定是真或者假，想根据某条件控制状态机时使用。
	- trigger是触发器，代码设置后为真，状态根据此条件处理过一次后重设为假，可以当“事件”使用。

- 在绘制Tilemap地图时，旋转Tilemap中的Tile元素，使用键盘上的"[" 和 "]" 按键

- composite Collider 2D 作用是将多个相邻，且带Tilemap collider 2D的物体的 collider合并，看作一个整体

### [TODO]
- 重新设计程序结构，抽象相关的类，UML
	- Sequence diagram 时序图
	- Use case diagram 用例图
	- Class diagram 类图
	- Activity diagram 活动图
	- Component diagram 组件图
	- State diagram 状态图
	- Object diagram 对象图
	- Deployment diagram 部署图
	- Timing diagram 定时图

- 跨平台操作

- 人物dash时产生拖尾的粒子效果

- 敌人子弹消失时的粒子效果，类似玩家子弹消失的粒子效果

- 玩家被子弹击中，或者踩到陷阱时候的流血效果（类似怪物被集中的流血效果）

- 玩家数据，关卡数据要序列化存储，可以使用playablescript存储起来，进行解耦和
例如某些脚本中的固定数据，每个房间之间的横向 纵向距离

- 关卡生成的时候使用了Physics2D.OverlapCircle，但是感觉应该可以使用更加程序化的方式来判断，生成房间时候两个房间是否重叠

- 关卡生成的时候使用了一个很笨的挨个判断的方式，虽然达到了目的，但是感觉效率很低，应该有更高效的办法

- 在关卡生成的时候，感觉可以将墙面和地面放在一个物体下集中生成，而不用通过脚本依赖的方式将二者连接起来

- 敌人 玩家 武器 等数据参数，要序列化持久存储，例如scriptableobject或者json

- 多个敌人的各种属性最好使用继承

- 目前敌人不会像玩家控制的人物那样翻转，即面朝方向和移动方向相同，需要修改

- 子弹 粒子等效果，需要对象池

- 需要一个计时器类，在游戏过程中各个地方试用的计时方法，都用这个类代替
	- [参考链接](https://github.com/akbiggs/UnityTimer)
	- [其中asmdef文件相关资料](https://blog.csdn.net/iningwei/article/details/91046449)

- [UML相关知识链接](https://www.bilibili.com/video/BV11b411c7hp)

- 多语言配置