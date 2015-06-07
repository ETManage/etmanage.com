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
        public const string SysModuleInfo = "SysModuleInfo";

        /// <summary>
        /// 角色
        /// </summary>
        public const string SysRoleInfo = "SysRoleInfo";

        /// <summary>
        /// 角色权限关系
        /// </summary>
        public const string SysRoleActionLink = "SysRoleActionLink";

        /// <summary>
        /// 权限
        /// </summary>
        public const string SysActionInfo = "SysActionInfo";

        /// <summary>
        /// 部门信息表
        /// </summary>
        public const string UserDepartmentInfo = "UserDepartmentInfo";

        /// <summary>
        /// 岗位信息表
        /// </summary>
        public const string UserPositionInfo = "UserPositionInfo";


        /// <summary>
        /// 用户部门关联表
        /// </summary>
        public const string UserDepartmentLink = "UserDepartmentLink";

        /// <summary>
        /// 用户岗位关联表
        /// </summary>
        public const string UserPositionLink = "UserPositionLink";

        /// <summary>
        /// 用户公司关联表
        /// </summary>
        public const string UserCompanyLink = "UserCompanyLink";

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
        public const string BlogCommentInfo = "BlogCommentInfo";
        public const string BlogRollInfo = "BlogRollInfo";
        public const string BlogViewRecordInfo = "BlogViewRecordInfo";
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
