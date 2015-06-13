using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ET.Sys_DEF;
using System.Data.Common;
using System.Data.SqlClient;
using ET.DALContract;
using ET.ToolKit.DBLayer;
using System.Linq;
namespace ET.Sys_BLL
{
    /// <summary>
    /// �������ݿ������
    /// </summary>
    public class PublicBLL
    {

        /// <summary>
        /// ���ô洢���̷�ҳ��������������ע�⣺����SQL�����ܳ��Ȳ��ܳ���5000
        /// </summary>
        public DataTable GetDatableByPager(string table, string startIndex, string pageSize, string seletestr, string condition, string strOrder, int intOrde)
        {
            int totalCount = 0;
            return GetDatableByPager(table, startIndex, pageSize, seletestr, condition, strOrder, intOrde, out totalCount);
        }

        /// <summary>
        /// ���ô洢���̷�ҳ��������������ע�⣺����SQL�����ܳ��Ȳ��ܳ���5000
        /// </summary>
        public DataTable GetDatableByPager(string table, string startIndex, string pageSize, string seletestr, string condition, string strOrder, int intOrde, out int recordTotalCount)
        {
            recordTotalCount = 0;
            DataTable dt = new BaseDAL().GetDatableByPager(table, startIndex, pageSize, seletestr, condition, strOrder, intOrde, out  recordTotalCount);
            return dt;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strField">��ѯ�ֶ�</param>
        /// <param name="strCondition">������ǰ����Ҫ�Լӡ�AND��</param>
        /// <returns></returns>
        public DataTable GetDataTableByCondition(string strField, string strTableName, string strCondition)
        {
            DataTable dt = new BaseDAL().GetDataTableByCondition(strField, strTableName, strCondition, null);
            return dt;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="strTableName">����</param>
        /// <param name="strField">��ѯ�ֶ�</param>
        /// <param name="strCondition">������ǰ����Ҫ�Լӡ�AND��</param>
        /// <param name="strOrder">����ʽ</param>
        /// <returns></returns>
        public DataTable GetDataTableByCondition(string strField, string strTableName, string strCondition, string strOrder)
        {
            DataTable dt = new BaseDAL().GetDataTableByCondition(strField, strTableName, strCondition, strOrder);
            return dt;
        }

        /// <summary>
        /// ���������õ�һ��ʵ��
        /// </summary>
        public T GetObjectByCondition<T>(string strField, string strTableName, string strCondition, string strOrder) where T : new()
        {
            T t = new BaseDAL().GetObjectByCondition<T>(strField, strTableName, strCondition, strOrder);
            return t;
        }

        /// <summary>
        /// ����SQL���õ�һ��ʵ��
        /// </summary>
        public T GetObjectBySql<T>(string Sql) where T : new()
        {
            return new BaseDAL().GetObjectBySql<T>(Sql);
        }

        /// <summary>
        /// ͨ�ü�¼����ѯ����
        /// </summary>
        /// <param name="Tablename"></param>
        /// <returns></returns>
        public int GetRecordCount(string Tablename, string Condition)
        {
            return new BaseDAL().GetRecordCount(Tablename, Condition);
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="arguments"></param>
        public DataSet GetDataSetByProcedure(string Procedure, Dictionary<string, string> Arguments)
        {
            return new BaseDAL().GetDataSetByProcedure(Procedure, Arguments);
        }

        /// <summary>
        /// ����SQL���õ�һ�����ݼ�
        /// </summary>
        public System.Data.DataSet GetDataSetBySql(string Sql)
        {
            return new BaseDAL().GetDataSetBySql(Sql);
        }

        /// <summary>
        /// ����SQL���õ�һ�����ݱ�
        /// </summary>
        public System.Data.DataTable GetDataTableBySql(string Sql)
        {
            return new BaseDAL().GetDataTableBySql(Sql);
        }

        /// <summary>
        /// ���������õ�һ��ʵ����
        /// </summary>
        public List<T> GetListByCondition<T>(string Fields, string TableName, string Condition, string strOrder) where T : class
        {
            return new BaseDAL().GetListByCondition<T>(Fields, TableName, Condition, strOrder);
        }

        public List<T> GetListByCondition<T>(int TopCount, string Fields, string TableName, string Condition, string strOrder) where T : class
        {
            return new BaseDAL().GetListByCondition<T>(TopCount, Fields, TableName, Condition, strOrder);
        }
        public List<T> GetListByCondition<T>(int TopCount, string Fields, string TableName, string Condition, string strOrder, bool IsNoLock) where T : class
        {
            return new BaseDAL().GetListByCondition<T>(TopCount, Fields, TableName, Condition, strOrder, IsNoLock);
        }
        /// <summary>
        /// ���������õ�һ��ʵ����
        /// </summary>
        public List<T> GetListBySql<T>(string Sql) where T : class
        {
            return new BaseDAL().GetListBySql<T>(Sql);
        }

        public List<KeyAndValue> GetNestListByCondition(string Fields, string TableName, string Condition, string strOrder)
        {
            List<KeyAndValue> Alllist = new BaseDAL().GetListByCondition<KeyAndValue>(Fields, TableName, Condition, strOrder);

            List<KeyAndValue> Outlist = new List<KeyAndValue>();
            NestRecursion(Alllist, "-1", Outlist);
            return Outlist;
        }
        void NestRecursion(List<KeyAndValue> Alllist, string PID, List<KeyAndValue> Outlist)
        {
            foreach (KeyAndValue item in Alllist.Where(info => info.pid == PID))
            {
                KeyAndValue info = item;
                List<KeyAndValue> children = new List<KeyAndValue>();
                NestRecursion(Alllist, item.id, children);
                info.children = children;
                Outlist.Add(info);
            }

        }
        public List<TreeModuleInfo> GetNestTreeByCondition(string Fields, string TableName, string Condition, string strOrder)
        {
            List<TreeModuleInfo> Alllist = new BaseDAL().GetListByCondition<TreeModuleInfo>(Fields, TableName, Condition, strOrder);

            List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
            NestTreeRecursion(Alllist, "-1", Outlist);
            return Outlist;
        }
        void NestTreeRecursion(List<TreeModuleInfo> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (TreeModuleInfo item in Alllist.Where(info => info.pid == PID))
            {
                TreeModuleInfo info = item;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();

                NestTreeRecursion(Alllist, item.id, children);


                info.children = children;
                Outlist.Add(info);
            }

        }
        /// <summary>
        /// ���������õ�һ����ҳ��ʵ����
        /// </summary>
        public List<T> GetListByConditionPager<T>(string Fields, string TableName, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount, bool IsNolock) where T : class, new()
        {
            return new BaseDAL().GetListByPager<T>(Fields, TableName, Condition, Orderby, Offset, Count, ref  RecordTotalCount, IsNolock);
        }

        /// <summary>
        /// ִ��SQL��䷵�ز���������һ������ִ����ɾ��
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public object ExecuteSqlScalar(string Sql)
        {
            return new BaseDAL().ExecuteSqlScalar(Sql);
        }

        /// <summary>
        /// ��ȡ���������ĵ�һ�����ݵ�һ���ֶ�����
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int ExecuteSqlNonQuery(string Sql)
        {
            return new BaseDAL().ExecuteSqlNonQuery(Sql);
        }


        /// <summary>
        /// ִ��SQL��䣬һ�������������⴦���SQL
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int ExecuteSql(string Sql)
        {
            return new BaseDAL().ExecuteSql(Sql);
        }

    }
}
