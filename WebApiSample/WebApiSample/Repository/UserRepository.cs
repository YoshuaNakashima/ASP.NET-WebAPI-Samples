using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiSample.Models;

namespace WebApiSample.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Users> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Users> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        void Add(Users users);
    }

    /// <summary>
    /// 
    /// </summary>
    public enum 分類
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public enum 仕様
    {
        /// <summary>
        /// 
        /// </summary>
        siyou1,

        /// <summary>
        /// 
        /// </summary>
        siyou2,

        /// <summary>
        /// 
        /// </summary>
        siyou3,
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : IDisposable, IUserRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Users> GetAll()
        {
            return db.Users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Users> GetById(int id)
        {
            return db.Users.FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public Task<Guid> UpdateSessionId(Guid sessionId)
        {
            var user = db.Users.Where(u => u.Id == 0).FirstOrDefault();
            if (user == null)
            {
                throw new Exception();
            }
            user.SessionId = sessionId;

            return new Task<Guid>(() => user.SessionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="users"></param>
        public void Add(Users users)
        {
            db.Users.Add(users);
            db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                }

                // TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
                // TODO: 大きなフィールドを null に設定します。

                disposedValue = true;
            }
        }

        // TODO: 上の Dispose(bool disposing) にアンマネージド リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
        // ~UserRepository() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }


        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
            // GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ancestorIds"></param>
        /// <returns></returns>
        public IQueryable<categories> GetSpecCategory(long[] ancestorIds)
        {
            IQueryable<categories> query = db.categories;
            query.Join(
                GetSpec(ancestorIds),
                c => c.id,
                i => i.descendant,
                (category, tree_path) => category
            );
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ancestorIds"></param>
        /// <returns></returns>
        public IQueryable<tree_paths> GetSpec(long[] ancestorIds)
        {
            IQueryable<tree_paths> query = db.tree_paths;
            foreach (long ancestorId in ancestorIds)
            {
                query = GetHierarchy(query, ancestorId);
            }
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        public IQueryable<tree_paths> GetHierarchy(IQueryable<tree_paths> paths, long ancestorId)
        {
            IGrouping<long, tree_paths> treeGroup = (new List<tree_paths>()).GroupBy(k => k.descendant).FirstOrDefault();
            return paths.Join(
                db.tree_paths.Where(p => p.ancestor == ancestorId),
                o => o.ancestor,
                i => i.descendant,
                (outer, inner) => outer
            );
        }
    }
}