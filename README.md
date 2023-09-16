# EasyChair Core (.NET)

EasyChair Core 是 EasyChair 模拟联合国会议主持软件的通用库。该仓库提供的是基于 .NET 6.0 构建的版本，使用 C# 语言编写。

~~无他，主要是 Visual Studio 的 InteliSense 实在是太香了，不愧是宇宙最强 IDE~~

## 特性
* 基于 .NET 的全平台支持（支持 Windows Desktop/WPF/UWP、Linux、Web、（以下基于 Xamarin）Mac OS、iOS、Android）
* 可以使用 C# 和 Visual Basic 语言，开发 Web 端、桌面端、移动端以及 IoT 端？应用程序
* 完全的面向对象设计，实现会议规程模块化
* 完整的 Visual Studio API 文档支持，并可生成基于 [Doxygen](http://www.doxygen.org) 的完整 API 文档
 
## 提供内容
### 当前可用
* **EasyChair**：功能强大的模拟联合国会议主持软件
* [x] 会议类
* [x] 国家和国家列表
* [x] 发言名单
* [ ] 计时器（需要外部`Timer`控件支持）
* [ ] 投票

* **EasyInterlock**：便利的会场间联动系统
* [x] 会场模拟时间轴（需要外部`Timer`控件支持）
* [ ] 联动中心—会场间内容传递协议
* [ ] 联动中心端和会场端

### 计划中
* EasyWrite：快速生成会场文件的文本标记语言
* EasyVote：简化投票流程的服务—应用端表决器
* EasyLog：与 EasyChair 配合使用的会场记录器
* 基于议事规则的继承类

## 系统需求
* 已部署 .NET 6.0 及以上版本的 Linux 或 MacOS 或 Windows
* Visual Studio（在 Windows 环境下）

## 下载和使用
**请注意：** 该仓库提供的内容并非完整的软件，仅包含与软件相关的实现，只能用于应用程序的开发。请访问 [CRH380B-6216L/easychair](https://github.com/CRH380B-6216L/easychair) 获取完整的会议软件。

使用 `git clone` 克隆该库，或进入 [Releases 页面](https://github.com/CRH380B-6216L/easychair-core-dotnet/releases)下载已编译完成的 DLL 文件。

使用前，您**不需要**向任何人申请或申报，也**不限制**任何形式的使用、复制、变更、改进、再分发或商业性质销售，但您应依 MIT 协议，在“关于”界面添加本仓库（CRH380B-6216L/easychair-core-dotnet）的 License 声明。

推荐使用 GitHub 的 Fork 功能复制一份属于您的本仓库的副本以修改任何内容，并**欢迎任何积极的 Pull Request**！

### 使用 Visual Studio
#### 基于源代码
使用 `git clone` 克隆该库，在“解决方案资源管理器”中右键点击您的解决方案，点击`添加(D)->现有项目(E)...`，选择任何您需要的部件的 .csproj 文件。

#### 基于已编译的 DLL 库
进入 [Releases 页面](https://github.com/CRH380B-6216L/easychair-core-dotnet/releases)下载已编译完成的 DLL 文件，在“解决方案资源管理器”中右键点击您的项目，点击`添加(D)->现有项(G)...`，切换文件类型为“可执行文件”，选择任何您需要的部件的 .dll 文件。

## 声明
* 本产品基于 MIT 协议开源。
* 本产品使用 Microsoft 提供的 [.NET 平台](https://github.com/dotnet/core)。基于 MIT 协议开源。