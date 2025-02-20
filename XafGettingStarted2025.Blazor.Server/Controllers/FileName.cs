using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Templates;

namespace XafGettingStarted2025.Blazor.Server.Controllers
{
    //HACK doc:https://www.devexpress.com/subscriptions/new-2024-1.xml#xaf-blazor-tabbed-mdi
    public class TabsController : WindowController
    {
        public TabsController() : base()
        {
            // Target required Windows (use the TargetXXX properties) and create their Actions.
            
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            Frame.TemplateChanged += Frame_TemplateChanged;
        }
        void Frame_TemplateChanged(object sender, EventArgs e)
        {
            if (Frame.Template is ITabbedMdiMainFormTemplate template)
            {
                template.TabsModel.RenderMode = DevExpress.Blazor.TabsRenderMode.AllTabs;
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
