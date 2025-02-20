using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XafGettingStarted2025.Module.Services;
using static System.Net.Mime.MediaTypeNames;

namespace XafGettingStarted2025.Module.BusinessObjects
{

    //HACK XAF 23.static analysis
    //https://docs.devexpress.com/eXpressAppFramework/403389/debugging-testing-and-error-handling/code-diagnostics?v=23.2
    [DefaultClassOptions]
    
    //https://www.devexpress.com/subscriptions/new-2023-1.xml#xaf
    //Grid List Editor Enhancements
    //XAF Blazor now supports the following Application Model properties for the ListView node:

    //IModelListView.Filter
    //IModelListView.IsFooterVisible
    //IModelListViewShowFindPanel.ShowFindPanel
    //IModelListViewPreviewColumn.PreviewColumn
    public class DiInBo : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://docs.devexpress.com/eXpressAppFramework/113146/business-model-design-orm/business-model-design-with-xpo/base-persistent-classes).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public DiInBo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.PlatformName =
                Session.ServiceProvider.GetRequiredService<IPlatformInfo>().GetPlatformName();
            // Place your initialization code here (https://docs.devexpress.com/eXpressAppFramework/112834/getting-started/in-depth-tutorial-winforms-webforms/business-model-design/initialize-a-property-after-creating-an-object-xpo?v=22.1).
        }

        DateTime createdOn;
        string user;
        string platformName;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PlatformName
        {
            get => platformName;
            set => SetPropertyValue(nameof(PlatformName), ref platformName, value);
        }
        public void SetUserAsAdmin()
        {
            SetPropertyValueWithSecurityBypass<string>(nameof(User), "Admin");
        }
        [ModelDefault("AllowEdit", "False")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string User
        {
            get => user;
            set => SetPropertyValue(nameof(User), ref user, value);
        }

        [HideInUI(HideInUI.All)]
        public DateTime CreatedOn
        {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }

    }
}