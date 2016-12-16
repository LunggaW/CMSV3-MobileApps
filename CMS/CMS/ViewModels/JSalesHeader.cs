using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.ViewModels
{
    public class JSalesHeader
    {

        public enum SalesTypeEnum { Sales = 1, Return = 2 }
        public enum SalesStatusEnum { Created, Validated, Approved, Rejected, Deleted }

        public string nota { set; get; }
        public string site { set; get; }
        public string user { set; get; }
        public string barcode { set; get; }
        public DateTime date { set; get; }
        public Decimal totalamount { set; get; }
        public int qty { set; get; }
        public SalesTypeEnum SalesType { set; get; }
        public SalesStatusEnum SalesStatus { set; get; }


         public List<SiteList> EnumToSiteList(Type EnumType)
         {
             List<SiteList> enumList = new List<SiteList>();
            SiteList List = new SiteList();

            Array Values = System.Enum.GetValues(EnumType);

            foreach (int Value in Values)
            {
                string Display = Enum.GetName(EnumType, Value);
                //ListItem Item = new ListItem(Display, Value.ToString());
                List = new SiteList();
                List.id = Value.ToString();
                List.name = Display;

                enumList.Add(List);
            }
             return enumList;
         }
    }
}