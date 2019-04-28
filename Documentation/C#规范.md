#### C#相关模块级代码说明
- 一般方法、类的注释格式
```
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
    /// public int TestClass (string _str)
    /// {
    ///         return GetZero();
    /// }
    /// </code>
    /// </example>
    public  int TestClass(string _str)
    {
        return 0;
    }
```