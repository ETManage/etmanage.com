/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2015/5/22 17:25:18                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GTDInbox') and o.name = 'FK_GTDINBOX_REFERENCE_GTDSCENE')
alter table GTDInbox
   drop constraint FK_GTDINBOX_REFERENCE_GTDSCENE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GTDInbox') and o.name = 'FK_GTDINBOX_REFERENCE_GTDPROJE')
alter table GTDInbox
   drop constraint FK_GTDINBOX_REFERENCE_GTDPROJE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GTDTask') and o.name = 'FK_GTDTASK_REFERENCE_GTDSCENE')
alter table GTDTask
   drop constraint FK_GTDTASK_REFERENCE_GTDSCENE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GTDTask') and o.name = 'FK_GTDTASK_REFERENCE_GTDPROJE')
alter table GTDTask
   drop constraint FK_GTDTASK_REFERENCE_GTDPROJE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDFriend')
            and   type = 'U')
   drop table GTDFriend
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDInbox')
            and   type = 'U')
   drop table GTDInbox
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDProject')
            and   type = 'U')
   drop table GTDProject
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDRecycle')
            and   type = 'U')
   drop table GTDRecycle
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDScene')
            and   type = 'U')
   drop table GTDScene
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GTDTask')
            and   type = 'U')
   drop table GTDTask
go

/*==============================================================*/
/* Table: GTDFriend                                             */
/*==============================================================*/
create table GTDFriend (
   FID                  uniqueidentifier     not null,
   UserID               uniqueidentifier     null,
   FUserID              uniqueidentifier     null,
   constraint PK_GTDFRIEND primary key (FID)
)
go

/*==============================================================*/
/* Table: GTDInbox                                              */
/*==============================================================*/
create table GTDInbox (
   BoxID                uniqueidentifier     not null,
   BoxTitle             nvarchar(50)         null,
   BoxContent           text                 null,
   BoxLabel             nvarchar(100)        null,
   Priority             varchar(20)          null default 'none',
   ProjectID            uniqueidentifier     null,
   SceneID              uniqueidentifier     null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   CreateTime           datetime             null,
   Creator              uniqueidentifier     null,
   DoUserID             uniqueidentifier     null,
   Status               varchar(20)          null default 'none',
   BoxType              varchar(20)          null default 'required',
   constraint PK_GTDINBOX primary key (BoxID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'none/low/middle/high
   ',
   'user', @CurrentUser, 'table', 'GTDInbox', 'column', 'Priority'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'recycle/none/finish,默认none',
   'user', @CurrentUser, 'table', 'GTDInbox', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'required/optional，默认required',
   'user', @CurrentUser, 'table', 'GTDInbox', 'column', 'BoxType'
go

/*==============================================================*/
/* Table: GTDProject                                            */
/*==============================================================*/
create table GTDProject (
   ProjectID            uniqueidentifier     not null,
   ProjectName          nvarchar(50)         null,
   description          nvarchar(500)        null,
   CreateTime           datetime             null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   Status               varchar(20)          null default 'none',
   constraint PK_GTDPROJECT primary key (ProjectID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'recycle/none/finish,默认none',
   'user', @CurrentUser, 'table', 'GTDProject', 'column', 'Status'
go

/*==============================================================*/
/* Table: GTDRecycle                                            */
/*==============================================================*/
create table GTDRecycle (
   RecycleID            uniqueidentifier     not null,
   RecycleTitle         nvarchar(50)         null,
   RecycleContent       text                 null,
   RecycleLabel         nvarchar(100)        null,
   Priority             nvarchar(20)         null default 'none',
   StartTime            datetime             null,
   EndTime              datetime             null,
   CreateTime           datetime             null,
   CreatorID            uniqueidentifier     null,
   CreatorName          nvarchar(50)         null,
   DoUserID             uniqueidentifier     null,
   DoUserName           nvarchar(50)         null,
   SceneName            nvarchar(50)         null,
   ProjectName          nvarchar(50)         null,
   PStartTime           datetime             null,
   PEndTime             datetime             null,
   RecycleType          nvarchar(20)         null,
   constraint PK_GTDRECYCLE primary key (RecycleID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'none/low/middle/high',
   'user', @CurrentUser, 'table', 'GTDRecycle', 'column', 'Priority'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'boxdel/boxout/taskdel/taskout/taskover/',
   'user', @CurrentUser, 'table', 'GTDRecycle', 'column', 'RecycleType'
go

/*==============================================================*/
/* Table: GTDScene                                              */
/*==============================================================*/
create table GTDScene (
   SceneID              uniqueidentifier     not null,
   SceneName            nvarchar(50)         null,
   description          nvarchar(500)        null,
   constraint PK_GTDSCENE primary key (SceneID)
)
go

/*==============================================================*/
/* Table: GTDTask                                               */
/*==============================================================*/
create table GTDTask (
   TaskID               uniqueidentifier     not null,
   TaskTitle            nvarchar(50)         null,
   TaskContent          text                 null,
   TaskLabel            nvarchar(100)        null,
   Priority             varchar(20)          null default 'none',
   ProjectID            uniqueidentifier     null,
   SceneID              uniqueidentifier     null,
   StartTime            datetime             null,
   EndTime              datetime             null,
   CreateTime           datetime             null,
   Creator              uniqueidentifier     null,
   DoUser               uniqueidentifier     null,
   Status               varchar(20)          null default 'none',
   TaskType             varchar(20)          null default 'required',
   constraint PK_GTDTASK primary key (TaskID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'none/low/middle/high',
   'user', @CurrentUser, 'table', 'GTDTask', 'column', 'Priority'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'recycle/none/finish,默认none',
   'user', @CurrentUser, 'table', 'GTDTask', 'column', 'Status'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'required/optional，默认required',
   'user', @CurrentUser, 'table', 'GTDTask', 'column', 'TaskType'
go

alter table GTDInbox
   add constraint FK_GTDINBOX_REFERENCE_GTDSCENE foreign key (SceneID)
      references GTDScene (SceneID)
go

alter table GTDInbox
   add constraint FK_GTDINBOX_REFERENCE_GTDPROJE foreign key (ProjectID)
      references GTDProject (ProjectID)
go

alter table GTDTask
   add constraint FK_GTDTASK_REFERENCE_GTDSCENE foreign key (SceneID)
      references GTDScene (SceneID)
go

alter table GTDTask
   add constraint FK_GTDTASK_REFERENCE_GTDPROJE foreign key (ProjectID)
      references GTDProject (ProjectID)
go

