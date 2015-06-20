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
        public bool Operate_BlogArticleFavorite(BlogArticleFavorite info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogArticleFavorite>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogArticleFavorite>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogArticleFavorite(string Condition)
        {
            return new TBaseDAL<BlogArticleFavorite>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogArticleFavorite Get_BlogArticleFavoriteByID(string infoid)
        {
            BlogArticleFavorite info = null;
            info = new TBaseDAL<BlogArticleFavorite>().GetInstanceById(infoid);

            return info;
        }
        public BlogArticleFavorite Get_BlogArticleFavorite(string Condition)
        {
            BlogArticleFavorite info = null;
            info = new TBaseDAL<BlogArticleFavorite>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogArticleFavorite> List_BlogArticleFavorite(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogArticleFavorite>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogArticleFavorite> PageList_BlogArticleFavorite(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogArticleFavorite>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
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
        public bool Operate_BlogViewRecord(BlogViewRecord info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogViewRecord>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogViewRecord>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogViewRecord(string Condition)
        {
            return new TBaseDAL<BlogViewRecord>().DeleteInstances(Condition) > 0;
        }
        public BlogViewRecord Get_BlogViewRecordByCondition(string condition)
        {
            BlogViewRecord info = new TBaseDAL<BlogViewRecord>().GetInstanceByCondition(condition);

            return info;
        }
        public List<BlogViewRecord> List_BlogViewRecord(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogViewRecord>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogViewRecord> PageList_BlogViewRecord(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogViewRecord>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
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

        #region 博客签到
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogUserSignIn(BlogUserSignIn info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogUserSignIn>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogUserSignIn>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogUserSignIn(string Condition)
        {
            return new TBaseDAL<BlogUserSignIn>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogUserSignIn Get_BlogUserSignInByID(string infoid)
        {
            BlogUserSignIn info = null;
            info = new TBaseDAL<BlogUserSignIn>().GetInstanceById(infoid);

            return info;
        }
        public BlogUserSignIn Get_BlogUserSignIn(string Condition)
        {
            BlogUserSignIn info = null;
            info = new TBaseDAL<BlogUserSignIn>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogUserSignIn> List_BlogUserSignIn(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogUserSignIn>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogUserSignIn> PageList_BlogUserSignIn(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogUserSignIn>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region 博客用户等级
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogUserLevel(BlogUserLevel info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogUserLevel>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogUserLevel>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogUserLevel(string Condition)
        {
            return new TBaseDAL<BlogUserLevel>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogUserLevel Get_BlogUserLevelByID(string infoid)
        {
            BlogUserLevel info = null;
            info = new TBaseDAL<BlogUserLevel>().GetInstanceById(infoid);

            return info;
        }
        public BlogUserLevel Get_BlogUserLevel(string Condition)
        {
            BlogUserLevel info = null;
            info = new TBaseDAL<BlogUserLevel>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogUserLevel> List_BlogUserLevel(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogUserLevel>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogUserLevel> PageList_BlogUserLevel(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogUserLevel>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region 用户经验关联
        /// <summary>
        /// 信息操作
        /// </summary>
        /// <param name="info">信息</param>
        public bool Operate_BlogUserLevelLink(BlogUserLevelLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<BlogUserLevelLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<BlogUserLevelLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_BlogUserLevelLink(string Condition)
        {
            return new TBaseDAL<BlogUserLevelLink>().DeleteInstances(Condition) > 0;
        }
        /// <summary>
        /// 获取单个信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public BlogUserLevelLink Get_BlogUserLevelLinkByID(string infoid)
        {
            BlogUserLevelLink info = null;
            info = new TBaseDAL<BlogUserLevelLink>().GetInstanceById(infoid);

            return info;
        }
        public BlogUserLevelLink Get_BlogUserLevelLink(string Condition)
        {
            BlogUserLevelLink info = null;
            info = new TBaseDAL<BlogUserLevelLink>().GetInstanceByCondition(Condition);

            return info;
        }
        public List<BlogUserLevelLink> List_BlogUserLevelLink(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<BlogUserLevelLink>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<BlogUserLevelLink> PageList_BlogUserLevelLink(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<BlogUserLevelLink>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion
    }
}
