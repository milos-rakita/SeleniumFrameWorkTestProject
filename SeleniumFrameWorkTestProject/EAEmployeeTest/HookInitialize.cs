using EAAutoFramework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAEmployeeTest
{
    public class HookInitialize : TestInitializeHook
    {
        public HookInitialize() : base(BrowsrType.Chrome)
        {
            InitialSettings();
            NavigateSite();
        }






    }
}
