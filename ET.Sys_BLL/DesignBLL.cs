using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class DesignBLL
    {

        

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_DesignTypeInfo(DesignTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<DesignTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<DesignTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_DesignTypeInfo(string Condition)
        {
            return new TBaseDAL<DesignTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
        public DesignTypeInfo Get_DesignTypeInfoByID(string infoid)
        {
            DesignTypeInfo info  = new TBaseDAL<DesignTypeInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<DesignTypeInfo> List_DesignTypeInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<DesignTypeInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<DesignTypeInfo> PageList_DesignTypeInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<DesignTypeInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_DesignGoodInfo(DesignGoodInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<DesignGoodInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<DesignGoodInfo>().UpdateInstance(info) > 0;
        }
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_DesignGoodInfo(string Condition)
        {
            return new TBaseDAL<DesignGoodInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
        public DesignGoodInfo Get_DesignGoodInfoByID(string infoid)
        {
            DesignGoodInfo info = null;
            info = new TBaseDAL<DesignGoodInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<DesignGoodInfo> List_DesignGoodInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<DesignGoodInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<DesignGoodInfo> PageList_DesignGoodInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<DesignGoodInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
    }
}
