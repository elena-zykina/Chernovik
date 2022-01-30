using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chernovik.DataBase
{
    class Transition
    {
        public static Frame MainFrame { get; set; }
        private static ChernovikEntities context;
        public static ChernovikEntities Context
        {
            get
            {
                if (context == null)
                    context = new ChernovikEntities();

                return context;
            }
        }
    }
}
