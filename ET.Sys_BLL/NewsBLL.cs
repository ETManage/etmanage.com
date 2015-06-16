using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class NewsBLL
    {
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_NewInfo(NewInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<NewInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<NewInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_NewInfo(string Condition)
        {
            return new TBaseDAL<NewInfo>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public NewInfo Get_NewInfoByID(string infoid)
        {
            NewInfo info = null;
            info = new TBaseDAL<NewInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<NewInfo> List_NewInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<NewInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<NewInfo> PageList_NewInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<NewInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_Blog_Type_Info(NewTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<NewTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<NewTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_Blog_Type_Info(string Condition)
        {
            return new TBaseDAL<NewTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public NewTypeInfo Get_Blog_Type_InfoByID(string infoid)
        {
            NewTypeInfo info = null;
            info = new TBaseDAL<NewTypeInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<NewTypeInfo> List_Blog_Type_Info(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<NewTypeInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<NewTypeInfo> PageList_Blog_Type_Info(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<NewTypeInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
    }
}
