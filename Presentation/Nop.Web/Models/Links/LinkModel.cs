using FluentValidation.Attributes;
using Nop.Web.Framework.Mvc;
using Nop.Web.Validators.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nop.Web.Models.Links
{
    /// <summary>
    ///     链接模型
    ///     <para> 2015年09月01日 10:57:55</para>
    /// </summary>
    [Validator(typeof(LinkValidator))]
    public class LinkModel : BaseNopModel
    {
        /// <summary>
        ///     获取或设置链接的名字
        ///     <para>刘安全 2015-09-01 10:57:58</para>
        /// </summary>
        /// <value>The name.</value>
        [AllowHtml]
        public string Name { get; set; }

        /// <summary>
        ///     获取或设置链接的值
        ///     <para>刘安全 2015-09-01 10:57:58</para>
        /// </summary>
        /// <value>The value.</value>
        [AllowHtml]
        public string Value { get; set; }
    }

}