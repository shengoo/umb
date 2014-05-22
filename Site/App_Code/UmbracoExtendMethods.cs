using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Site.App_Code
{
    public static class UmbracoExtendMethods
    {

        /// <summary>
        /// Return title of the content if exists, or return Name.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetTitleOrString(this IPublishedContent content)
        {
            return content.GetPropertyValue<string>("title") == "" ? content.Name : content.GetPropertyValue<string>("title");
        }


        public static string GetAliasOrUrl(this IPublishedContent content)
        {
            return content.GetPropertyValue<string>("umbracoUrlAlias") == "" ? content.Url : "/" + content.GetPropertyValue<string>("umbracoUrlAlias");
        }

    }
}