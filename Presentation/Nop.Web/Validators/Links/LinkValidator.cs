using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace Nop.Web.Validators.Links
{
  
        /// <summary>
        ///     链接验证器
        ///     <para> 2015年09月01日 11:42:48</para>
        /// </summary>
        public class LinkValidator : BaseNopValidator<LinkModel>
        {
            /// <summary>
            ///     初始化类链接验证器的实例.
            ///     <para>2015-09-02 16:45:55</para>
            /// </summary>
            /// <param name="localizationService">本地化服务</param>
            public LinkValidator(ILocalizationService localizationService)
            {
                // 当Name为空时，显示Link.Name所指定资源
                RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Link.Name"));
                RuleFor(x => x.Value).NotEmpty().WithMessage(localizationService.GetResource("Link.Value"));
            }
        }

    
}