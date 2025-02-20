using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafGettingStarted2025.Module.Services;

namespace XafGettingStarted2025.Module.Controllers
{
    public class DiController : ViewController
    {
        SimpleAction ShowPlatformInfo;

        private readonly IServiceProvider serviceProvider;
        public DiController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            ShowPlatformInfo = new SimpleAction(this, "Show Platform Info", "View");
            ShowPlatformInfo.Execute += ShowPlatformInfo_Execute;


            //HACK Dependency Injection Documentation: https://docs.devexpress.com/eXpressAppFramework/404364/app-shell-and-base-infrastructure/dependency-injection-in-xaf-applications
            //HACK Dependency Injection Tickets: https://supportcenter.devexpress.com/Ticket/Details/T815184/use-dependency-injection-in-xaf-controllers#:~:text=XAF%20does%20not%20provide%20any%20built-in%20dependency%20injection,that%20configures%20controllers%20created%20by%20the%20base%20class.

            //HACK before it was part of the framework Manuel Grundner created a ticket to add it to the framework https://blog.delegate.at/2013/02/21/how-to-use-dependency-injection-in-xaf.html

        }
        // Implement this constructor to support dependency injection.
        [ActivatorUtilitiesConstructor]
        public DiController(IServiceProvider serviceProvider) : this()
        {
            this.serviceProvider = serviceProvider;
        }
        private void ShowPlatformInfo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
            IPlatformInfo platformInfo = serviceProvider.GetService<IPlatformInfo>();

            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            options.Message = platformInfo.GetPlatformName();
            options.Type = InformationType.Success;
            options.Web.Position = InformationPosition.Top;
            options.Win.Caption = "Platform Info";
            options.Win.Type = WinMessageType.Flyout;
            Application.ShowViewStrategy.ShowMessage(options);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
          

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
    }
}
