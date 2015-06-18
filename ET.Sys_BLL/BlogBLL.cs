using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;
using ET.Constant.DBConst;

namespace ET.Sys_BLL
{
    public class BlogBLL
    {



        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogTypeInfo(BlogTypeInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogTypeInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogTypeInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogTypeInfo(string Condition)
        {
            return new TBaseDAL<BlogTypeInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogTypeInfo Get_BlogTypeInfoByID(string infoid)
        {
            BlogTypeInfo info = null;
            info = new TBaseDAL<BlogTypeInfo>().GetInstanceById(infoid);

            return info;
        }
        public BlogTypeInfo Get_BlogTypeInfoByCondition(string contidion)
        {
            BlogTypeInfo info = new TBaseDAL<BlogTypeInfo>().GetInstanceByCondition(contidion);

            return info;
        }
        public List<BlogTypeInfo> List_BlogTypeInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogTypeInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogTypeInfo> PageList_BlogTypeInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogTypeInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info"></param>
        /// <param name="IsInsert">是否新增</param>
        /// <returns></returns>
        public bool Operate_BlogArticleInfo(BlogArticleInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogArticleInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogArticleInfo>().UpdateInstance(info) > 0;
        }
        /// <summary>
        /// 禁用状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_DisableArticle(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=-1 WHERE 1=1" + condition, TableNames.BlogArticleInfo)) > 0;
        }
        /// <summary>
        /// 启用状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_EnabledArticle(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=1 WHERE 1=1" + condition, TableNames.BlogArticleInfo)) > 0;
        }
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogArticleInfo(string Condition)
        {
            return new TBaseDAL<BlogArticleInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogArticleInfo Get_BlogArticleInfoByID(string infoid)
        {
            BlogArticleInfo info = null;
            info = new TBaseDAL<BlogArticleInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<BlogArticleInfo> List_BlogArticleInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogArticleInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogArticleInfo> PageList_BlogArticleInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogArticleInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }


        public bool Operate_BlogCommentInfo(BlogCommentInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogCommentInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogCommentInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogCommentInfo(string Condition)
        {
            return new TBaseDAL<BlogCommentInfo>().DeleteInstances(Condition) > 0;
        }
        public List<BlogCommentInfo> List_BlogCommentInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogCommentInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogCommentInfo> PageList_BlogCommentInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogCommentInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }


        #region 收藏
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogArticleFavoriteInfo(BlogArticleFavoriteInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogArticleFavoriteInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogArticleFavoriteInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogArticleFavoriteInfo(string Condition)
        {
            return new TBaseDAL<BlogArticleFavoriteInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogArticleFavoriteInfo Get_BlogArticleFavoriteInfoByID(string infoid)
        {
            BlogArticleFavoriteInfo info = null;
            info = new TBaseDAL<BlogArticleFavoriteInfo>().GetInstanceById(infoid);

            return info;
        }
        public BlogArticleFavoriteInfo Get_BlogArticleFavoriteInfo(string Condition)
        {
            BlogArticleFavoriteInfo info = null;
            info = new TBaseDAL<BlogArticleFavoriteInfo>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogArticleFavoriteInfo> List_BlogArticleFavoriteInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogArticleFavoriteInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogArticleFavoriteInfo> PageList_BlogArticleFavoriteInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogArticleFavoriteInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion
        #region 友情链接
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogRollInfo(BlogRollInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogRollInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogRollInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogRollInfo(string Condition)
        {
            return new TBaseDAL<BlogRollInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogRollInfo Get_BlogRollInfoByID(string infoid)
        {
            BlogRollInfo info = null;
            info = new TBaseDAL<BlogRollInfo>().GetInstanceById(infoid);

            return info;
        }
        public BlogRollInfo Get_BlogRollInfo(string Condition)
        {
            BlogRollInfo info = null;
            info = new TBaseDAL<BlogRollInfo>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogRollInfo> List_BlogRollInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogRollInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogRollInfo> PageList_BlogRollInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogRollInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion
        #region 博客预览
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogViewRecordInfo(BlogViewRecordInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogViewRecordInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogViewRecordInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogViewRecordInfo(string Condition)
        {
            return new TBaseDAL<BlogViewRecordInfo>().DeleteInstances(Condition) > 0;
        }
        #endregion


        #region 留言板
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogMessageInfo(BlogMessageInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogMessageInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogMessageInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogMessageInfo(string Condition)
        {
            return new TBaseDAL<BlogMessageInfo>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogMessageInfo Get_BlogMessageInfoByID(string infoid)
        {
            BlogMessageInfo info = null;
            info = new TBaseDAL<BlogMessageInfo>().GetInstanceById(infoid);

            return info;
        }
        public BlogMessageInfo Get_BlogMessageInfo(string Condition)
        {
            BlogMessageInfo info = null;
            info = new TBaseDAL<BlogMessageInfo>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogMessageInfo> List_BlogMessageInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogMessageInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogMessageInfo> PageList_BlogMessageInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogMessageInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion


        #region 博客文章投稿
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogPublish(BlogPublish info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogPublish>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogPublish>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogPublish(string Condition)
        {
            return new TBaseDAL<BlogPublish>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogPublish Get_BlogPublishByID(string infoid)
        {
            BlogPublish info = null;
            info = new TBaseDAL<BlogPublish>().GetInstanceById(infoid);

            return info;
        }
        public BlogPublish Get_BlogPublish(string Condition)
        {
            BlogPublish info = null;
            info = new TBaseDAL<BlogPublish>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogPublish> List_BlogPublish(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogPublish>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogPublish> PageList_BlogPublish(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogPublish>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion
    }
}
