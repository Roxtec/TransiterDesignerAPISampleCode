// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE-CefSharp file.

using BrowserWrapper.Controls;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

namespace BrowserWrapper
{
    public partial class BrowserForm : Form
    {
        public const string DefaultApplicationUrl = "https://transitdesigner.roxtec.com";

        private ChromiumWebBrowser browser;
        private SaveCompletion saveCompletion;

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

            // Setup save completion, which allows us to call into Transit Designer to force a save
            // before exiting.
            saveCompletion = new SaveCompletion(browser);
        }

        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            var b = ((ChromiumWebBrowser)sender);

            this.InvokeOnUiThreadIfRequired(() => b.Focus());
        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            saveCompletion.TrySave().ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    // The transit could not be saved. Perhaps this is because of a network error.
                    // The sample does not include a retry mechanism.
                    var exception = t.Exception?.Flatten() ?? new Exception("Unknown failure");
                    MessageBox.Show(
                        exception.Message, 
                        "Failed to save the transit",
                        MessageBoxButtons.OK);
                }
                else
                {
                    // The transit was saved, or saving wasn't necessary (perhaps because no changes
                    // were made, or the auto-save function ran already).
                    // This dialog is shown for demo/debug purpose only - it is recommended to remove
                    // it in the real application.
                    var didSave = t.Result;
                    MessageBox.Show(
                        $"All changes have been sent to the server (didSave = {didSave})",
                        "Transit saved",
                        MessageBoxButtons.OK);

                }

                Close();
            }, TaskScheduler.FromCurrentSynchronizationContext());
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
