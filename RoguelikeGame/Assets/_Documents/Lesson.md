1. Step1
- 导入角色资源
pixels per unit 表示一个Unity内的单位，可以容纳一张贴图的几个像素

Filter Mode 当该纹理由于3D变换进行拉伸时，它将如何被过滤插值。共有三种选择：
Point 单点插值，纹理将变得块状化（blocky up close）；
Bilinear 双线性插值，纹理将变得模糊（blurry up close）；
Trilinear 三线性插值，类似Bilinear，但是纹理还会在不同的mip水平之间（between the different mip levels）进行模糊；

- 在控制人物移动的时候，不要强行控制人物的position，而是控制刚体组件，这样就不会存在被物体挡住产生抖动的情况了

- Animator 相关
一个物体有多个animation clip的情况下，会使用到Animator进行各个动画之间的切换
在两个动画之间的transition，选中后，inspector面板中关闭has Exit Time（无退出等待时间，立即开启下一动画）
translation duration 两段动画融合过渡的时长

可以在Animator的Parameters标签中创建变量
例如创建了bool类型的变量，在代码中可以通过anim.SetBool("isMoving", true);语句为该变量赋值

OnBecameInvisible 当gameobject消失在屏幕内触发的事件

Random.Range参数，min包含，max排除

Layers之间的的忽略关系，在Edit -> Project Settings -> Physics2D -> Layer Collision Matrix 中进行设置

UGUI中的文字outline和shadow，可以多次叠加，描边线是基于上一次次的轮廓继续描边，影子是基于上一次影子的外边缘添加影子
利用此特性可以实现有立体感的文字等效果

当animation录制的时候，在Inspector面板上，在Transform的Position问字处右键，会弹出一个菜单，点击“Add kye”就可以在当前点创建关键帧

Animator中，trigger和bool的区别
bool只根据你代码设置确定是真或者假，想根据某条件控制状态机时使用。
trigger是触发器，代码设置后为真，状态根据此条件处理过一次后重设为假，可以当“事件”使用。