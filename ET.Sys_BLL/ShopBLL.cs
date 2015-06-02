using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class ShopBLL
    {

        

        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_ShopTypeInfo(ShopTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<ShopTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<ShopTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_ShopTypeInfo(string Condition)
        {
            return new TBaseDAL<ShopTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
        public ShopTypeInfo Get_ShopTypeInfoByID(string infoid)
        {
            ShopTypeInfo info  = new TBaseDAL<ShopTypeInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<ShopTypeInfo> List_ShopTypeInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<ShopTypeInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<ShopTypeInfo> PageList_ShopTypeInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<ShopTypeInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_ShopGoodInfo(ShopGoodInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<ShopGoodInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<ShopGoodInfo>().UpdateInstance(info) > 0;
        }
        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_ShopGoodInfo(string Condition)
        {
            return new TBaseDAL<ShopGoodInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// ��ȡ����ģ����Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>ģ����Ϣ</returns>
        public ShopGoodInfo Get_ShopGoodInfoByID(string infoid)
        {
            ShopGoodInfo info = null;
            info = new TBaseDAL<ShopGoodInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<ShopGoodInfo> List_ShopGoodInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<ShopGoodInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<ShopGoodInfo> PageList_ShopGoodInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<ShopGoodInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
    }
}
