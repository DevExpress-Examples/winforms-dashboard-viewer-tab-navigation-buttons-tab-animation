Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports DevExpress.XtraEditors
Imports System.Linq
Imports System.Windows.Forms

Namespace DashboardNextPrevTab
	Partial Public Class ViewerForm
		Inherits XtraForm

		Private Const slideShowIntervalInMilliseconds As Integer = 3000
		Private slideShowTimer As New Timer()
		Private ReadOnly Property TabContainer() As TabContainerDashboardItem
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
			Dim nextTabItem As New DashboardToolbarItem("Next tab", AddressOf NextPrevTabItemClick)
			nextTabItem.Tag = NextPrevValue.Next
			nextTabItem.SvgImage = imageCollection("Next")
			e.Items.Insert(0, nextTabItem)

			Dim prevTabItem As New DashboardToolbarItem("Previous tab", AddressOf NextPrevTabItemClick)
			prevTabItem.Tag = NextPrevValue.Prev
			prevTabItem.SvgImage = imageCollection("Prev")
			e.Items.Insert(0, prevTabItem)

			Dim showTabHederItem As New DashboardToolbarItem(TabContainer.ShowCaption, "Show tab headers", AddressOf ShowHideTabHeadersItemClick)
			showTabHederItem.SvgImage = imageCollection("ShowCaption")
			e.Items.Insert(0, showTabHederItem)

			Dim slideShowItem As New DashboardToolbarItem(slideShowTimer.Enabled, "Slideshow", AddressOf SlideShowItemClick)
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
'INSTANT VB NOTE: The variable tabContainer was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim tabContainer_Renamed As TabContainerDashboardItem = TabContainer
			If tabContainer_Renamed IsNot Nothing Then
				Dim increment As Integer = If(value = NextPrevValue.Next, 1, -1)
				Dim tabContainerName As String = tabContainer_Renamed.ComponentName
				Dim selectedIndex As Integer = dashboardViewer.GetSelectedTabPageIndex(tabContainerName)
				Dim pageCount As Integer = tabContainer_Renamed.TabPages.Count
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
'INSTANT VB NOTE: The variable tabContainer was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim tabContainer_Renamed As TabContainerDashboardItem = TabContainer
			If tabContainer_Renamed IsNot Nothing Then
				If args.Item.Checked.HasValue AndAlso args.Item.Checked.Value Then
					tabContainer_Renamed.ShowCaption = True
				Else
					tabContainer_Renamed.ShowCaption = False
				End If
			End If
		End Sub
		Private Sub DashboardViewerSelectedTabPageChanged(ByVal sender As Object, ByVal e As SelectedTabPageChangedEventArgs)
'INSTANT VB NOTE: The variable tabContainer was renamed since Visual Basic does not handle local variables named the same as class members well:
			Dim tabContainer_Renamed As TabContainerDashboardItem = TabContainer
			If tabContainer_Renamed IsNot Nothing AndAlso Not tabContainer_Renamed.ShowCaption Then
				Dim selectedTabPageName As String = e.Page
				dashboardViewer.Dashboard.Title.Text = TabContainer.TabPages(selectedTabPageName).Name
			End If
		End Sub
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
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
