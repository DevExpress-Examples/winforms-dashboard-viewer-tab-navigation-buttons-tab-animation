Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraEditors
Imports System.Linq
Imports System.Windows.Forms

Namespace DashboardNextPrevTab

    Public Partial Class ViewerForm
        Inherits XtraForm

        Const slideShowIntervalInMilliseconds As Integer = 3000

        Private slideShowTimer As Timer = New Timer()

        Private ReadOnly Property TabContainer As TabContainerDashboardItem
            Get
                Dim dashboard As Dashboard = dashboardViewer.Dashboard
                Return TryCast(dashboard.Items.SingleOrDefault(Function(i) TypeOf i Is TabContainerDashboardItem), TabContainerDashboardItem)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler dashboardViewer.CustomizeDashboardTitle, AddressOf DashboardViewerCustomizeDashboardTitle
            AddHandler dashboardViewer.SelectedTabPageChanged, AddressOf DashboardViewerSelectedTabPageChanged
            dashboardViewer.UpdateDashboardTitle()
            SetUpTimer()
        End Sub

        Private Sub SetUpTimer()
            slideShowTimer.Interval = slideShowIntervalInMilliseconds
            AddHandler slideShowTimer.Tick, AddressOf SlideShowTimerTick
        End Sub

        Private Sub DashboardViewerCustomizeDashboardTitle(ByVal sender As Object, ByVal e As CustomizeDashboardTitleEventArgs)
            Dim nextTabItem As DashboardToolbarItem = New DashboardToolbarItem("Next tab", AddressOf NextPrevTabItemClick)
            nextTabItem.Tag = NextPrevValue.Next
            nextTabItem.SvgImage = imageCollection("Next")
            e.Items.Insert(0, nextTabItem)
            Dim prevTabItem As DashboardToolbarItem = New DashboardToolbarItem("Previous tab", AddressOf NextPrevTabItemClick)
            prevTabItem.Tag = NextPrevValue.Prev
            prevTabItem.SvgImage = imageCollection("Prev")
            e.Items.Insert(0, prevTabItem)
            Dim showTabHederItem As DashboardToolbarItem = New DashboardToolbarItem(TabContainer.ShowCaption, "Show tab headers", AddressOf ShowHideTabHeadersItemClick)
            showTabHederItem.SvgImage = imageCollection("ShowCaption")
            e.Items.Insert(0, showTabHederItem)
            Dim slideShowItem As DashboardToolbarItem = New DashboardToolbarItem(slideShowTimer.Enabled, "Slideshow", AddressOf SlideShowItemClick)
            slideShowItem.SvgImage = imageCollection("Slideshow")
            e.Items.Insert(0, slideShowItem)
        End Sub

        Private Sub SlideShowTimerTick(ByVal sender As Object, ByVal e As System.EventArgs)
            transitionManager.StartTransition(dashboardViewer)
            Try
                ShowNextPrevTab(NextPrevValue.Next)
            Finally
                transitionManager.EndTransition()
            End Try
        End Sub

        Private Sub ShowNextPrevTab(ByVal value As NextPrevValue)
            Dim tabContainer As TabContainerDashboardItem = Me.TabContainer
            If tabContainer IsNot Nothing Then
                Dim increment As Integer = If(value = NextPrevValue.Next, 1, -1)
                Dim tabContainerName As String = tabContainer.ComponentName
                Dim selectedIndex As Integer = dashboardViewer.GetSelectedTabPageIndex(tabContainerName)
                Dim pageCount As Integer = tabContainer.TabPages.Count
                dashboardViewer.SetSelectedTabPage(tabContainerName, (selectedIndex + pageCount + increment) Mod pageCount)
            End If
        End Sub

        Private Sub NextPrevTabItemClick(ByVal args As DashboardToolbarItemClickEventArgs)
            ShowNextPrevTab(CType(args.Item.Tag, NextPrevValue))
        End Sub

        Private Sub SlideShowItemClick(ByVal args As DashboardToolbarItemClickEventArgs)
            If args.Item.Checked.HasValue AndAlso args.Item.Checked.Value Then
                slideShowTimer.Start()
            Else
                slideShowTimer.Stop()
            End If
        End Sub

        Private Sub ShowHideTabHeadersItemClick(ByVal args As DashboardToolbarItemClickEventArgs)
            Dim tabContainer As TabContainerDashboardItem = Me.TabContainer
            If tabContainer IsNot Nothing Then
                If args.Item.Checked.HasValue AndAlso args.Item.Checked.Value Then
                    tabContainer.ShowCaption = True
                Else
                    tabContainer.ShowCaption = False
                End If
            End If
        End Sub

        Private Sub DashboardViewerSelectedTabPageChanged(ByVal sender As Object, ByVal e As SelectedTabPageChangedEventArgs)
            Dim tabContainer As TabContainerDashboardItem = Me.TabContainer
            If tabContainer IsNot Nothing AndAlso Not tabContainer.ShowCaption Then
                Dim selectedTabPageName As String = e.Page
                dashboardViewer.Dashboard.Title.Text = Me.TabContainer.TabPages(selectedTabPageName).Name
            End If
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If

            If disposing Then
                RemoveHandler slideShowTimer.Tick, AddressOf SlideShowTimerTick
                slideShowTimer.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub
    End Class

    Friend Enum NextPrevValue
        [Next]
        Prev
    End Enum
End Namespace
