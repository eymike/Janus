﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JanusDesktop.Content;

namespace Janus
{
    class Program
    {
        static void Main(string[] args)
        {
            var swapChainFactory = new JanusDesktop.Graphics.DesktopSwapChainFactory();
            swapChainFactory.Show();

            var app = new JanusEngine.Application(
                swapChainFactory, 
                swapChainFactory,
                new DesktopFileSystem());

            app.Run();
        }
    }
}
