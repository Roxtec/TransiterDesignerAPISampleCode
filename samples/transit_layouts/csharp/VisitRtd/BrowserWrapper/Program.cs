// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Forms;
using CefSharp;

namespace BrowserWrapper
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            // For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            // We are using our current exe as the BrowserSubProcess
            // Multiple instances will be spawned to handle all the 
            // Chromium processes, render, gpu, network, plugin, etc.
            var subProcessExe = new CefSharp.BrowserSubprocess.BrowserSubprocessExecutable();
            var result = subProcessExe.Main(args);
            if (result > 0)
            {
                return result;
            }

            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"),
            };

            // We use our Applications exe as the BrowserSubProcess, multiple copies
            // will be spawned
            var exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            settings.BrowserSubprocessPath = exePath;

            // Specify the standard user data path, to reuse login cookies etc.
            settings.UserDataPath = @$"{localAppData}\Google\Chrome\User Data";
            
            // Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            var applicationUrl = args.Length > 0 ? args[0] : BrowserForm.DefaultApplicationUrl;
            var browser = new BrowserForm(applicationUrl);
            Application.Run(browser);

            return 0;
        }
    }
}
