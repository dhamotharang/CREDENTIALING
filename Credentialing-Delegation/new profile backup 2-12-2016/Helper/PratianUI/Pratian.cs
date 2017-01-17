using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Routing;

namespace PratianUI
{
    static public class Pratian
    {
        private static Random rng = new Random();
        private enum InputControlType
        {
            CheckBox = 1,
            RadioButton = 2,
            SquareRadioButton = 3
        };
        private enum DateTimeFormat
        {
            Date = 1,
            Time = 2,
            DateTime = 3
        };
        public enum ValidationSwitch
        {
            On = 1,
            Off = 2
        };

        #region INPUT-CONTROL HELPERS
        // COMMON IMPLEMENTATION:
        private static MvcHtmlString InputControlHelper(HtmlHelper htmlHelper, InputControlType inputType, string labelText, IDictionary<string, object> htmlAttributes)
        {
            try
            {
                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

                // SET INPUT CONTROL TYPE:
                string prefix = null, type = null, inputCssClass = null;
                switch (inputType)
                {
                    case InputControlType.CheckBox: prefix = "checkBox"; inputCssClass = "normal-checkbox"; type = "checkbox";
                        break;
                    case InputControlType.RadioButton: prefix = "radioButton"; inputCssClass = "normal-radio"; type = "radio";
                        break;
                    case InputControlType.SquareRadioButton: prefix = "radioButton"; inputCssClass = "checkbox-radio"; type = "radio";
                        break;
                    default:
                        break;
                }
                var pratUiId = prefix + rng.Next(0, 1000000);

                // WRAPPER ELEMENT:
                TagBuilder div = new TagBuilder("div");
                div.Attributes.Add("style", "position: relative; white-space: nowrap; display:inline;");

                // CHECKBOX ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", type);

                // HTML ATTRIBUTES BY USER:
                input.MergeAttributes(attributes);

                // APPENDING PLUGIN CSS CLASS:
                input.AddCssClass(inputCssClass);

                // SETTING RANDOM ID IF NOT SPECIFIED BY USER:
                if (!input.Attributes.ContainsKey("id"))
                {
                    input.Attributes.Add("id", pratUiId);
                }

                // LABEL ELEMENT:
                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", input.Attributes["id"]);
                if (input.Attributes.ContainsKey("labelClass"))
                {
                    label.Attributes.Add("class", input.Attributes["labelClass"]);
                    input.Attributes.Remove("labelClass");
                }

                // SPAN ELEMENT:
                TagBuilder span = new TagBuilder("span");
                label.InnerHtml += span;
                if (labelText != null)
                {
                    label.InnerHtml += labelText;
                }

                // BUILD COMPLETE HTML STRING:
                div.InnerHtml += (input.ToString(TagRenderMode.SelfClosing) + label.ToString(TagRenderMode.Normal));
                return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        private static MvcHtmlString InputControlHelperFor<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, InputControlType inputType, string labelText, IDictionary<string, object> htmlAttributes, ValidationSwitch validationSwitch = ValidationSwitch.On)
        {
            try
            {
                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var value = metadata.Model;

                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);
                RouteValueDictionary validationAttributes = new RouteValueDictionary(htmlHelper.GetUnobtrusiveValidationAttributes(fullBindingName, metadata));

                // SET INPUT CONTROL TYPE:
                string type = null, inputCssClass = null;
                switch (inputType)
                {
                    case InputControlType.CheckBox: inputCssClass = "normal-checkbox"; type = "checkbox";
                        break;
                    case InputControlType.RadioButton: inputCssClass = "normal-radio"; type = "radio";
                        break;
                    case InputControlType.SquareRadioButton: inputCssClass = "checkbox-radio"; type = "radio";
                        break;
                    default:
                        break;
                }
                var pratUiId = fieldId + rng.Next(0, 1000000);

                // WRAPPER ELEMENT:
                TagBuilder div = new TagBuilder("div");
                div.Attributes.Add("style", "position: relative; white-space: nowrap; display:inline;");

                // CHECKBOX ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", type);

                // HTML ATTRIBUTES BY USER:
                input.MergeAttributes(attributes);

                // VALIDATION ATTRIBUTES OF MODEL:
                if (validationSwitch == ValidationSwitch.On)
                {
                    input.MergeAttributes(validationAttributes);
                }

                // APPENDING PLUGIN CSS CLASS:
                input.AddCssClass(inputCssClass);

                // SETTING RANDOM ID IF NOT SPECIFIED BY USER:
                if (!input.Attributes.ContainsKey("id"))
                {
                    input.Attributes.Add("id", pratUiId);
                }

                // SETTING NAME FROM THE MODEL:
                if (fullBindingName != null && !input.Attributes.ContainsKey("name"))
                {
                    input.Attributes.Add("name", fullBindingName);
                }

                // SETTING VALUE FROM THE MODEL:
                if (value != null && !input.Attributes.ContainsKey("value"))
                {
                    if (value.GetType() == typeof(bool) && (bool)value == true)
                    {
                        value = input.Attributes.ContainsKey("checked") ? true : false;
                    }
                    else if (value.GetType() == typeof(bool) && (bool)value == false)
                    {
                        value = input.Attributes.ContainsKey("checked") ? false : true;
                    }
                    input.Attributes.Add("value", value.ToString());
                }

                // LABEL ELEMENT:
                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", input.Attributes["id"]);
                if (input.Attributes.ContainsKey("labelClass"))
                {
                    label.Attributes.Add("class", input.Attributes["labelClass"]);
                    input.Attributes.Remove("labelClass");
                }

                // SPAN ELEMENT:
                TagBuilder span = new TagBuilder("span");
                label.InnerHtml += span;
                if (labelText != null)
                {
                    label.InnerHtml += labelText;
                }

                // BUILD COMPLETE HTML STRING:
                div.InnerHtml += (input.ToString(TagRenderMode.SelfClosing) + label.ToString(TagRenderMode.Normal));
                return new MvcHtmlString(div.ToString(TagRenderMode.Normal));
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }

        // HELPER METHODS:
        // A. WITHOUT MODEL: 
        /// <summary>
        /// <para>CheckBox Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiCheckbox(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.CheckBox, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>CheckBox Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiCheckbox(this HtmlHelper htmlHelper, string labelText, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.CheckBox, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>RadioButton Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiRadioButton(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.RadioButton, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>RadioButton Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiRadioButton(this HtmlHelper htmlHelper, string labelText, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.RadioButton, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>SquareRadioButton Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSquareRadioButton(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.SquareRadioButton, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>SquareRadioButton Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSquareRadioButton(this HtmlHelper htmlHelper, string labelText, object htmlAttributes = null)
        {
            return InputControlHelper(htmlHelper: htmlHelper, inputType: InputControlType.SquareRadioButton, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        
        // B. WITH MODEL:
        /// <summary>
        /// <para>CheckBox Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, ValidationSwitch validationSwitch = ValidationSwitch.On)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.CheckBox, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), validationSwitch: validationSwitch);
        }
        /// <summary>
        /// <para>CheckBox Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null, ValidationSwitch validationSwitch = ValidationSwitch.On)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.CheckBox, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), validationSwitch: validationSwitch);
        }
        /// <summary>
        /// <para>RadioButton Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.RadioButton, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>RadioButton Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.RadioButton, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>SquareRadioButton Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSquareRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.SquareRadioButton, labelText: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>SquareRadioButton Helper for a Model</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="labelText">Text to be displayed as label for the checkbox.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSquareRadioButtonFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string labelText, object htmlAttributes = null)
        {
            return InputControlHelperFor(htmlHelper: htmlHelper, expression: expression, inputType: InputControlType.SquareRadioButton, labelText: labelText, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        #endregion

        #region DATE-TIME PICKER HELPERS
        // COMMON IMPLEMENTATION:
        private static MvcHtmlString DateTimePickerHelper(HtmlHelper htmlHelper, DateTimeFormat dateTimeFormat, bool setMaxDateToCurrentDate, string position, IDictionary<string, object> htmlAttributes)
        {
            try
            {
                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

                // SET DATE-TIME PICKER FORMAT:
                string prefix = null, format = null, mask = null, sideBySide = null;
                switch (dateTimeFormat)
                {
                    case DateTimeFormat.Date: prefix = "datePicker"; format = "MM/DD/YYYY"; mask = "99/99/9999" ;
                        break;
                    case DateTimeFormat.Time: prefix = "timePicker"; format = "HH:mm:ss"; mask = "99:99:99";
                        break;
                    case DateTimeFormat.DateTime: prefix = "dateTimePicker"; format = "MM/DD/YYYY HH:mm:ss"; mask = "99/99/9999 99:99:99"; sideBySide = "true" ;
                        break;
                    default:
                        break;
                }
                var pratUiId = prefix + rng.Next(0, 1000000);

                // TEXT INPUT ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("pratUiId", pratUiId);

                // HTML ATTRIBUTES BY USER:
                input.MergeAttributes(attributes);

                // BUILD THE SCRIPT:
                StringBuilder scriptString = new StringBuilder();
                scriptString.Append("<script type='text/javascript'>" +
                                        "$(function(){" +
                                            "$('input[pratUiId=" + pratUiId + "]').datetimepicker({" +
                                                "format: '" + format + "', " +
                                                "useCurrent: false" +
                                                (setMaxDateToCurrentDate == true ? ", maxDate: new Date()" : "") +
                                                (position != null ? ", widgetPositioning: {horizontal: 'auto', vertical: '" + position + "'}" : "") +
                                                (sideBySide != null ? ", sideBySide: true" : "") +
                                            "});" +
                                            "$('input[pratUiId=" + pratUiId + "]').mask('" + mask + "');" +
                                        "});" +
                                    "</script>");

                // BUILD COMPLETE HTML STRING:
                StringBuilder combinedString = new StringBuilder();
                combinedString.Append(input.ToString(TagRenderMode.SelfClosing));
                combinedString.Append(scriptString.ToString());
                return new MvcHtmlString(combinedString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        private static MvcHtmlString DateTimePickerHelperFor<TModel, TValue>(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, DateTimeFormat dateTimeFormat, bool setMaxDateToCurrentDate, string position, IDictionary<string, object> htmlAttributes, ValidationSwitch validationSwitch = ValidationSwitch.On)
        {
            try
            {
                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                var value = metadata.Model;

                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);
                RouteValueDictionary validationAttributes = new RouteValueDictionary(htmlHelper.GetUnobtrusiveValidationAttributes(fullBindingName, metadata));

                // SET DATE-TIME PICKER FORMAT:
                string prefix = null, format = null, mask = null, sideBySide = null;
                switch (dateTimeFormat)
                {
                    case DateTimeFormat.Date: prefix = "datePicker"; format = "MM/DD/YYYY"; mask = "99/99/9999";
                        break;
                    case DateTimeFormat.Time: prefix = "timePicker"; format = "HH:mm:ss"; mask = "99:99:99";
                        break;
                    case DateTimeFormat.DateTime: prefix = "dateTimePicker"; format = "MM/DD/YYYY HH:mm:ss"; mask = "99/99/9999 99:99:99"; sideBySide = "true";
                        break;
                    default:
                        break;
                }
                var pratUiId = prefix + rng.Next(0, 1000000);

                // TEXT INPUT ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("pratUiId", pratUiId);

                // HTML ATTRIBUTES BY USER:
                input.MergeAttributes(attributes);
                
                // VALIDATION ATTRIBUTES OF MODEL:
                if (validationSwitch == ValidationSwitch.On)
                {
                    input.MergeAttributes(validationAttributes);
                }

                // SETTING RANDOM ID IF NOT SPECIFIED BY USER:
                if (!input.Attributes.ContainsKey("id"))
                {
                    input.Attributes.Add("id", pratUiId);
                }

                // SETTING NAME FROM THE MODEL:
                if (fullBindingName != null && !input.Attributes.ContainsKey("name"))
                {
                    input.Attributes.Add("name", fullBindingName);
                }

                // SETTING VALUE FROM THE MODEL:
                if (value != null && !input.Attributes.ContainsKey("value"))
                {
                    if (value.GetType() == typeof(DateTime))
                    {
                        DateTime datetime  = ((DateTime)value);
                        string Date = null;
                        switch (dateTimeFormat)
                        {
                            case DateTimeFormat.Date: Date =     datetime.Month + "/" +
                                                                 datetime.Day + "/" +
                                                                 datetime.Year;
                                                                 break;
                            case DateTimeFormat.Time: Date =     datetime.Hour + ":" +
                                                                 datetime.Minute + ":" +
                                                                 datetime.Second;
                                                                 break;
                            case DateTimeFormat.DateTime: Date = datetime.Month     + "/" +
                                                                 datetime.Day       + "/" +
                                                                 datetime.Year      + " " +
                                                                 datetime.Hour      + ":" +
                                                                 datetime.Minute    + ":" +
                                                                 datetime.Second;
                                                                 break;
                            default: break;
                        }
                        input.Attributes.Add("value", Date);
                    }
                    else
                    {
                        input.Attributes.Add("value", value.ToString());
                    }
                }     

                // BUILD THE SCRIPT:
                StringBuilder scriptString = new StringBuilder();
                scriptString.Append("<script type='text/javascript'>" +
                                        "$(function(){" +
                                            "$('input[pratUiId=" + pratUiId + "]').datetimepicker({" +
                                                "format: '" + format + "', " +
                                                "useCurrent: false" +
                                                (setMaxDateToCurrentDate == true ? ", maxDate: new Date()" : "") +
                                                (position != null ? ", widgetPositioning: {horizontal: 'auto', vertical: '" + position + "'}" : "") +
                                                (sideBySide != null ? ", sideBySide: true" : "") +
                                            "});" +
                                            "$('input[pratUiId=" + pratUiId + "]').mask('" + mask + "');" +
                                        "});" +
                                    "</script>");

                // BUILD COMPLETE HTML STRING:
                StringBuilder combinedString = new StringBuilder();
                combinedString.Append(input.ToString(TagRenderMode.SelfClosing));
                combinedString.Append(scriptString.ToString());
                return new MvcHtmlString(combinedString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }

        // HELPER METHODS:
        // A. WITHOUT MODEL:
        /// <summary>
        /// <para>DatePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePicker(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DatePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePicker(this HtmlHelper htmlHelper, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DatePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="setMaxDateToCurrentDate">Sets current date as maximum date for datepicker widget.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePicker(this HtmlHelper htmlHelper, bool setMaxDateToCurrentDate, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: setMaxDateToCurrentDate, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DatePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="setMaxDateToCurrentDate">Sets current date as maximum date for datepicker widget.</param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePicker(this HtmlHelper htmlHelper, bool setMaxDateToCurrentDate, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: setMaxDateToCurrentDate, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>TimePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiTimePicker(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Time, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>TimePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiTimePicker(this HtmlHelper htmlHelper, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.Time, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DateTimePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDateTimePicker(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.DateTime, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DateTimePicker Helper</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDateTimePicker(this HtmlHelper htmlHelper, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelper(htmlHelper: htmlHelper, dateTimeFormat: DateTimeFormat.DateTime, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        // B. WITH MODEL:
        /// <summary>
        /// <para>DatePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null, ValidationSwitch validationSwitch = ValidationSwitch.On)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes), validationSwitch: validationSwitch);
        }
        /// <summary>
        /// <para>DatePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DatePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="setMaxDateToCurrentDate">Sets current date as maximum date for datepicker widget.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool setMaxDateToCurrentDate, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: setMaxDateToCurrentDate, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DatePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="setMaxDateToCurrentDate">Sets current date as maximum date for datepicker widget.</param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, bool setMaxDateToCurrentDate, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Date, setMaxDateToCurrentDate: setMaxDateToCurrentDate, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>TimePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Time, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>TimePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.Time, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DateTimePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.DateTime, setMaxDateToCurrentDate: false, position: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DateTimePicker Helper for a Model.</para>
        /// <para>Dependency CSS: bootstrap-datetimepicker.css</para>
        /// <para>Dependency JS: moment.js, bootstrap-datetimepicker.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="position">Vertical position for the date picker widget, valid strings args: "top" or "bottom".</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string position, object htmlAttributes = null)
        {
            return DateTimePickerHelperFor(htmlHelper: htmlHelper, expression: expression, dateTimeFormat: DateTimeFormat.DateTime, setMaxDateToCurrentDate: false, position: position, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        #endregion

        #region DROPDOWN LIST HELPERS
        // COMMON IMPLEMENTATION:
        private static MvcHtmlString DropDownHelper(HtmlHelper htmlHelper, IEnumerable<object> list, List<string> selected, IDictionary<string, object> htmlAttributes)
        {
            try
            {
                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);
                var pratUiId = "dropDown" + rng.Next(0, 1000000);

                // SELECT ELEMENT:
                TagBuilder select = new TagBuilder("select");
                select.Attributes.Add("pratUiId", pratUiId);

                // HTML ATTRIBUTES BY USER:
                select.MergeAttributes(attributes);

                // EMPTY OPTION FOR PLACEHOLDER:
                select.InnerHtml += "<option></option>";

                // IF THE OBJECT IF OF TYPE JOBJECT:
                if (list.GetType() == typeof(JObject))
                {
                    Dictionary<object, object[]> newList = ((JObject)list).ToObject<Dictionary<object, object[]>>();
                    foreach (KeyValuePair<object, object[]> obj in newList)
                    {
                        foreach (JObject item in obj.Value)
                        {
                            Dictionary<string, string> d = item.ToObject<Dictionary<string, string>>();
                            if (selected != null)
                            {
                                select.InnerHtml += "<option value='" + (selected.Contains(d.FirstOrDefault(x => x.Key == "code").Value) ? d.FirstOrDefault(x => x.Key == "code").Value.ToString() + "' " + "selected='selected'" : (d.FirstOrDefault(x => x.Key == "code").Value).ToString() + "'") + ">" + d.FirstOrDefault(x => x.Key == "name").Value + "</option>";
                            }
                            else
                            {
                                select.InnerHtml += "<option value='" + d.FirstOrDefault(x => x.Key == "code").Value + "'>" + d.FirstOrDefault(x => x.Key == "name").Value + "</option>";
                            }
                            
                        }
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (selected != null && selected.Contains(item))
                        {
                            select.InnerHtml += "<option value='" + item.ToString() + "' selected='selected'>" + item.ToString() + "</option>";
                        }
                        else
                        {
                            select.InnerHtml += "<option value='" + item.ToString() + "'>" + item.ToString() + "</option>";
                        }
                    }
                }

                StringBuilder scriptString = new StringBuilder();
                scriptString.Append(select.ToString(TagRenderMode.Normal));
                scriptString.Append("<script type='text/javascript'>" +
                                        "$(function(){" +
                                            "$('select[pratUiId=" + pratUiId + "]').select2({" +
                                                    "width: '100%' ," +
                                                    "placeholder: '" + (select.Attributes.ContainsKey("placeholder") ? select.Attributes["placeholder"] : "Select") + "'" +
                                            "});" +
                                        "});" +
                                    "</script>");
                return new MvcHtmlString(scriptString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }

        // HELPER METHODS:
        // A. WITHOUT MODEL:
        /// <summary>
        /// <para>DropDownList Helper</para>
        /// <para>Dependency CSS: select2.css</para>
        /// <para>Dependency JS: select2.full.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list">List of strings which needs to be displayed as options for the dropdown list.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownList(this HtmlHelper htmlHelper, IEnumerable<object> list, object htmlAttributes = null)
        {
            return DropDownHelper(htmlHelper: htmlHelper, list: list, selected: null, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DropDownList Helper</para>
        /// <para>Dependency CSS: select2.css</para>
        /// <para>Dependency JS: select2.full.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list">List of strings which needs to be displayed as options for the dropdown list.</param>
        /// <param name="selected">A string of text which needs to be displayed already as selected option for the dropdown list.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownList(this HtmlHelper htmlHelper, IEnumerable<object> list, string selected, object htmlAttributes = null)
        {
            return DropDownHelper(htmlHelper: htmlHelper, list: list, selected: new List<string> { selected }, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DropDownList Helper</para>
        /// <para>Dependency CSS: select2.css</para>
        /// <para>Dependency JS: select2.full.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list">List of strings which needs to be displayed as options for the dropdown list.</param>
        /// <param name="selected">Array of strings text which needs to be displayed already as selected options for the dropdown list.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownList(this HtmlHelper htmlHelper, IEnumerable<object> list, string[] selected, object htmlAttributes = null)
        {
            return DropDownHelper(htmlHelper: htmlHelper, list: list, selected: new List<string>(selected), htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
        /// <summary>
        /// <para>DropDownList Helper</para>
        /// <para>Dependency CSS: select2.css</para>
        /// <para>Dependency JS: select2.full.js</para>
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="list">List of strings which needs to be displayed as options for the dropdown list.</param>
        /// <param name="selected">List of strings of text which needs to be displayed already as selected options for the dropdown list.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownList(this HtmlHelper htmlHelper, IEnumerable<object> list, List<string> selected, object htmlAttributes = null)
        {
            return DropDownHelper(htmlHelper: htmlHelper, list: list, selected: selected, htmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        // B. WITH MODEL:
        /// <summary>
        /// <para>DropDownList Helper for a Model</para>
        /// <para>Dependency CSS: select2.css</para>
        /// <para>Dependency JS: select2.full.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="url">Controller method from where filtered JSON will be fetched.</param>
        /// <param name="minInput">Minimum number of character(s) after which a search query will be fired.</param>
        /// <param name="width">Width of dropdown field, can be in '%' or 'px'.</param>
        /// <param name="placeholder">Placeholder for the dropdown field.</param>
        /// <param name="delay">Time in milliseconds after which a search query will be fired, after minimum characters of input.</param>
        /// <param name="id">Exact key for id to be used as the options' id.</param>
        /// <param name="name">Exact key for text to be used as the options' display text.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string url, int minInput, string width, string placeholder, string delay, string id, string name, object htmlAttributes = null)
        {
            try
            {
                RouteValueDictionary attributes = new RouteValueDictionary(htmlAttributes);

                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var pratUiId = fieldId + rng.Next(0, 1000000);
                var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = metadata.Model;

                // BUILDING SELECT ELEMENT:
                TagBuilder tag = new TagBuilder("select");
                tag.Attributes.Add("name", fullBindingName);
                tag.Attributes.Add("pratUiId", pratUiId);

                // ATTRIBUTES BY USER:
                tag.MergeAttributes(attributes);

                // ADDING ID FROM MODEL:
                if (!tag.Attributes.ContainsKey("id"))
                {
                    tag.Attributes.Add("id", fieldId);
                }

                // VALIDATION ATTRIBUTES OF MODEL:
                var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
                if (validationAttributes != null && validationAttributes.Count != 0)
                {
                    foreach (var item in validationAttributes)
                    {
                        tag.Attributes.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }

                // BUILDING OPTION ELEMENT:
                if (value != null)
                {
                    TagBuilder option = new TagBuilder("option");
                    option.Attributes.Add("", value.ToString());
                    option.InnerHtml += value.ToString();
                    tag.InnerHtml += option;
                }

                // APPENDING SCRIPT STRING:
                StringBuilder scriptString = new StringBuilder();
                scriptString.Append(tag.ToString(TagRenderMode.Normal));
                scriptString.Append("<script type='text/javascript'>");
                scriptString.Append("$(function(){" +
                                        "$.fn.select2.amd.require(['select2/utils', 'select2/dropdown', 'select2/dropdown/attachContainer', 'select2/dropdown/search'], function(Utils, DropdownAdapter, AttachContainer, DropdownSearch) {" +
                                            "var CustomAdapter = Utils.Decorate(Utils.Decorate(DropdownAdapter, DropdownSearch), AttachContainer);" +
                                            "$('[pratUiId=" + pratUiId + "]').select2({" +
                                               "ajax: {" +
                                                   "url: '" + url + "'," +
                                                   "dataType: 'json'," +
                                                   "type: 'POST'," +
                                                   "delay: " + delay + "," +
                                                   "data: function (params) {" +
                                                       "return {" +
                                                           "searchTerm: params.term," +
                                                       "};" +
                                                   "}," +
                                                   "processResults: function (data, params) {" +
                                                       "var array = JSON.parse(data);" +
                                                       "var i = 0;" +
                                                       "while(i < array.length){" +
                                                           "array[i]['id'] = array[i]['" + id + "'];" +
                                                           "array[i]['text'] = array[i]['" + name + "'];" +
                                                           "delete array[i]['" + id + "'];" +
                                                           "delete array[i]['" + name + "'];" +
                                                           "i++;" +
                                                           "}" +
                                                       "return { results: array };" +
                                                   "}," +
                                                   "cache: true" +
                                               "}," +
                                               "dropdownAdapter: CustomAdapter," +
                                               "dropdownParent: $('[pratUiId=" + pratUiId + "]')," +
                                               "escapeMarkup: function (markup) { return markup; }," +
                                               "minimumInputLength: '" + minInput + "'," +
                                               "width: '" + width + "'," +
                                               "placeholder: { id:'-1', text:'" + placeholder + "'}," +
                                               "templateResult: function (option) { return option.text; }," +
                                               "templateSelection: function (option) { return option.text; }" +
                                            "}).trigger('change');});" +
                                            "$('[pratUiId=" + pratUiId + "]').on('select2:select', function () { $('[pratUiId=" + pratUiId + "]').select2('close'); });" +
                                    "});");
                scriptString.Append("</script>");

                return new MvcHtmlString(scriptString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion
        
        //PHONE NUMBER:
        #region PhoneFormat
        /// <summary>
        /// <para>Phone Format Helper</para>
        /// <para>Dependency JS: jquery.maskedinput.js</para>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiPhoneFormat(this HtmlHelper html, object htmlAttributes = null)
        {
            try
            {
                var pratUiId = "phoneFormat" + rng.Next(0, 1000000);

                // BUILDING ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("pratUiId", pratUiId);

                // ATTRIBUTES BY USER:
                if (htmlAttributes != null)
                {
                    foreach (var item in htmlAttributes.GetType().GetProperties())
                    {
                        input.Attributes.Add(item.Name.ToString().Replace("@_", "@-"), item.GetValue(htmlAttributes, null).ToString());
                    }
                }

                // APPENDING SCRIPT STRING:
                StringBuilder script = new StringBuilder();
                script.Append(input.ToString(TagRenderMode.Normal));
                script.Append("<script>");
                script.Append("$(function(){" +
                                " $('input[pratUiId=" + pratUiId + "]').mask('(999) 999-9999');" +
                              "});");
                script.Append("</script>");
                return new MvcHtmlString(script.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion
        #region PhoneFormatFor
        /// <summary>
        /// <para>Phone Format Helper for a model</para>
        /// <para>Dependency JS: jquery.maskedinput.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiPhoneFormatFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            try
            {
                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = metadata.Model;

                var pratUiId = fieldId + rng.Next(0, 1000000);

                // BUILDING ELEMENT:
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "text");
                input.Attributes.Add("pratUiId", pratUiId);
                input.Attributes.Add("name", fullBindingName);

                // ATTRIBUTES BY USER:
                if (htmlAttributes != null)
                {
                    foreach (var item in htmlAttributes.GetType().GetProperties())
                    {
                        input.Attributes.Add(item.Name.ToString().Replace("@_", "@-"), item.GetValue(htmlAttributes, null).ToString());
                    }
                }

                // ADDING ID FROM MODEL:
                if (!input.Attributes.ContainsKey("id"))
                {
                    input.Attributes.Add("id", fieldId);
                }

                // ADD "VALUE" ATTRIBUTE FROM MODEL:
                if (!input.Attributes.ContainsKey("value"))
                {
                    if (value != null)
                    {
                        input.Attributes.Add("value", value.ToString());
                    }
                }

                // VALIDATION ATTRIBUTES OF MODEL:
                var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
                if (validationAttributes != null && validationAttributes.Count != 0)
                {
                    foreach (var item in validationAttributes)
                    {
                        input.Attributes.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }

                // APPENDING SCRIPT STRING:
                StringBuilder script = new StringBuilder();
                script.Append(input.ToString(TagRenderMode.Normal));
                script.Append("<script>");
                script.Append("$(function(){" +
                                " $('input[pratUiId=" + pratUiId + "]').mask('(999) 999-9999');" +
                              "});");
                script.Append("</script>");
                return new MvcHtmlString(script.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion

        // OTHERS:
        #region DualListFor
        /// <summary>
        /// <para>DualList Helper for a model</para>
        /// <para>Dependency CSS:  bootstrap-duallistbox.css</para>
        /// <para>Dependency JS: jquery.bootstrap-duallistbox.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="NonSelected"></param>
        /// <param name="Selected"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDualListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string url, string id, string name, string NonSelected, string Selected, object htmlAttributes = null)
        {
            try
            {
                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var pratUiId = fieldId + rng.Next(0, 1000000);

                var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = metadata.Model;

                TagBuilder tag = new TagBuilder("select");
                tag.Attributes.Add("name", fullBindingName);

                if (htmlAttributes != null)
                {
                    foreach (var item in htmlAttributes.GetType().GetProperties())
                    {
                        tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                    }

                }

                var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
                if (validationAttributes != null && validationAttributes.Count != 0)
                {
                    foreach (var item in validationAttributes)
                    {
                        tag.Attributes.Add(item.Key.ToString(), item.Value.ToString());
                    }
                }

                StringBuilder scriptString = new StringBuilder();
                scriptString.Append(tag.ToString(TagRenderMode.Normal));
                scriptString.Append("<script type='text/javascript'>");
                scriptString.Append("$(function () {" +
                              "var demo2 = $('.DualList').bootstrapDualListbox({" +
                                "nonSelectedListLabel: '" + NonSelected + "'," +
                                "selectedListLabel: '" + Selected + "'," +
                                "preserveSelectionOnMove: 'moved'," +
                                 "moveOnSelect: true" +

                                  "});" +
                                  "var flag=0;" +
                                 "function GetData(data)" +
                                 "{" +
                                  "var name = [];" +
                                    "var j = 0;" +
                                   "while(j<data.length)" +
                                   "{" +
                                     "data[j]['id']=data[j]['" + id + "'];" +
                                     "data[j]['text']=data[j]['" + name + "'];" +
                                     "delete data[j]['" + id + "'];" +
                                     "delete data[j]['" + name + "'];" +
                                      "j++;" +
                                    "}" +
                                   "for(var i=0;i<data.length;i++)" +
                                    "{" +
                                       "name.push(data[i].text);" +
                                    "}" +
                                    "var length=name.length+1;" +
                                    "if(name.length>0 && name.length<length)" +
                                      "{" +
                                       "for (var i = 0; i < name.length; i++)" +
                                        "{" +
                                          "demo2.append('<option value=' +name[i]+ '>' +name[i]+ '</option>');" +
                                        "}" +
                                       "}" +
                                    "return name;}" +
                             "$('.DualList').on('load',function () {" +
                                  "$('#SelectItem').empty();" +
                                  "flag=1;" +
                                 "demo2.trigger('bootstrapduallistbox.refresh',true);" +
                                  "var data = [];" +

                                      "$.ajax({" +
                                      "url: '" + url + "'," +
                                      "type: 'GET'," +
                                      "success:function(result)" +
                                      "{" +
                                        "data=GetData(JSON.parse(result));" +

                                        "$('.removeall').click();" +
                                      "}" +
                                     "});" +

                                      "demo2.trigger('bootstrapduallistbox.refresh',true);" +
                                 "});" +
                                 "if(flag==0){" +
                                "$('.DualList').trigger('load');" +
                                "}" +
                         "});");
                scriptString.Append("</script>");
                return new MvcHtmlString(scriptString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion

        #region Search Input Dropdown List
        /// <summary>
        /// <para>DropDownList Helper with search input for a model</para>
        /// <para>Dependency CSS: jquery.ui.css</para>
        /// <para>Dependency JS: jquery.ui.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUISearchInputDropDown<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string url, string id, string name, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fullBindingID = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            // BUILDING ELEMENT:
            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("name", fullBindingName);
            input.Attributes.Add("type", "text");
            input.Attributes.Add("id", fullBindingID);

            // ATTRIBUTES BY USER:
            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    input.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }

            // ADDING ID FROM MODEL:
            if (!input.Attributes.ContainsKey("id"))
            {
                input.Attributes.Add("id", fieldId);
            }

            // VALIDATION ATTRIBUTES OF MODEL:
            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            if (validationAttributes != null && validationAttributes.Count != 0)
            {
                foreach (var item in validationAttributes)
                {
                    input.Attributes.Add(item.Key.ToString(), item.Value.ToString());
                }
            }

            // SETTING VALUE:
            if (value != null)
            {
                input.Attributes.Add("value", value.ToString());
            }

            StringBuilder script = new StringBuilder();
            script.Append(input.ToString(TagRenderMode.SelfClosing));
            script.Append(" <script type='text/javascript'>");

            script.Append(" $(function() {" +
                //"function log( message ) {"+
                //"alert(message);"+
                // "$( '<div>' ).text( message ).prependTo( '#log' );"+
                //   "$( '#log' ).scrollTop( 0 );"+
                //   "}"+
               "function GetName(data)" +
                "{" +
                   "var name = [];" +
                   "var j = 0;" +
                 "while(j<data.length)" +
                 "{" +
                   "data[j]['id']=data[j]['" + id + "'];" +
                   "data[j]['text']=data[j]['" + name + "'];" +
                   "delete data[j]['" + id + "'];" +
                   "delete data[j]['" + name + "'];" +
                    "j++;" +
                  "}" +
                  "for(var i=0;i<data.length;i++)" +
                    "{" +
                    "name.push(data[i].text);" +
                    "}" +
                    "return name;}" +
              "var result=[];" +
               "$( '#" + fullBindingID + "' ).autocomplete({" +
               "source: function( request, response ) {" +
                " $.ajax({" +
                      "url: '" + url + "'," +
                      "type:'POST'," +
                      "data: {" +
                         "searchTerm: request.term" +
                           "}," +
                    "success: function( data ) {" +
                      "response(GetName(JSON.parse(data)));" +
                      "result=(JSON.parse(data));" +
                      "console.log(result);" +
                   "}," +
            "minLength: 3," +
        "});" +
        "}" +
     "});" +
     "});");
            script.Append(" </script>");

            return new MvcHtmlString(script.ToString());
        }
        #endregion

        #region DropDownWithCheckboxFor
        /// <summary>
        /// <para>DropDownWithCheckbox Helper for a model</para>
        /// <para>Dependency Js: jquery.multiselect.js</para>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiDropDownWithCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string url, string id, string name, object htmlAttributes = null)
        {
            try
            {
                var fieldName = ExpressionHelper.GetExpressionText(expression);
                var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
                var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
                var pratUiId = fieldId + rng.Next(1, 1000);
                var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
                var value = metadata.Model;

                // BUILDING ELEMENT:
                TagBuilder select = new TagBuilder("select");
                select.Attributes.Add("name", fullBindingName);
                select.Attributes.Add("pratUiId", pratUiId);
                select.Attributes.Add("multiple", "multiple");

                // VALIDATION ATTRIBUTES OF MODEL:
                if (htmlAttributes != null)
                {
                    foreach (var item in htmlAttributes.GetType().GetProperties())
                    {
                        select.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                    }
                }

                // ADDING ID FROM MODEL:
                if (!select.Attributes.ContainsKey("id"))
                {
                    select.Attributes.Add("id", fieldId);
                }

                // APPENDING SCRIPT STRING:
                StringBuilder script = new StringBuilder();
                script.Append(select.ToString(TagRenderMode.Normal));
                script.Append("<script type='text/javascript'>");
                script.Append("$(function(){" +
                            "function GetData(data)" +
                                 "{" +
                                  "var name = [];" +
                                    "var j = 0;" +
                                   "while(j<data.length)" +
                                   "{" +
                                     "data[j]['id']=data[j]['" + id + "'];" +
                                     "data[j]['text']=data[j]['" + name + "'];" +
                                     "delete data[j]['" + id + "'];" +
                                     "delete data[j]['" + name + "'];" +
                                      "j++;" +
                                    "}" +
                                   "for(var i=0;i<data.length;i++)" +
                                    "{" +
                                       "name.push(data[i].text);" +
                                    "}" +
                                    "var length=name.length+1;" +
                                    "if(name.length>0 && name.length<length)" +
                                      "{" +
                                       "for (var i = 0; i < name.length; i++)" +
                                        "{" +
                                          "$('[pratUiId=" + pratUiId + "]').append('<option value=' +name[i]+ '>' +name[i]+ '</option>');" +
                                        "}" +
                                         "$('[pratUiId=" + pratUiId + "]').multiselect({" +
                                    "selectAll:true," +
                                        "column:1," +
                                         "search: true" +
                                             "});" +
                                       "}" +
                                    "return name;}" +
                           "$('[pratUiId=" + pratUiId + "]').on('load',function(){" +

                                   "$.ajax({" +
                                      "url: '" + url + "'," +
                                      "type: 'GET'," +
                                      "success:function(result)" +
                                      "{" +
                                        "data=GetData(JSON.parse(result));" +
                                      "}," +
                                     "});" +

                              "});" +
                              "$('[pratUiId=" + pratUiId + "]').trigger('load');" +
                     "})");
                script.Append("</script>");
                return new MvcHtmlString(script.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion

        #region SelectAllCheckbox
        /// <summary>
        /// <para>SelectAllCheckbox Helper</para>
        /// <para>Dependency CSS: checkbox-radio.css</para>
        /// <para><c>Dependency JS: RadioCheckBox-Plugin.js</c></para>
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id">Id for the parent element enclosing all the checkboxes, along with the one created by this helper.</param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSelectAllCheckbox(this HtmlHelper html, string id, object htmlAttributes = null)
        {
            try
            {
                var pratUiId = rng.Next(0, 1000000).ToString();
                TagBuilder div = new TagBuilder("div");
                div.Attributes.Add("style", "position: relative; white-space: nowrap; display:inline;");

                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "checkbox");

                if (htmlAttributes != null)
                {
                    foreach (var item in htmlAttributes.GetType().GetProperties())
                    {
                        input.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                    }
                }

                input.AddCssClass("normal-checkbox");

                if (!input.Attributes.ContainsKey("id"))
                {
                    input.Attributes.Add("id", pratUiId);
                }

                TagBuilder label = new TagBuilder("label");
                label.Attributes.Add("for", input.Attributes["id"]);

                TagBuilder span = new TagBuilder("span");
                label.InnerHtml += span;

                div.InnerHtml += (input.ToString(TagRenderMode.SelfClosing) + label.ToString(TagRenderMode.Normal));

                StringBuilder scriptString = new StringBuilder();
                scriptString.Append(div.ToString(TagRenderMode.Normal));
                scriptString.Append("<script>$(function(){" +
                                                "$('#" + input.Attributes["id"] + "').on('change', function(){" +
                                                    "$('#" + id + " input[type=checkbox]:not(#" + input.Attributes["id"] + ")').prop('checked', $(this).prop('checked'));" +
                                                "});" +
                                                "$('#" + id + " input[type=checkbox]').on('change', function(){" +
                                                    "if($('#" + id + " input[type=checkbox]:checked:not(#" + input.Attributes["id"] + ")').length == $('#" + id + " input[type=checkbox]:not(#" + input.Attributes["id"] + ")').length){$('#" + input.Attributes["id"] + "').prop('checked', true);}" +
                                                    "else{$('#" + input.Attributes["id"] + "').prop('checked', false);}" +
                                                "});" +
                                             "});" +
                                    "</script>");
                return new MvcHtmlString(scriptString.ToString());
            }
            catch (Exception e)
            {
                return new MvcHtmlString("<script>console.error(\"" + e.Message.ToString() + "\");</script>");
            }
        }
        #endregion

        #region DISCARDED
        #region Checkbox - FLAT
        /// <summary>
        /// CheckboxFlat Helper for a model
        /// Dependency CSS: flat/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiFlatCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            // tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_flat" + color + "', radioClass: 'iradio_flat" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Checkbox - MINIMAL
        /// <summary>
        /// CheckboxMinimal Helper for a model
        /// Dependency CSS: minimal/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiMinimalCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_minimal" + color + "', radioClass: 'iradio_minimal" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Checkbox - SQUARE
        /// <summary>
        /// CheckboxSquare Helper for a model
        /// Dependency CSS: square/_all.css
        /// Dependency JS: icheck.js
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSquareCheckboxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "checkbox");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_square" + color + "', radioClass: 'iradio_square" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Radio - FLAT
        /// <summary>
        /// RadioFlat Helper for a model
        /// Dependency CSS: flat/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiFlatRadioFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            // tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "radio");
            //tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_flat" + color + "', radioClass: 'iradio_flat" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Radio - MINIMAL
        /// <summary>
        /// RadioMinimal Helper for a model
        /// Dependency CSS: minimal/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiMinimalRadioFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            // tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "radio");
            tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_minimal" + color + "', radioClass: 'iradio_minimal" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Radio - SQUARE
        /// <summary>
        /// RadioSqaure Helper for a model
        /// Dependency CSS: square/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSqaureRadioFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes = null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);
            var pratUiId = TagBuilder.CreateSanitizedId(fullBindingName) + rng.Next(0, 1000000);

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var value = metadata.Model;

            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("name", fullBindingName);
            // tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("pratUiId", pratUiId);
            tag.Attributes.Add("type", "radio");
            //tag.Attributes.Add("value", value == null ? "" : value.ToString());

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }

            }
            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }

            var validationAttributes = html.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);

            StringBuilder scriptString = new StringBuilder();

            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("$(function(){$('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_square" + color + "', radioClass: 'iradio_square" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");

            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion

        #region Radio - SQUARE without model
        /// <summary>
        /// RadioSqaure Helper
        /// Dependency CSS: square/_all.css
        /// Dependency JS: icheck.js  
        /// </summary>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString PratianUiSqaureRadio(this HtmlHelper html, object htmlAttributes = null)
        {

            var pratUiId = rng.Next(0, 1000000).ToString();
            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("type", "radio");
            tag.Attributes.Add("pratUiId", pratUiId);

            if (htmlAttributes != null)
            {
                foreach (var item in htmlAttributes.GetType().GetProperties())
                {
                    tag.Attributes.Add(item.Name.ToString().Replace(@"_", @"-"), item.GetValue(htmlAttributes, null).ToString());
                }
            }

            var color = "";
            if (htmlAttributes.GetType().GetProperty("color") != null)
            {
                color = "-" + htmlAttributes.GetType().GetProperty("color").GetValue(htmlAttributes).ToString().Trim();
            }
            StringBuilder scriptString = new StringBuilder();
            scriptString.Append(tag.ToString(TagRenderMode.SelfClosing));
            scriptString.Append("<script type='text/javascript'>");
            scriptString.Append("jQuery(function($){ $('input[pratUiId=" + pratUiId + "]').iCheck({");
            scriptString.Append("checkboxClass: 'icheckbox_square" + color + "', radioClass: 'iradio_square" + color + "'");
            scriptString.Append("})});");
            scriptString.Append("</script>");
            return new MvcHtmlString(scriptString.ToString());
        }
        #endregion
        #endregion
    }
}