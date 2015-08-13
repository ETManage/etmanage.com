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
        #region 文章类型
        public bool Update_BlogTypeInfo(BlogTypeInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogTypeInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogTypeInfo>().Update(info) > 0;
        }

        public bool Delete_BlogTypeInfo(string condition)
        {
            return new TSqlBaseDAL<BlogTypeInfo>().Delete(condition) > 0;
        }

        public BlogTypeInfo Get_BlogTypeInfoByID(string infoid)
        {
            return new TSqlBaseDAL<BlogTypeInfo>().GetById(infoid);
        }
        public BlogTypeInfo Get_BlogTypeInfoByCondition(string contidion)
        {
            return new TSqlBaseDAL<BlogTypeInfo>().GetByCondition(contidion);
        }
        public List<BlogTypeInfo> List_BlogTypeInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogTypeInfo>().GetListByCondition(fields, condition, orderby);
        }
        public List<BlogTypeInfo> Pagination_BlogTypeInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogTypeInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 文章操作
        public bool Update_BlogArticleInfo(BlogArticleInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogArticleInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogArticleInfo>().Update(info) > 0;
        }
        /// <summary>
        /// 禁用状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Update_DisableArticle(string condition)
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
        public bool Update_EnabledArticle(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=1 WHERE 1=1" + condition, TableNames.BlogArticleInfo)) > 0;
        }

        public bool Delete_BlogArticleInfo(string condition)
        {
            return new TSqlBaseDAL<BlogArticleInfo>().Delete(condition) > 0;
        }

        public BlogArticleInfo Get_BlogArticleInfoByID(string infoid)
        {
            return new TSqlBaseDAL<BlogArticleInfo>().GetById(infoid);
        }

        public List<BlogArticleInfo> List_BlogArticleInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogArticleInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogArticleInfo> Pagination_BlogArticleInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogArticleInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 文章评论
        public bool Update_BlogCommentInfo(BlogCommentInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogCommentInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogCommentInfo>().Update(info) > 0;
        }

        public bool Delete_BlogCommentInfo(string condition)
        {
            return new TSqlBaseDAL<BlogCommentInfo>().Delete(condition) > 0;
        }

        public List<BlogCommentInfo> List_BlogCommentInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogCommentInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogCommentInfo> Pagination_BlogCommentInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogCommentInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }

        #endregion

        #region 收藏
        public bool Update_BlogArticleFavorite(BlogArticleFavorite info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogArticleFavorite>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogArticleFavorite>().Update(info) > 0;
        }

        public bool Delete_BlogArticleFavorite(string condition)
        {
            return new TSqlBaseDAL<BlogArticleFavorite>().Delete(condition) > 0;
        }

        public BlogArticleFavorite Get_BlogArticleFavoriteByID(string infoid)
        {
            return new TSqlBaseDAL<BlogArticleFavorite>().GetById(infoid);
        }

        public BlogArticleFavorite Get_BlogArticleFavorite(string condition)
        {
            return new TSqlBaseDAL<BlogArticleFavorite>().GetByCondition(condition);
        }

        public List<BlogArticleFavorite> List_BlogArticleFavorite(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogArticleFavorite>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogArticleFavorite> Pagination_BlogArticleFavorite(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogArticleFavorite>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 友情链接
        public bool Update_BlogRollInfo(BlogRollInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogRollInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogRollInfo>().Update(info) > 0;
        }

        public bool Delete_BlogRollInfo(string condition)
        {
            return new TSqlBaseDAL<BlogRollInfo>().Delete(condition) > 0;
        }

        public BlogRollInfo Get_BlogRollInfoByID(string infoid)
        {
            return new TSqlBaseDAL<BlogRollInfo>().GetById(infoid);
        }

        public BlogRollInfo Get_BlogRollInfo(string condition)
        {
            return new TSqlBaseDAL<BlogRollInfo>().GetByCondition(condition);
        }

        public List<BlogRollInfo> List_BlogRollInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogRollInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogRollInfo> Pagination_BlogRollInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogRollInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 博客预览
        public bool Update_BlogViewRecord(BlogViewRecord info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogViewRecord>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogViewRecord>().Update(info) > 0;
        }

        public bool Delete_BlogViewRecord(string condition)
        {
            return new TSqlBaseDAL<BlogViewRecord>().Delete(condition) > 0;
        }

        public BlogViewRecord Get_BlogViewRecordByCondition(string condition)
        {
            return new TSqlBaseDAL<BlogViewRecord>().GetByCondition(condition);
        }

        public List<BlogViewRecord> List_BlogViewRecord(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogViewRecord>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogViewRecord> Pagination_BlogViewRecord(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogViewRecord>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 留言板
        public bool Update_BlogMessageInfo(BlogMessageInfo info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogMessageInfo>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogMessageInfo>().Update(info) > 0;
        }

        public bool Delete_BlogMessageInfo(string condition)
        {
            return new TSqlBaseDAL<BlogMessageInfo>().Delete(condition) > 0;
        }

        public BlogMessageInfo Get_BlogMessageInfoByID(string infoid)
        {
            return new TSqlBaseDAL<BlogMessageInfo>().GetById(infoid);
        }

        public BlogMessageInfo Get_BlogMessageInfo(string condition)
        {
            return new TSqlBaseDAL<BlogMessageInfo>().GetByCondition(condition);
        }

        public List<BlogMessageInfo> List_BlogMessageInfo(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogMessageInfo>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogMessageInfo> Pagination_BlogMessageInfo(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogMessageInfo>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 博客文章投稿
        public bool Update_BlogPublish(BlogPublish info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogPublish>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogPublish>().Update(info) > 0;
        }

        public bool Delete_BlogPublish(string condition)
        {
            return new TSqlBaseDAL<BlogPublish>().Delete(condition) > 0;
        }

        public BlogPublish Get_BlogPublishByID(string infoid)
        {
            return new TSqlBaseDAL<BlogPublish>().GetById(infoid);
        }

        public BlogPublish Get_BlogPublish(string condition)
        {
            return new TSqlBaseDAL<BlogPublish>().GetByCondition(condition);
        }

        public List<BlogPublish> List_BlogPublish(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogPublish>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogPublish> Pagination_BlogPublish(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogPublish>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 博客签到
        public bool Update_BlogUserSignIn(BlogUserSignIn info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogUserSignIn>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogUserSignIn>().Update(info) > 0;
        }

        public bool Delete_BlogUserSignIn(string condition)
        {
            return new TSqlBaseDAL<BlogUserSignIn>().Delete(condition) > 0;
        }

        public BlogUserSignIn Get_BlogUserSignInByID(string infoid)
        {
            return new TSqlBaseDAL<BlogUserSignIn>().GetById(infoid);
        }

        public BlogUserSignIn Get_BlogUserSignIn(string condition)
        {
            return new TSqlBaseDAL<BlogUserSignIn>().GetByCondition(condition);
        }

        public List<BlogUserSignIn> List_BlogUserSignIn(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogUserSignIn>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogUserSignIn> Pagination_BlogUserSignIn(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogUserSignIn>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 博客用户等级
        public bool Update_BlogUserLevel(BlogUserLevel info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogUserLevel>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogUserLevel>().Update(info) > 0;
        }

        public bool Delete_BlogUserLevel(string condition)
        {
            return new TSqlBaseDAL<BlogUserLevel>().Delete(condition) > 0;
        }

        public BlogUserLevel Get_BlogUserLevelByID(string infoid)
        {
            return new TSqlBaseDAL<BlogUserLevel>().GetById(infoid);
        }

        public BlogUserLevel Get_BlogUserLevel(string condition)
        {
            return new TSqlBaseDAL<BlogUserLevel>().GetByCondition(condition);
        }

        public List<BlogUserLevel> List_BlogUserLevel(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogUserLevel>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogUserLevel> Pagination_BlogUserLevel(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogUserLevel>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 用户经验关联
        public bool Update_BlogUserLevelLink(BlogUserLevelLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogUserLevelLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogUserLevelLink>().Update(info) > 0;
        }

        public bool Delete_BlogUserLevelLink(string condition)
        {
            return new TSqlBaseDAL<BlogUserLevelLink>().Delete(condition) > 0;
        }

        public BlogUserLevelLink Get_BlogUserLevelLinkByID(string infoid)
        {
            return new TSqlBaseDAL<BlogUserLevelLink>().GetById(infoid);
        }

        public BlogUserLevelLink Get_BlogUserLevelLink(string condition)
        {
            return new TSqlBaseDAL<BlogUserLevelLink>().GetByCondition(condition);
        }

        public List<BlogUserLevelLink> List_BlogUserLevelLink(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogUserLevelLink>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogUserLevelLink> Pagination_BlogUserLevelLink(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogUserLevelLink>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion

        #region 用户请求
        public bool Update_BlogUserRequest(BlogUserRequest info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<BlogUserRequest>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<BlogUserRequest>().Update(info) > 0;
        }

        public bool Delete_BlogUserRequest(string condition)
        {
            return new TSqlBaseDAL<BlogUserRequest>().Delete(condition) > 0;
        }

        public BlogUserRequest Get_BlogUserRequestByID(string infoid)
        {
            return new TSqlBaseDAL<BlogUserRequest>().GetById(infoid);
        }

        public BlogUserRequest Get_BlogUserRequest(string condition)
        {
            return new TSqlBaseDAL<BlogUserRequest>().GetByCondition(condition);
        }

        public List<BlogUserRequest> List_BlogUserRequest(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<BlogUserRequest>().GetListByCondition(fields, condition, orderby);
        }

        public List<BlogUserRequest> Pagination_BlogUserRequest(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<BlogUserRequest>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        #endregion
    }
}
