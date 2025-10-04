# WheelSpeedQML2

基于 Qt Quick Controls 2 的车轮速度模拟界面，重新设计以贴近现有的 C# 桌面程序风格。项目包含：

- **ApplicationWindow** 布局，左侧为控制面板，右侧为轮子动画展示。
- **WheelDisplay** 自定义组件，使用 Canvas 绘制轮子并支持旋转动画。
- 可设置转速、角度、锯条数以及采样频率，并提供简单的笔记记录区域。

构建方式：

```bash
mkdir build && cd build
cmake .. && cmake --build .
```

运行生成的 `WheelSpeedQML2` 可执行文件即可体验界面。