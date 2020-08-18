// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE-CefSharp file.

using BrowserWrapper.Controls;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;

namespace BrowserWrapper
{
    public partial class BrowserForm : Form
    {
        public const string DefaultApplicationUrl = "https://transitdesigner.roxtec.com";

        private ChromiumWebBrowser browser;

        public BrowserForm()
            : this(DefaultApplicationUrl)
        {
        }


        public BrowserForm(string applicationUrl)
        {
            CustomInitializeComponent(applicationUrl);
        }

        private void CustomInitializeComponent(string applicationUrl)
        {
            InitializeComponent();

            Text = "Roxtec Transit Designer";
            WindowState = FormWindowState.Maximized;

            browser = new ChromiumWebBrowser(applicationUrl);
            toolStripContainer.ContentPanel.Controls.Add(browser);

            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;

            // Make the exit button stand out more.
            // The application can be exited using this button or the standard window close button. 
            exitButton.Font = new Font(exitButton.Font.FontFamily, 14);
            exitButton.BackColor = Color.Aqua;
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                browser.Dispose();
                Cef.Shutdown();
            }
            catch
            {
                // Ignore error since we're about to close anyway
            }

            base.OnFormClosing(e);
        }
    }
}
