using System;
using VSNewProjectDialogExample.Interfaces;

namespace VSNewProjectDialogExample.Implementation.Sorting
{
    public class BasePropertySortDecorator<ObjectType, PropertyType> : ISortDecorator
        where PropertyType : IComparable
        where ObjectType : class
    {
        protected readonly bool _ascending;
        private readonly Func<ObjectType, PropertyType> _getProperty;

        protected BasePropertySortDecorator(string name, bool ascending,
            Func<ObjectType, PropertyType> getProperty
            )
        {
            Name = name;
            _ascending = ascending;
            _getProperty = getProperty;
        }

        public string Name { get; }

        public int Compare(object a, object b)
        {
            var object1 = a as ObjectType;
            var object2 = b as ObjectType;
            if (object1 == null || object2 == null)
                return 0;

            var propertyValue1 = _getProperty(object1);
            var propertyValue2 = _getProperty(object2);
            if (propertyValue1 == null || propertyValue2 == null)
                return 0;

            return _ascending
                ? propertyValue1.CompareTo(propertyValue2)
                : propertyValue2.CompareTo(propertyValue1);
        }
    }
}