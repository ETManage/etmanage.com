using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class NewsBLL
    {
        #region 资讯信息
        public bool Update_NewInfo(NewInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<NewInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<NewInfo>().Update(info) > 0;
        }

        public bool Delete_NewInfo(string condition)
        {
            return new TSqlBaseDAL<NewInfo>().Delete(condition) > 0;
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
