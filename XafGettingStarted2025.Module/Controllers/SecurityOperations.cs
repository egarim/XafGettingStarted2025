using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafGettingStarted2025.Module.BusinessObjects;

namespace XafGettingStarted2025.Module.Controllers
{
    //https://www.devexpress.com/subscriptions/new-2024-1.xml#xaf-bypass-security-pefmissions-checks
    public class SecurityOperations : ViewController
    {
        SimpleAction action;
        public SecurityOperations() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            action = new SimpleAction(this, "Set Value Without Security", "View");
            action.Execute += action_Execute;
            
        }
        private void action_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            (this.View.CurrentObject as DiInBo).SetUserAsAdmin();
            this.View.ObjectSpace.CommitChanges();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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
