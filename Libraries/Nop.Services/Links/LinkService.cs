using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Links;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Links
{
    public class LinkService : ILinkService
    {
        /// <summary>
        ///     获取或设置所有链接对象缓存时用到的键
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private const string LinksAllKey = "Nop.links.all";

        /// <summary>
        ///     获取或设置链接对象缓存时用到的键，其格式为：Nop.links.id-{0}，其中{0}替换为链接的Id
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private const string LinksByIdKey = "Nop.links.id-{0}";

        /// <summary>
        ///     清除缓存匹配Nop.links.模式的缓存
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private const string LinksPatternKey = "Nop.links.";

        /// <summary>
        ///     缓存管理器
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        ///     事件公布器
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        ///     链接仓库
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        private readonly IRepository<Link> _linkRepository;

        /// <summary>
        ///     初始化<see cref="LinkService" /> 类的一个新实例.
        ///     <para>刘安全 2015-08-29 17:44:27</para>
        /// </summary>
        /// <param name="linkRepository">链接仓库.</param>
        /// <param name="eventPublisher">事件发布者 The event publisher.</param>
        /// <param name="cacheManager">高速缓存管理器.</param>
        public LinkService(IRepository<Link> linkRepository, IEventPublisher eventPublisher, ICacheManager cacheManager)
        {
            this._linkRepository = linkRepository;
            this._eventPublisher = eventPublisher;
            this._cacheManager = cacheManager;
        }

        /// <summary>
        ///     删除链接.
        ///     <para>刘安全 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">要删除的链接对象</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public virtual void DeleteLink(Link link)
        {            
            // 如果参数为null则抛出参数为空的异常
            if (link == null)
                throw new ArgumentNullException(link.Name);

            // 如果链接对象不为null，则利用链接仓库把它从数据库中删除
            _linkRepository.Delete(link);

            /* 清除缓存中键匹配Nop.links.的缓存，即清除缓存中所有Link的缓存。之所以要清除匹配Nop.links.的缓存而不是
               只清除这一个对象的缓存，是因为缓存中可能不是只有当前Link对象的缓存，还可能存在包含当前对象的链接列表。
               当对象已删除时，若链接列表未从缓存中删除，则会因列表中包含已删除对象，而导致数据不准确 */
            _cacheManager.RemoveByPattern(LinksPatternKey);

            _eventPublisher.EntityDeleted(link);
        }

        /// <summary>
        ///     通过ID获取链接.
        ///     <para>刘安全 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="linkId">链接对象的ID字符串</param>
        /// <returns>通过ID值获取到的链接.</returns>
        public virtual Link GetLinkById(int linkId)
        {
            if (linkId == 0)
                return null;

            // 生成链接对象统一格式的缓存键。键的格式为：Nop.links.id-{0}
            var key = string.Format(LinksByIdKey, linkId);
            // 从缓存中获取key关联的链接对象。若不存在，则从链接仓库中获取ID为给定linkId值的链接对象(此对象将同时由_cacheManager.Getw做缓存处理)。
            return _cacheManager.Get(key, () => _linkRepository.GetById(linkId));
        }

        /// <summary>
        ///     获取所有链接.
        ///     <para>刘安全 2015-08-29 17:17:00</para>
        /// </summary>
        /// <returns>连接序列</returns>
        public virtual IList<Link> GetAllLinks()
        {
            var key = string.Format(LinksAllKey);
            /* 从缓存中获取键为Nop.links.all的缓存对象（链接对象列表）。若缓存对象不存在，则从链接仓库中获取所有链接
               并返回链接对象列表(此对象列表将同时由_cacheManager.Getw做缓存处理)。*/
            return _cacheManager.Get(key, () =>
            {
                var query = _linkRepository.Table;

                return query.ToList();
            });
        }

        /// <summary>
        ///     插入链接.
        ///     <para>刘安全 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">欲插入数据库的链接对象</param>
        /// <exception cref="System.ArgumentNullException">link</exception>
        public virtual void InsertLink(Link link)
        {          
            
            if (link == null)
                throw new ArgumentNullException(link.Name);

            // 将链接对象添加到数据库中
            _linkRepository.Insert(link);

            /* 清除缓存中键匹配Nop.links.的缓存，即清除缓存中所有Link的缓存。之所以要清除匹配Nop.links.的缓存而不是
               只增加这一个对象的缓存，是因为缓存中可能存在包含当前对象的链接列表。当增加一个Link对象时，若链接列表
               未从缓存中删除，则会因列表中不包含新增的Link对象，而导致数据不准确 */
            _cacheManager.RemoveByPattern(LinksPatternKey);

            _eventPublisher.EntityInserted(link);
        }

        /// <summary>
        ///     更新链接.
        ///     <para>刘安全 2015-08-29 17:17:00</para>
        /// </summary>
        /// <param name="link">The link.</param>
        /// <exception cref="System.ArgumentNullException">link</exception>
        public virtual void UpdateLink(Link link)
        {
            if (link == null)
                throw new ArgumentNullException(link.Name);

            _linkRepository.Update(link);

            /* 清除缓存中键匹配Nop.links.的缓存，即清除缓存中所有Link的缓存。之所以要清除匹配Nop.links.的缓存而不是
               只清除这一个对象的缓存，是因为缓存中可能存在包含当前对象的链接列表。当Link对象的信息已改变时，若链接
               列表未从缓存中删除，则会因列表中当前Link对象的信息未改变，而导致数据不准确 */
            _cacheManager.RemoveByPattern(LinksPatternKey);

            _eventPublisher.EntityUpdated(link);
        }


    }
}
