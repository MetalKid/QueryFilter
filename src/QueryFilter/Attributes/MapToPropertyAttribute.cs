#region << Usings >>

using System;

#endregion

namespace QueryFilter.Attributes
{
    /// <summary>
    /// This attribute is used by being applied to properties on the properties of a filter class that
    /// specified which property it maps to on the corresponding entity object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class MapToPropertyAttribute : Attribute
    {
        
        #region << Properties >>

        /// <summary>
        /// Gets or sets the name of the property on map to/from.
        /// </summary>
        public string PropertyName { get; set; }

        #endregion

        #region << Constructors >>
        
        /// <summary>
        /// Default constuctor that assumes the property name is based on the property this attribute is
        /// associated with.
        /// </summary>
        /// <param name="propertyName">The name of the property to map to.</param>
        public MapToPropertyAttribute(string propertyName = null)
        {
            PropertyName = propertyName;
        }

        #endregion

    }
}
