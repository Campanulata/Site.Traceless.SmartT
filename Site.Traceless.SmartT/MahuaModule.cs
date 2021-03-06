﻿using Autofac;
using AutoMapper;
using Newbe.Mahua;
using Newbe.Mahua.Apis;
using Newbe.Mahua.MahuaEvents;
using Site.Traceless.SmartT.Func;
using Site.Traceless.SmartT.MahuaEvents;
using Site.Traceless.SmartT.Service;

namespace Site.Traceless.SmartT
{
    /// <summary>
    /// Ioc容器注册
    /// </summary>
    public class MahuaModule : IMahuaModule
    {
        public Module[] GetModules()
        {
            // 可以按照功能模块进行划分，此处可以改造为基于文件配置进行构造。实现模块化编程。
            return new Module[]
            {
                new PluginModule(),
                new MahuaEventsModule(),
                new ReMindModule()
            };
        }

        /// <summary>
        /// 基本模块
        /// </summary>
        private class PluginModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                // 将实现类与接口的关系注入到Autofac的Ioc容器中。如果此处缺少注册将无法启动插件。
                // 注意！！！PluginInfo是插件运行必须注册的，其他内容则不是必要的！！！
                builder.RegisterType<PluginInfo>()
                    .As<IPluginInfo>();

                //注册在“设置中心”中注册菜单，若想订阅菜单点击事件，可以查看教程。http://www.newbe.cf/docs/mahua/2017/12/24/Newbe-Mahua-Navigations.html
                builder.RegisterType<MyMenuProvider>()
                    .As<IMahuaMenuProvider>();
                MapperConfig.RegisterMappers();
            }
        }

        /// <summary>
        /// <see cref="IMahuaEvent"/> 事件处理模块
        /// </summary>
        private class MahuaEventsModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                // 将需要监听的事件注册，若缺少此注册，则不会调用相关的实现类
                builder.RegisterType<PluginEnabledMahuaEvent>().As<IPluginEnabledMahuaEvent>();
                builder.RegisterType<GroupMessageReceivedMahuaEvent>().As<IGroupMessageReceivedMahuaEvent>();
                builder.RegisterType<PrivateMessageFromFriendReceivedMahuaEvent>().As<IPrivateMessageFromFriendReceivedMahuaEvent>();
                builder.RegisterType<ExceptionOccuredMahuaEvent>().As<IExceptionOccuredMahuaEvent>();
                builder.RegisterType<InitializationMahuaEvent>().As<IInitializationMahuaEvent>();
                

            }
        }

        private class ReMindModule : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                base.Load(builder);
                builder.RegisterType<ServerRemind>()
                    .As<IServerRemind>()
                    .SingleInstance();
            }
        }
    }
}
