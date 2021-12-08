Imports System
Imports System.Windows.Forms

Namespace DashboardNextPrevTab

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "The Bezier"
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New ViewerForm())
        End Sub
    End Module
End Namespace
