using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class OneGTDBLL
    {

        #region 收集箱表

        public bool Update_GTDInbox(GTDInbox info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<GTDInbox>().Insert(info);
            else
                return new TSqlBaseDAL<GTDInbox>().Update(info);
        }

        public bool Delete_GTDInbox(string condition)
        {
            return new TSqlBaseDAL<GTDInbox>().Delete(condition) > 0;
        }

        public bool Delete_GTDInbox(GTDInbox info)
        {
            return new TSqlBaseDAL<GTDInbox>().Delete(info);
        }

        public GTDInbox Get_GTDInboxByID(string infoid)
        {
            return new TSqlBaseDAL<GTDInbox>().GetById(infoid);
        }

        public List<GTDInbox> List_GTDInbox(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<GTDInbox>().GetListByCondition(fields, condition, orderby);
        }

        public List<GTDInbox> Pagination_GTDInbox(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<GTDInbox>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 任务表

        public bool Update_GTDTask(GTDTask info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<GTDTask>().Insert(info);
            else
                return new TSqlBaseDAL<GTDTask>().Update(info);
        }

        public bool Delete_GTDTask(string condition)
        {
            return new TSqlBaseDAL<GTDTask>().Delete(condition) > 0;
        }

        public GTDTask Get_GTDTaskByID(string infoid)
        {
            return new TSqlBaseDAL<GTDTask>().GetById(infoid);
        }

        public List<GTDTask> List_GTDTask(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<GTDTask>().GetListByCondition(fields, condition, orderby);
        }

        public List<GTDTask> Pagination_GTDTask(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<GTDTask>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 回收表

        public bool Update_GTDRecycle(GTDRecycle info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<GTDRecycle>().Insert(info);
            else
                return new TSqlBaseDAL<GTDRecycle>().Update(info);
        }

        public bool Delete_GTDRecycle(string condition)
        {
            return new TSqlBaseDAL<GTDRecycle>().Delete(condition) > 0;
        }

        public GTDRecycle Get_GTDRecycleByID(string infoid)
        {
            return new TSqlBaseDAL<GTDRecycle>().GetById(infoid);
        }

        public List<GTDRecycle> List_GTDRecycle(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<GTDRecycle>().GetListByCondition(fields, condition, orderby);
        }

        public List<GTDRecycle> Pagination_GTDRecycle(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<GTDRecycle>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 项目表
        public bool Update_GTDProject(GTDProject info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<GTDProject>().Insert(info);
            else
                return new TSqlBaseDAL<GTDProject>().Update(info);
        }

        public bool Delete_GTDProject(string condition)
        {
            return new TSqlBaseDAL<GTDProject>().Delete(condition) > 0;
        }

        public GTDProject Get_GTDProjectByID(string infoid)
        {
            return  new TSqlBaseDAL<GTDProject>().GetById(infoid);
        }

        public List<GTDProject> List_GTDProject(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<GTDProject>().GetListByCondition(fields, condition, orderby);
        }

        public List<GTDProject> Pagination_GTDProject(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<GTDProject>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 情景表

        public bool Update_GTDScene(GTDScene info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<GTDScene>().Insert(info);
            else
                return new TSqlBaseDAL<GTDScene>().Update(info);
        }

        public bool Delete_GTDScene(string condition)
        {
            return new TSqlBaseDAL<GTDScene>().Delete(condition) > 0;
        }

        public GTDScene Get_GTDSceneByID(string infoid)
        {
            return  new TSqlBaseDAL<GTDScene>().GetById(infoid);
        }

        public List<GTDScene> List_GTDScene(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<GTDScene>().GetListByCondition(fields, condition, orderby);
        }

        public List<GTDScene> Pagination_GTDScene(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<GTDScene>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion
    }
}
