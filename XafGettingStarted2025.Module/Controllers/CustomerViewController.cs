using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafGettingStarted2025.Module.BusinessObjects;

namespace XafGettingStarted2025.Module.Controllers
{
    //xcv
    public class CustomerViewController : ViewController
    {
        SimpleAction ChangeState;
        public CustomerViewController() : base()
        {
            // Target required Views (use the TargetXXX properties) and create their Actions.
            this.TargetObjectType = typeof(Customer);
            this.TargetViewType = ViewType.DetailView;


            //xas
            ChangeState = new SimpleAction(this, "Change State", "View");
            ChangeState.Execute += ChangeState_Execute;
            

        }
        private void ChangeState_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            Customer customerObject = ((Customer)this.View.CurrentObject);
            customerObject.Inactive = !customerObject.Inactive;

            View.ObjectSpace.CommitChanges();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            this.ChangeState.Enabled["Inactive"] = false;

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
