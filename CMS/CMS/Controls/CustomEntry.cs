using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMS.Controls
{
    public class CustomEntry : Entry
    {
        public CustomEntry()
        {
            this.Keyboard = Keyboard.Numeric;
        }
    }
}
