﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// The abstract base class for all border-width sub properties.
    /// </summary>
    abstract class CSSBorderPartWidthProperty : CSSProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Medium;
        internal static readonly IValueConverter<Length> Converter = WithBorderWidth();
        Length _width;

        #endregion

        #region ctor

        internal CSSBorderPartWidthProperty(String name, CSSStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the thickness of the given border.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        public void SetWidth(Length width)
        {
            _width = width;
            _value = new CSSPrimitiveValue(Serialize(width));
        }

        internal override void Reset()
        {
            _width = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, SetWidth);
        }

        #endregion

        #region Helpers

        static ICssObject Serialize(Length width)
        {
            if (width == Length.Thin)
                return new CssIdentifier(Keywords.Thin);
            else if (width == Length.Medium)
                return new CssIdentifier(Keywords.Medium);
            else if (width == Length.Thick)
                return new CssIdentifier(Keywords.Thick);

            return width;
        }

        #endregion
    }
}
