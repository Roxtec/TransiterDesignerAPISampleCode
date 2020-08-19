using System.Drawing;
using System.Windows.Forms;

namespace BrowserWrapper
{
    partial class BrowserForm
    {
        /// <summary>
        /// Modify the contents of this method to customize the browser wrapper UI. 
        /// </summary>
        private void CustomizeUI()
        {
            Text = "Roxtec Transit Designer";
            WindowState = FormWindowState.Maximized;

            // Set a custom text on the Exit button
            exitButton.Text = "Exit and return to calling application";

            // Make the exit button stand out more, visually.
            exitButton.Font = new Font(exitButton.Font.FontFamily, 14);
            exitButton.BackColor = Color.Aqua;
        }
    }
}
