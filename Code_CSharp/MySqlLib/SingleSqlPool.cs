using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Alivever.com.MySqlLib
{
    /// <summary>
    /// 数据库链接线程池
    /// </summary>
    public class CSingleSqlPool
    {
        protected class CSingleSqlItem
        {
            public CSingleSqlExecuter DbConn;
            public string BorrowerId = null;
        }

        protected readonly List<CSingleSqlItem> ConnList = new List<CSingleSqlItem>();
        protected readonly Dictionary<CSingleSqlExecuter, CSingleSqlItem> ConnDict = new Dictionary<CSingleSqlExecuter, CSingleSqlItem>();

        public int Cfg_MaxWaitSec = 30;

        public void Add(CSingleSqlExecuter _DbConn, int _clone2Total)
        {
            if (_clone2Total < 0)
                throw new Exception("_cloneTimes must >= 0");

            this.Add(_DbConn);

            for(int i =0; i < _clone2Total ; i++)
            {
                CSingleSqlExecuter cl = (CSingleSqlExecuter)_DbConn.Clone();
                this.Add(cl);
            }
        }//Add(CSingleSqlExecuter _DbConn, int _cloneTimes)

        public void Add(CSingleSqlExecuter _DbConn)
        {
            CSingleSqlItem item = new CSingleSqlItem()
            {
                DbConn = _DbConn
            };

            lock (ConnList)
            {
                ConnList.Add(item);
                ConnDict.Add(_DbConn, item);
            }
        }//Add(CSingleSqlExecuter _DbConn)

        /// <summary>
        /// 临时借用一个DB链接. 如果不等待,则失败时返回null. 如果等待,则在超时范围内自动等待,否则抛出异常.
        /// </summary>
        /// <param name="_BorrowerId"></param>
        /// <param name="_bWait"></param>
        /// <returns></returns>
        public CSingleSqlExecuter Borrow(string _BorrowerId, bool _bWait)
        {
            lock(ConnList)
            {
                CSingleSqlItem rst = null;
                foreach (var crrItem in this.ConnList)
                {
                    if (crrItem.BorrowerId == null)
                    {
                        rst = crrItem;
                        break;
                    }

                }
                
                if ( rst != null)
                {
                    rst.BorrowerId = _BorrowerId;
                    return rst.DbConn;
                }

                if (!_bWait)
                    return null;

                //如果需要等待
                for(int i=0; i < this.Cfg_MaxWaitSec ; i++ )
                {
                    Thread.Sleep(1000);
                    var tryRst = Borrow( _BorrowerId, false);
                    if (tryRst != null)
                        return tryRst;
                }

                throw new Exception($"Borrow time out. Can't get a DbConn in {this.Cfg_MaxWaitSec} sec.");
            }//lock(ConnList)
        }// Borrow(bool _bWait)

        public void Return(CSingleSqlExecuter _DbConn)
        {
            lock(this.ConnList)
            {
                this.ConnDict[_DbConn].BorrowerId = null;

            }//lock(this.ConnList)

        }//Return(CSingleSqlExecuter _DbConn)

        public void CloseAndClearAllConnections()
        {
            lock (this.ConnList)
            {

                foreach (var crrItem in this.ConnList)
                    crrItem.DbConn.CloseDbConnNow();

                this.ConnList.Clear();
                this.ConnDict.Clear();
            }//lock (this.ConnList)
        }//void CloseAndClearAllConnections()

    }//class CSingleSqlPool
}
