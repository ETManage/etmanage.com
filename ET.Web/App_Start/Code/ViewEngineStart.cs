using PrecompiledMvcViewEngineContrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ET.Web
{
    public class ViewEngineStart
    {
        /// <summary>
        /// 初始化注册预编译视图
        /// </summary>
        public static void Start()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            var engine = new PrecompiledMvcEngine(new List<Assembly> {
                Assembly.Load("ET.Weixin.Web")
            });
            System.Web.Mvc.ViewEngines.Engines.Insert(0, engine);
            System.Web.WebPages.VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
    }
}