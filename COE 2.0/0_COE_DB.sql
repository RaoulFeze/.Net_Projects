--Create DATABASE COE_DB

--Drop table TrailerOperator

--Drop table YardEmployee

--Drop table EmployeeLicense

--Drop table EmployeeRole

--Drop table EmployeeStanding

--Drop Table Employee_Audit_Trail

--Drop Table Site_Audit_Trail

--Drop Table SiteHazard

--Drop Table ToolsCheckList

--Drop Table JobCard

--Drop Table CrewMember

-- Drop Table TruckLicense

--Drop Table Crew

--Drop Table Tool

--Drop Table Truck

--Drop Table Site

--Drop table OperatorPermit

--Drop Table CorrectiveAction

--Drop Table Hazard

--DROP Table Equipment

--Drop Table Yard

--Drop table Role

--Drop table Standing

--Drop table Task

--Drop Table Community

--Drop Table HazardCategory

--Drop table LicenseClass

--Drop table EquipmentCategory

--Drop Table TruckCategory

--Drop Table SiteType

--Drop Table District

--Drop Table Employee


create table Employee
(
	EmployeeID integer identity(1,1) not null constraint pk_Employee primary key clustered,
	PayrollNumber int null,
	FirstName varchar(20) null,
	LastName varchar(20) not null,
	Email varchar(40) null,
	Phone varchar(13) null constraint ck_phone check (Phone like '([1-9][0-9][0-9]) [0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
)

create table District
(
	DistrictID integer identity(1,1) not null constraint pk_District primary key clustered,
	DistrictName varchar(20) not null
)

create table SiteType
(
	SiteTypeID integer identity(1,1) not null constraint pk_SiteType primary key clustered,
	SiteTypeDescription char(1) not null,
	NumberOfCyle int not null
)
create table EquipmentCategory
(
	CategoryID integer identity(1,1) not null constraint PK_EquipmentCategory primary key clustered,
	Description varchar(20) not null
)

create table TruckCategory
(
	CategoryID integer identity(1,1) not null constraint PK_TruckCategory primary key clustered,
	Description varchar(20)
)

create table HazardCategory
(
	HazardCategoryID integer identity(1,1) not null constraint pk_HazardCategory primary key clustered,
	HazardCategoryName varchar(100) not null
)

create table Community
(
	CommunityID integer identity(1,1) not null constraint pk_Community primary key clustered,
	Name varchar(50) not null
)

create table Task
(
	TaskID integer identity(1,1) not null constraint PK_Task primary key clustered,
	Description varchar(50)
)

create table Role
(
	RoleID integer identity(1,1) not null constraint PK_Role primary key clustered,
	Description varchar(20) not null
)

Create table Standing
(
	StandingID Integer identity(1,1) not null constraint PK_Standing primary key clustered,
	Description varchar(20) not null
)

create table LicenseClass
(
	LicenseClassID integer identity(1,1) not null constraint PK_DriverPermit primary key clustered,
	Description varchar (20) not null,
	RenewalCycle int null
)

---------------------FIRST CHILDREN------------------------------------
create table TrailerOperator
(
	OperatorID integer identity not null constraint PK_TrailerOperator primary key clustered,
	EmployeeID int null constraint FK_TrailerOperator_to_Employee references Employee(EmployeeID),
	TrainedDate Datetime null
)
create nonclustered index IX_TrailerOperator_EmployeeID
on TrailerOperator(EmployeeID)

create table EmployeeRole
(
	EmployeeID int not null constraint FK_EmployeeRole_to_Employee references Employee(EmployeeID),
	RoleID int not null constraint FK_EmployeeRole_to_Role references Role(RoleID),
	AppointmentDate datetime null,
	constraint PK_EmployeeRole primary key clustered(EmployeeID, RoleID)
)
create nonclustered index IX_EmployeeRole_RoleID
on EmployeeRole(RoleID)
create nonclustered index IX_EmployeeRole_EmployeeID
on EmployeeRole(EmployeeID)

create table EmployeeLicense
(
	LicenseID integer identity(1,1) not null constraint PK_EmployeeLicense primary key clustered,
	EmployeeID int not null constraint FK_EmployeeLicense_to_Employee references Employee(EmployeeID),
	LicenseClassID int not null constraint FK_EmployeeLicense_to_LicenseClass references LicenseClass(LicenseClassID),
	RenewalDate datetime null
)
create nonclustered index IX_EmployeeLicense_EmployeeID
on EmployeeLicense(EmployeeID)
create nonclustered index EmployeeLicense_LicenseClassID
on EmployeeLicense(LicenseClassID)

create table EmployeeStanding
(
	EmployeeID int not null constraint FK_EmployeeStanding_to_Employee references Employee(EmployeeID),
	StandingID int not null constraint FK_EmployeeStanding_to_Standing references Standing(StandingID),
	StandingDate Datetime not null,
	constraint PK_EmployeeStanding primary key clustered(EmployeeID, StandingID)
)
create table Yard
(
	YardID integer identity(1,1) not null constraint pk_Yard primary key clustered,
	DistrictID int not null constraint fk_Yard_To_District references District(DistrictID),
	YardName varchar(20) not null
)

create table YardEmployee
(
	YardEmeployeeID integer identity(1,1) not null constraint PK_YardEmployee primary key clustered,
	EmployeeID int not null constraint FK_YardEmployee_to_Employee references Employee(EmployeeID),
	YardID int not null constraint FK_YardEmployee_to_Yard references Yard(YardID),
	AssignedDate datetime not null
)
create nonclustered index IX_YardEmployee_EmployeeID
on YardEmployee(EmployeeID)
create nonclustered index IX_yardEmployee_YardID
on YardEmployee(YardID)


create table Equipment
(
	EquipmentID integer identity(1,1) not null constraint PK_Equipment primary key clustered,
	CategoryID int null constraint FK_Equipment_to_EquipmentCategory references EquipmentCategory(CategoryID),
	YardID int null constraint FK_Equipment_to_Yard references Yard(YardID),
	EquipmentNumber varchar(20) not null,
	Description varchar(20) not null
)
create nonclustered index IX_Equipment_CategoryID
on Equipment(CategoryID)

create table Hazard
(
	HazardID integer identity(1,1) not null constraint pk_Hazard primary key clustered,
	HazardCategoryID int null constraint fk_Hazard_To_HazardCategory references hazardCategory(HazardCategoryID),
	HazardDescription varchar(100) not null
)

------------------------SECOND CHILDREN----------------------------------------
create table CorrectiveAction
(
	CorrectiveActionID integer identity(1,1) not null constraint pk_CorrectiveAction primary key clustered,
	HazardID int null constraint fk_CorrectiveAction_To_hazard references Hazard(HazardID),
	CorrectiveActionDescription varchar(500) not null
)
create nonclustered index CorrectiveAction_HazardID
on CorrectiveAction(HazardID)


create table TruckLicense
(
	TruckLicenseID integer identity(1,1) constraint PK_TruckLicense primary key clustered,
	CategoryID int not null constraint FK_TruckLicense_to_TruckCategory references TruckCategory(CategoryID),
	LicenseClassID int not null constraint FK_TruckLicense_to_LicenseClass references LicenseClass(LicenseClassID)
)

create nonclustered index IX_TruckLicense_LicenceCategoryID
on TruckLicense(CategoryID)
create nonclustered index IX_TruckLicense_LicenceClassID
on TruckLicense(LicenseClassID)

create table OperatorPermit
(
	PermitID integer identity(1,1) not null constraint PK_Operator primary key clustered,
	EmployeeID int not null constraint FK_Operator_to_Employee references Employee(EmployeeID),
	CategoryID int not null constraint FK_Operator_to_EquipmentCategory references EquipmentCategory(CategoryID),
	CompletionDate datetime null,
	RenewalDate datetime null
)
create nonclustered index IX_OperatorPermit_EmployeeID
on OperatorPermit(EmployeeID)
create nonclustered index IX_OperatorPermit_CategoryID
on OperatorPermit(CategoryID)

create table Site
(
	SiteID integer identity(1,1) not null constraint pk_Site primary key clustered,
	SiteTypeID int null constraint FK_Site_to_SiteType references SiteType(SitetYpeID),
	YardID int null constraint fk_Site_To_yard references Yard(YardID),
	CommunityID int null constraint fk_Site_To_Community references Community(CommunityID),
	Pin int not null,
	Description varchar(200) not null,
	StreetAddress varchar(35) null,
	Area int not null,
	Notes varchar(1000) null,
	Grass int constraint df_GrassOnSite default 0,
	Watering bit constraint df_WateringSite default 0,
	Planting bit constraint df_PlantingSite default 0
)
create nonclustered index IX_Site_YardID
on Site(YardID)
create nonclustered index IX_Site_to_SiteTypeID
on Site(SiteTypeID)
create nonclustered index IX_Site_CommunityID
on Site(CommunityID)

create table Truck
(
	TruckID integer identity not null constraint pk_Truck primary key clustered,
	CategoryID int null constraint FK_Truck_to_TruckCategory references TruckCategory(CategoryID),
	YardID int null constraint FK_Truck_To_Yard references Yard(YardID),
	TruckNumber varchar(20) not null,
	TruckDescription varchar(20) not null
)
create nonclustered index IX_Truck_CategoryID
on Truck(CategoryID)
create nonclustered index IX_Truck_YardID
on Truck(YardID)

create table Tool
(
	ToolID integer identity(1,1) not null constraint pk_Tool primary key clustered,
	YardID int null constraint fk_Tool_To_Yard references Yard(YardID),
	ToolDescription varchar(30) not null
)
create nonclustered index IX_Tool_YardID
on Tool(YardID)

 -----------------THIRD CHILDREN------------------------------------------

create table Crew
(
	CrewID integer identity(1,1) not null constraint pk_Crew primary key clustered,
	CrewDate datetime not null,
	TruckID int null constraint fk_Crew_To_Truck references Truck(TruckID),
	EquipmentID int null constraint FK_Crew_to_Equipment references Equipment(EquipmentID),
	FLHA_CompletedBy  bit constraint df_CompletedFLHA default null,
	KM_Start int null,
	KM_End int null,
	AdditionalComments varchar(100) null,
	constraint ck_KM_End_GreaterThan_KM_Start check (KM_End >= KM_Start)
)
create nonclustered index IX_Crew_TruckID
on Crew(TruckID)
create nonclustered index IX_Crew_EquipmentID
on Crew(EquipmentID)

create table CrewMember
(
	CrewMemberID integer identity(1,1) not null constraint pk_CrewMember primary key clustered,
	EmployeeID int not null constraint fk_CrewMwmber_To_Employee references Employee(EmployeeID),
	CrewID int not null constraint fk_CrewMember_To_Crew references Crew(CrewID),
	Driver  bit constraint df_NotDriver default null
)
create nonclustered index IX_CrewMember_EmployeeID
on CrewMember(EmployeeID)

create table JobCard
(
	JobCardID Integer identity(1,1) not null constraint pk_JobCard primary key clustered,
	SiteID int not null constraint fk_SiteHistory_to_Site references Site(SiteID),
	TaskID int not null constraint kf_JobCard_To_Task references Task(TaskID),
	CrewID int not null constraint fk_JobCard_To_Crew references Crew(CrewID),
	TimeOnSite time null,
    TimeOffSite time null,
	ActionRequired varchar(100) null,
	ClosedDate DateTime null
)
create nonclustered index IX_JobCard_SiteID
on JobCard(SiteID)
create nonclustered index IX_JobCard_TaskID
on JobCard(TaskID)

create table ToolsChecklist
(
	ToolCheckListID integer identity(1,1) not null constraint pk_ToolCheckList primary key clustered,
	ToolID int not null constraint fk_ToolsChecklist_To_Tool references Tool(ToolID),
	CrewID int not null constraint fk_ToolsChecklist_To_Crew references Crew(CrewID),
	Quantity int null
)
create nonclustered index IX_ToolsChecklist_ToolID
on ToolsChecklist(ToolID)

create table SiteHazard
(
	SiteHazardID integer identity(1,1) not null constraint pk_SiteHazard primary key clustered,
	HazardID int not null constraint fk_SiteHazard_To_Hazard references hazard(HazardID),
	JobCardID int not null constraint fk_SiteHazard_To_JobCard references JobCard(JobCardID),
	ReviewedBy int null,
	ReviewedDate Datetime null
)
create nonclustered index IX_SiteHazard_JobCardID
on SiteHazard(JobCardID)

create table Employee_Audit_Trail
(
	AuditTrailID integer identity(1,1) not null constraint PK_Employee_Audit_Trail primary key clustered,
	EmployeeID int not null constraint FK_Employee_Audit_Trail_to_Employee references Employee(EmployeeID),
	ColumnAffected varchar(10) not null,
	ChangedBy int not null,
	ChangedDate datetime not null,
	OldValue varchar(50) null,
	NewValue varchar(50) null
)
create nonclustered index IX_Employee_Audit_Trail_EmployeeID
on Employee_Audit_Trail(EmployeeID)
create nonclustered index IX_Employee_Audit_Trail_ChangedBy
on Employee_Audit_Trail(ChangedBy)

create table Site_Audit_Trail
(
	AuditTrailID Integer identity(1,1) not null constraint PK_Site_Audit_Trail primary key clustered,
	SiteID int not null constraint FK_Site_Audit_Trail_to_Site references Site(SiteID),
	ColumnAffected varchar(50) not null,
	ChangedBy int not null,
	ChangedDate datetime not null,
	OldValue varchar(100) null,
	NewValue varchar(100) null
)
create nonclustered index IX_Site_Audit_Trail_SiteID
on Site_Audit_Trail(SiteID)
create nonclustered index IX_Site_Audit_Trail_ChangedBy
on Site_Audit_Trail(ChangedBy)