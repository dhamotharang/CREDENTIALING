using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.CustomHelpers
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SkipPasswordExpirationCheckAttribute : Attribute
    {
    }
}