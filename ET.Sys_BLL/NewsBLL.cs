using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;
using ET.Constant.DBConst;

namespace ET.Sys_BLL
{
    public class NewsBLL
    {
        #region ��Ѷ��Ϣ
        public bool Update_NewInfo(NewInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<NewInfo>().Insert(info);
            else
                return new TSqlBaseDAL<NewInfo>().Update(info);
        }

        public bool Delete_NewInfo(string condition)
        {
            return new TSqlBaseDAL<NewInfo>().Delete(condition) > 0;
        }
        /// <summary>
        /// �޸�״̬
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool UpdateStatus_NewInfo(string condition,int status)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status="+status+" WHERE 1=1" + condition, TableNames.NewInfo)) > 0;
        }
        public NewInfo Get_NewInfoByID(string infoid)
        {
            return new TSqlBaseDAL<NewInfo>().GetById(infoid);
        }

        public List<NewInfo> List_NewInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<NewInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<NewInfo> Pagination_NewInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<NewInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion
    }
}
