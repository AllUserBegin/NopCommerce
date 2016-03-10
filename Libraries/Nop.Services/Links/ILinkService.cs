using Nop.Core.Domain.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Links
{
    /// <summary>
    ///     链接服务接口 
    ///     <para> 2015-08-29 17:17:00</para>
    /// </summary>
    public interface ILinkService
    {
        /// <summary>
        ///     删除链接.
        ///     <para> 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">要删除的链接对象</param>
        void DeleteLink(Link link);

        /// <summary>
        ///     通过ID获取链接.
        ///     <para> 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="linkId">链接对象的ID字符串</param>
        /// <returns>通过ID值获取到的链接.</returns>
        Link GetLinkById(int linkId);

        /// <summary>
        ///     获取所有链接.
        ///     <para> 2015-08-29 17:17:00</para>
        /// </summary>
        /// <returns>连接序列</returns>
        IList<Link> GetAllLinks();

        /// <summary>
        ///     插入链接.
        ///     <para> 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">欲插入数据库的链接对象</param>
        void InsertLink(Link link);

        /// <summary>
        ///     更新链接.
        ///     <para> 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">欲更新的链接对象</param>
        void UpdateLink(Link link);
    }

}
