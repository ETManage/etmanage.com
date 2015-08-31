using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class DesignBLL
    {
        #region 设计类别管理
        public bool Update_DesignTypeInfo(DesignTypeInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<DesignTypeInfo>().Insert(info);
            else
                return new TSqlBaseDAL<DesignTypeInfo>().Update(info);
        }

        public bool Delete_DesignTypeInfo(string condition)
        {
            return new TSqlBaseDAL<DesignTypeInfo>().Delete(condition) > 0;
        }

        public DesignTypeInfo Get_DesignTypeInfoByID(string infoid)
        {
            return new TSqlBaseDAL<DesignTypeInfo>().GetById(infoid);
        }

        public List<DesignTypeInfo> List_DesignTypeInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<DesignTypeInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<DesignTypeInfo> Pagination_DesignTypeInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<DesignTypeInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 设计管理
        public bool Update_DesignGoodInfo(DesignGoodInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<DesignGoodInfo>().Insert(info);
            else
                return new TSqlBaseDAL<DesignGoodInfo>().Update(info);
        }

        public bool Delete_DesignGoodInfo(string condition)
        {
            return new TSqlBaseDAL<DesignGoodInfo>().Delete(condition) > 0;
        }

        public DesignGoodInfo Get_DesignGoodInfoByID(string infoid)
        {
            return new TSqlBaseDAL<DesignGoodInfo>().GetById(infoid);
        }

        public List<DesignGoodInfo> List_DesignGoodInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<DesignGoodInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<DesignGoodInfo> Pagination_DesignGoodInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<DesignGoodInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion
    }
}
