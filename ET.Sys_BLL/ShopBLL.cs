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
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_ShopTypeInfo(ShopTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<ShopTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<ShopTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_ShopTypeInfo(string Condition)
        {
            return new TBaseDAL<ShopTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
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
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_ShopGoodInfo(ShopGoodInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<ShopGoodInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<ShopGoodInfo>().UpdateInstance(info) > 0;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_ShopGoodInfo(string Condition)
        {
            return new TBaseDAL<ShopGoodInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
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
