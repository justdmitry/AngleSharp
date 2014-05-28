﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule
    /// </summary>
    public sealed class CSSColumnRuleProperty : CSSProperty
    {
        #region Fields

        CSSColumnRuleWidthProperty _width;
        CSSColumnRuleStyleProperty _style;
        CSSColumnRuleColorProperty _color;

        #endregion

        #region ctor

        internal CSSColumnRuleProperty()
            : base(PropertyNames.ColumnRule)
        {
            _inherited = false;
            _style = new CSSColumnRuleStyleProperty();
            _width = new CSSColumnRuleWidthProperty();
            _color = new CSSColumnRuleColorProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the column-rule color.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
        }

        /// <summary>
        /// Gets the value of the column-rule style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style.Style; }
        }

        /// <summary>
        /// Gets the value of the column-rule width.
        /// </summary>
        public Length Width
        {
            get { return _width.Width; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var index = 0;
            var list = value as CSSValueList ?? new CSSValueList(value);
            var startGroup = new List<CSSProperty>(3);
            var width = new CSSColumnRuleWidthProperty();
            var style = new CSSColumnRuleStyleProperty();
            var color = new CSSColumnRuleColorProperty();
            startGroup.Add(width);
            startGroup.Add(style);
            startGroup.Add(color);

            while (true)
            {
                var length = startGroup.Count;

                for (int i = 0; i < length; i++)
                {
                    if (CheckSingleProperty(startGroup[i], index, list))
                    {
                        startGroup.RemoveAt(i);
                        index++;
                        break;
                    }
                }

                if (length == startGroup.Count)
                    break;
            }

            if (index == list.Length)
            {
                _width = width;
                _style = style;
                _color = color;
                return true;
            }

            return false;
        }

        #endregion
    }
}
