Namespace DashboardNextPrevTab

    Partial Class ViewerForm

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim transition1 As DevExpress.Utils.Animation.Transition = New DevExpress.Utils.Animation.Transition()
            Dim slideFadeTransition1 As DevExpress.Utils.Animation.SlideFadeTransition = New DevExpress.Utils.Animation.SlideFadeTransition()
            Me.dashboardViewer = New DevExpress.DashboardWin.DashboardViewer(Me.components)
            Me.imageCollection = New DevExpress.Utils.SvgImageCollection(Me.components)
            Me.transitionManager = New DevExpress.Utils.Animation.TransitionManager(Me.components)
            CType((Me.dashboardViewer), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.imageCollection), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' dashboardViewer
            ' 
            Me.dashboardViewer.AllowMaximizeDashboardItems = False
            Me.dashboardViewer.AllowPrintDashboard = False
            Me.dashboardViewer.Appearance.BackColor = System.Drawing.SystemColors.Control
            Me.dashboardViewer.Appearance.Options.UseBackColor = True
            Me.dashboardViewer.DashboardSource = New System.Uri("Dashboards\SalesDashboard.xml", System.UriKind.Relative)
            Me.dashboardViewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dashboardViewer.Location = New System.Drawing.Point(0, 0)
            Me.dashboardViewer.Name = "dashboardViewer"
            Me.dashboardViewer.Size = New System.Drawing.Size(1016, 564)
            Me.dashboardViewer.TabIndex = 0
            ' 
            ' imageCollection
            ' 
            Me.imageCollection.Add("Next", "image://svgimages/arrows/next.svg")
            Me.imageCollection.Add("Prev", "image://svgimages/arrows/prev.svg")
            Me.imageCollection.Add("Slideshow", "image://svgimages/icon builder/business_presentation.svg")
            Me.imageCollection.Add("ShowCaption", "image://svgimages/dashboards/showcaption.svg")
            ' 
            ' transitionManager
            ' 
            Me.transitionManager.ShowWaitingIndicator = False
            transition1.BarWaitingIndicatorProperties.Caption = ""
            transition1.BarWaitingIndicatorProperties.Description = ""
            transition1.Control = Me.dashboardViewer
            transition1.LineWaitingIndicatorProperties.AnimationElementCount = 5
            transition1.LineWaitingIndicatorProperties.Caption = ""
            transition1.LineWaitingIndicatorProperties.Description = ""
            transition1.RingWaitingIndicatorProperties.AnimationElementCount = 5
            transition1.RingWaitingIndicatorProperties.Caption = ""
            transition1.RingWaitingIndicatorProperties.Description = ""
            slideFadeTransition1.Parameters.EffectOptions = DevExpress.Utils.Animation.PushEffectOptions.FromRight
            transition1.TransitionType = slideFadeTransition1
            transition1.WaitingIndicatorProperties.Caption = ""
            transition1.WaitingIndicatorProperties.Description = ""
            Me.transitionManager.Transitions.Add(transition1)
            ' 
            ' ViewerForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1016, 564)
            Me.Controls.Add(Me.dashboardViewer)
            Me.Name = "ViewerForm"
            Me.Text = "Dashboard Viewer"
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
            CType((Me.dashboardViewer), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.imageCollection), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

#End Region
        Private dashboardViewer As DevExpress.DashboardWin.DashboardViewer

        Private imageCollection As DevExpress.Utils.SvgImageCollection

        Private transitionManager As DevExpress.Utils.Animation.TransitionManager
    End Class
End Namespace
