using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class EmptySortDecorator : ISortDecorator
    {
        public static readonly ISortDecorator Value = new EmptySortDecorator();

        public string Name { get; }
        public int Compare(object a, object b)
        {
            return 0;
        }
    }
}
