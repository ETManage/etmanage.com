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
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_NewInfo(NewInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<NewInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<NewInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_NewInfo(string Condition)
        {
            return new TBaseDAL<NewInfo>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
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
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_Blog_Type_Info(NewTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<NewTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<NewTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_Blog_Type_Info(string Condition)
        {
            return new TBaseDAL<NewTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
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
