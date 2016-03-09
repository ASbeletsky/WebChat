﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebChat.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ChatChatApp",
                url: "chat/{*.}",
                defaults: new { controller = "Chat", action = "ClientIndex" }
            );

            routes.MapRoute(
                name: "AgentChatApp",
                url: "agent-dashboard/{*.}",
                defaults: new { controller = "Agent", action = "Index" }
            );

            routes.MapRoute(
                name: "StatisticApp",
                url: "statistic/{*.}",
                defaults: new { controller = "Statistic", action = "Index" }
            );

            routes.MapRoute(
                name: "WebChatMainScript",
                url: "chat-script",
                defaults:  new { controller = "CustomerApp", action = "GetMainScript" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
