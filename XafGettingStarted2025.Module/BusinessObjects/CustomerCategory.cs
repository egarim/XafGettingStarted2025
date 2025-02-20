using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafGettingStarted2025.Module.BusinessObjects
{
    //xc
    [DefaultClassOptions]
    public class CustomerCategory : BaseObject
    {
        public CustomerCategory(Session session) : base(session)
        { }


        string description;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Description
        {
            get => description;
            set => SetPropertyValue(nameof(Description), ref description, value);
        }

        [Association("CustomerCategory-Customers")]
        public XPCollection<Customer> Customers
        {
            get
            {
                return GetCollection<Customer>(nameof(Customers));
            }
        }

    }

}
