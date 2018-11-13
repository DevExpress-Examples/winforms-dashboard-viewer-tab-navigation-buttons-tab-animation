using DevExpress.DashboardCommon;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors;
using System.Linq;
using System.Windows.Forms;

namespace DashboardNextPrevTab
{
    public partial class ViewerForm : XtraForm {
        const int slideShowIntervalInMilliseconds = 3000;
        Timer slideShowTimer = new Timer();
        TabContainerDashboardItem TabContainer {
            get {
                Dashboard dashboard = dashboardViewer.Dashboard;
                return dashboard.Items.SingleOrDefault(i => i is TabContainerDashboardItem) as TabContainerDashboardItem;
            }
        }
        public ViewerForm() {
            InitializeComponent();

            dashboardViewer.CustomizeDashboardTitle += DashboardViewerCustomizeDashboardTitle;
            dashboardViewer.SelectedTabPageChanged += DashboardViewerSelectedTabPageChanged;

            SetUpTimer();
        }
        void SetUpTimer() {
            slideShowTimer.Interval = slideShowIntervalInMilliseconds;
            slideShowTimer.Tick += SlideShowTimerTick;
        }
        void DashboardViewerCustomizeDashboardTitle(object sender, CustomizeDashboardTitleEventArgs e) {
            DashboardToolbarItem nextTabItem = new DashboardToolbarItem("Next tab", NextPrevTabItemClick);
            nextTabItem.Tag = NextPrevValue.Next;
            nextTabItem.SvgImage = imageCollection["Next"];
            e.Items.Insert(0, nextTabItem);

            DashboardToolbarItem prevTabItem = new DashboardToolbarItem("Previous tab", NextPrevTabItemClick);
            prevTabItem.Tag = NextPrevValue.Prev;
            prevTabItem.SvgImage = imageCollection["Prev"];
            e.Items.Insert(0, prevTabItem);

            DashboardToolbarItem showTabHederItem = new DashboardToolbarItem(TabContainer.ShowCaption, "Show tab headers", ShowHideTabHeadersItemClick);
            showTabHederItem.SvgImage = imageCollection["ShowCaption"];
            e.Items.Insert(0, showTabHederItem);

            DashboardToolbarItem slideShowItem = new DashboardToolbarItem(slideShowTimer.Enabled, "Slideshow", SlideShowItemClick);
            slideShowItem.SvgImage = imageCollection["Slideshow"];
            e.Items.Insert(0, slideShowItem);
        }
        void SlideShowTimerTick(object sender, System.EventArgs e) {
            transitionManager.StartTransition(dashboardViewer);
            try {
                ShowNextPrevTab(NextPrevValue.Next);
            } finally {
                transitionManager.EndTransition();
            }
        }
        void ShowNextPrevTab(NextPrevValue value) {
            TabContainerDashboardItem tabContainer = TabContainer;
            if(tabContainer != null) {
                int increment = value == NextPrevValue.Next ? 1 : -1;
                string tabContainerName = tabContainer.ComponentName;
                int selectedIndex = dashboardViewer.GetSelectedTabPageIndex(tabContainerName);
                int pageCount = tabContainer.TabPages.Count;
                dashboardViewer.SetSelectedTabPage(tabContainerName, (selectedIndex + pageCount + increment) % pageCount);
            }
        }
        void NextPrevTabItemClick(DashboardToolbarItemClickEventArgs args) {
            ShowNextPrevTab((NextPrevValue)args.Item.Tag);
        }
        void SlideShowItemClick(DashboardToolbarItemClickEventArgs args) {
            if(args.Item.Checked.HasValue && args.Item.Checked.Value)
                slideShowTimer.Start();
            else
                slideShowTimer.Stop();
        }
        void ShowHideTabHeadersItemClick(DashboardToolbarItemClickEventArgs args) {
            TabContainerDashboardItem tabContainer = TabContainer;
            if(tabContainer != null) {
                if(args.Item.Checked.HasValue && args.Item.Checked.Value) {
                    tabContainer.ShowCaption = true;
                } else {
                    tabContainer.ShowCaption = false;
                }
            }
        }
        void DashboardViewerSelectedTabPageChanged(object sender, SelectedTabPageChangedEventArgs e) {
            TabContainerDashboardItem tabContainer = TabContainer;
            if(tabContainer != null && !tabContainer.ShowCaption) {
                string selectedTabPageName = e.Page;
                dashboardViewer.Dashboard.Title.Text = TabContainer.TabPages[selectedTabPageName].Name;
            }
        }
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            if(disposing) {
                slideShowTimer.Tick -= SlideShowTimerTick;
                slideShowTimer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    enum NextPrevValue {
        Next,
        Prev
    }
}
