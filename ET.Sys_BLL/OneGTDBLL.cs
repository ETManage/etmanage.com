using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using ET.DALContract;

namespace ET.Sys_BLL
{
    public class OneGTDBLL
    {

        #region �ռ����
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_GTDInbox(GTDInbox info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<GTDInbox>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<GTDInbox>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_GTDInbox(string Condition)
        {
            return new TBaseDAL<GTDInbox>().DeleteInstances(Condition) > 0;

        }
        public bool Delete_GTDInbox(GTDInbox info)
        {
            return new TBaseDAL<GTDInbox>().DeleteInstance(info) > 0;

        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��Ϣ</returns>
        public GTDInbox Get_GTDInboxByID(string infoid)
        {
            GTDInbox info  = new TBaseDAL<GTDInbox>().GetInstanceById(infoid);

            return info;
        }
        public List<GTDInbox> List_GTDInbox(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<GTDInbox>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<GTDInbox> PageList_GTDInbox(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<GTDInbox>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region �����
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_GTDTask(GTDTask info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<GTDTask>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<GTDTask>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_GTDTask(string Condition)
        {
            return new TBaseDAL<GTDTask>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��Ϣ</returns>
        public GTDTask Get_GTDTaskByID(string infoid)
        {
            GTDTask info = new TBaseDAL<GTDTask>().GetInstanceById(infoid);

            return info;
        }
        public List<GTDTask> List_GTDTask(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<GTDTask>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<GTDTask> PageList_GTDTask(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<GTDTask>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region ���ձ�
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_GTDRecycle(GTDRecycle info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<GTDRecycle>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<GTDRecycle>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_GTDRecycle(string Condition)
        {
            return new TBaseDAL<GTDRecycle>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��Ϣ</returns>
        public GTDRecycle Get_GTDRecycleByID(string infoid)
        {
            GTDRecycle info =  new TBaseDAL<GTDRecycle>().GetInstanceById(infoid);

            return info;
        }
        public List<GTDRecycle> List_GTDRecycle(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<GTDRecycle>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<GTDRecycle> PageList_GTDRecycle(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<GTDRecycle>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region ��Ŀ��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_GTDProject(GTDProject info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<GTDProject>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<GTDProject>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_GTDProject(string Condition)
        {
            return new TBaseDAL<GTDProject>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��Ϣ</returns>
        public GTDProject Get_GTDProjectByID(string infoid)
        {
            GTDProject info =  new TBaseDAL<GTDProject>().GetInstanceById(infoid);

            return info;
        }
        public List<GTDProject> List_GTDProject(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<GTDProject>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<GTDProject> PageList_GTDProject(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<GTDProject>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion

        #region �龰��
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        /// <param name="info">��Ϣ</param>
        public bool Operate_GTDScene(GTDScene info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<GTDScene>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<GTDScene>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// ɾ����Ϣ
        /// </summary>
        /// <param name="Condition">������Ҫ��AND��ͷ</param>
        public bool Delete_GTDScene(string Condition)
        {
            return new TBaseDAL<GTDScene>().DeleteInstances(Condition) > 0;

        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <returns>��Ϣ</returns>
        public GTDScene Get_GTDSceneByID(string infoid)
        {
            GTDScene info =  new TBaseDAL<GTDScene>().GetInstanceById(infoid);

            return info;
        }
        public List<GTDScene> List_GTDScene(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<GTDScene>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<GTDScene> PageList_GTDScene(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<GTDScene>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        #endregion
    }
}
