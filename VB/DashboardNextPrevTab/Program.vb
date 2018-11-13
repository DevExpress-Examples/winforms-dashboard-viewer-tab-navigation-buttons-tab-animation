Imports System
Imports System.Windows.Forms

Namespace DashboardNextPrevTab
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		<STAThread>
		Shared Sub Main()
			DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "The Bezier"
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New ViewerForm())
		End Sub
	End Class
End Namespace
