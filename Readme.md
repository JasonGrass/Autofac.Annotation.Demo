# Autofac.Annotation.Demo

一个测试代码

---

`WeatherForecastController` 没有添加 Component 特性，
使用 Autofac 的 `RegisterAssemblyTypes` 实现 Controller 的注入。

现象：
WeatherForecastController 中的 IRecordService 中的 IRecordRepository 无法获取到实例。

P.S WeatherForecastController 中的 IRecordService 如果从构造函数注入则可以。
