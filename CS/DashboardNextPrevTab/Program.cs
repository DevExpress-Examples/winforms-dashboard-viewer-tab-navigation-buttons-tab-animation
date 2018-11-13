using System;
using System.Windows.Forms;

namespace DashboardNextPrevTab {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "The Bezier";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerForm());
        }
    }
}
