using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Constant.DBConst
{
    public static class TableNames
    {
        #region 系统框架模块
        /// <summary>
        /// 用户表
        /// </summary>
        public const string UserBaseInfo = "UserBaseInfo";

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public const string UserPropertyInfo = "UserPropertyInfo";

        /// <summary>
        /// 用户角色信息
        /// </summary>
        public const string UserRoleLink = "UserRoleLink";
        /// <summary>
        /// 系统模块
        /// </summary>
        public const string SysFunction = "SysFunction";
        /// <summary>
        /// 用户组织架构，利用type区分，公司company,dept,post
        /// </summary>
        public const string UserOrgLink = "UserOrgLink";
        /// <summary>
        /// 岗位权限表
        /// </summary>
        public const string UserPostFuncLink = "UserPostFuncLink";
        /// <summary>
        /// 部门权限表
        /// </summary>
        public const string UserDeptFuncLink = "UserDeptFuncLink";
        /// <summary>
        /// 用户权限表
        /// </summary>
        public const string UserFuncLink = "UserFuncLink";


        

        /// <summary>
        /// 角色
        /// </summary>
        public const string SysRoleInfo = "SysRoleInfo";

        /// <summary>
        /// 角色权限关系
        /// </summary>
        public const string SysRoleFuncLink = "SysRoleFuncLink";


        /// <summary>
        /// 部门信息表
        /// </summary>
        public const string UserDepartmentInfo = "UserDepartmentInfo";

        /// <summary>
        /// 岗位信息表
        /// </summary>
        public const string UserPositionInfo = "UserPositionInfo";

        /// <summary>
        /// 公司信息表
        /// </summary>
        public const string UserCompanyInfo = "UserCompanyInfo";


        /// <summary>
        /// 新闻类别表
        /// </summary>
        public const string NewInfo = "NewInfo";

        /// <summary>
        /// 新闻信息表
        /// </summary>
        public const string NewTypeInfo = "NewTypeInfo";
       


        #endregion



        #region 博客
        /// <summary>
        /// 博客类别表
        /// </summary>
        public const string BlogTypeInfo = "BlogTypeInfo";
        /// <summary>
        /// 博客信息表
        /// </summary>
        public const string BlogArticleInfo = "BlogArticleInfo";
         /// <summary>
        /// 博客文章收集信息表
        /// </summary>
        public const string BlogArticleFavoriteInfo = "BlogArticleFavoriteInfo";
        /// <summary>
        /// 平评论表
        /// </summary>
        public const string BlogCommentInfo = "BlogCommentInfo";
        /// <summary>
        /// 友情链接
        /// </summary>
        public const string BlogRollInfo = "BlogRollInfo";
        /// <summary>
        /// 预览记录
        /// </summary>
        public const string BlogViewRecordInfo = "BlogViewRecordInfo";
        /// <summary>
        /// 用户投稿
        /// </summary>
        public const string BlogPublish = "BlogPublish";
        
        #endregion


        #region  创意设计
        
                    /// <summary>
        /// 商品类别表
        /// </summary>
        public const string DesignTypeInfo = "DesignTypeInfo";
        /// <summary>
        /// 商品信息表
        /// </summary>
        public const string DesignGoodInfo = "DesignGoodInfo";
        #endregion

        #region GTD平台

        public const string GTDInbox = "GTDInbox";
        public const string GTDTask = "GTDTask";
        public const string GTDRecycle = "GTDRecycle";
        public const string GTDScene = "GTDScene";
        public const string GTDProject = "GTDProject";
        public const string GTDFriend = "GTDFriend";
        #endregion


    }
}
