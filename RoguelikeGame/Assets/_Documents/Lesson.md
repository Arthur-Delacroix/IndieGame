1. Step1
- 导入角色资源
pixels per unit 表示一个Unity内的单位，可以容纳一张贴图的几个像素

Filter Mode 当该纹理由于3D变换进行拉伸时，它将如何被过滤插值。共有三种选择：
Point 单点插值，纹理将变得块状化（blocky up close）；
Bilinear 双线性插值，纹理将变得模糊（blurry up close）；
Trilinear 三线性插值，类似Bilinear，但是纹理还会在不同的mip水平之间（between the different mip levels）进行模糊；