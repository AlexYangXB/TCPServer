//#define SimpleChinese   
#define TradionalChinese
using System.Reflection;

#if(SimpleChinese)

    [assembly: AssemblyDescription("康艺网点管理端")]
    [assembly: AssemblyTitle("广州康艺电子有限公司")]
    [assembly: AssemblyProduct("广州康艺电子有限公司")]
    [assembly: AssemblyCompany("广州康艺电子有限公司")]
    [assembly: AssemblyCopyright("Copyright © 广州康艺电子有限公司 2015")]

#endif

#if(TradionalChinese)

[assembly: AssemblyDescription("微克服務據點管理端")]
    [assembly: AssemblyTitle("V&T微克股份有限公司")]
    [assembly: AssemblyProduct("V&T微克股份有限公司")]
    [assembly: AssemblyCompany("V&T微克股份有限公司")]
    [assembly: AssemblyCopyright("Copyright © V&T微克股份有限公司 2015")]

#endif
// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“内部版本号”和“修订号”的默认值，
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0.0")]