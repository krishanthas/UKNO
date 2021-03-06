
CREATE TABLE APP.APP_IFRS_ACCOUNTS
	(
	"AccountNumber"     VARCHAR2 (128) NOT NULL,
	"AsAtDate"          DATE NOT NULL,
	"SolId"             VARCHAR2 (128),
	"CustomerId"        VARCHAR2 (128),
	"Name"              VARCHAR2 (128),
	"Product"           VARCHAR2 (128),
	"GrantedAmountLKR"  NUMBER (18,4),
	"CapitalLKR"        NUMBER (18,4),
	"InterestOSLKR"     NUMBER (18,4),
	"GrantedDate"       DATE,
	"Currency"          VARCHAR2 (128),
	"ImpairmentStatus"  VARCHAR2 (128),
	"ImpairmentAmount"  NUMBER (18,4),
	"PresentValue"      NUMBER (18,4),
	"Classification"    VARCHAR2 (128),
	"Period"            INTEGER,
	"AmortizedAmount"   NUMBER (18,4),
	"InterestRate"      NUMBER (18,4),
	"AppliedInterestRate" NUMBER (18,4),
	"TotalCashFlowAmount" NUMBER (18,4),
	"DelFlag"           VARCHAR2 (128),
	CONSTRAINT PK_ACCOUNTS PRIMARY KEY (AccountNumber, AsAtDate)
	);


CREATE TABLE APP.APP_IFRS_CASHFLOWS
	(
	"Id"          INTEGER NOT NULL,
	"CustomerId"  VARCHAR2 (128),
	"AccountNumber" VARCHAR2 (128),
	"SolId"       VARCHAR2 (128),
	"AccountName" VARCHAR2 (128),
	"InterestRate" NUMBER (18,4),
	"Date"        DATE,
	"Amount"      NUMBER (18,4),
	"PresentValue" NUMBER (18,4),
	"Status"      VARCHAR2 (128),
	"EntryUserId" VARCHAR2 (128),
	"EntryTime"   DATE,
	"AsAtDate"    DATE,
	"DelFlag"     VARCHAR2 (128),
	CONSTRAINT PK_CASHFLOWS_ID PRIMARY KEY (Id)
	);

CREATE TABLE APP.APP_IFRS_CUSTOMERS
	(
	"Id"                  VARCHAR2 (128) NOT NULL,
	"Name"                VARCHAR2 (128),
	"BranchId"            VARCHAR2 (128),
	"BranchCode"          VARCHAR2 (128),
	"CapitalOSLKR"        NUMBER (18,4),
	"StatusCode"          VARCHAR2 (128),
	"TotalPresentValue"   NUMBER (18,4),
	"TotalAmortizedAmount" NUMBER (18,4),
	"TotalCashFlowsAmount" NUMBER (18,4),
	"TotalImpairmentAmount" NUMBER (18,4),
	"ImpairmentStatus"    VARCHAR2 (128),
	"EntryUserId"         VARCHAR2 (128),
	"AsAtDate"            DATE,
	"DelFlag"             VARCHAR2 (128),
	"RelationshipManager" VARCHAR2 (128),
	"Verify_1"            VARCHAR2 (128),
	"Verify_2"            VARCHAR2 (128),
	"Verify_3"            VARCHAR2 (128),
	"Verify_4"            VARCHAR2 (128),
	"q1"                  VARCHAR2 (128),
	"q2"                  VARCHAR2 (128),
	"q3"                  VARCHAR2 (128),
	"q4"                  VARCHAR2 (128),
	"q5"                  VARCHAR2 (128),
	"q6"                  VARCHAR2 (128),
	"q7"                  VARCHAR2 (128),
	"q8"                  VARCHAR2 (128),
	"q9"                  VARCHAR2 (128),
	"q10"                 VARCHAR2 (128),
	"q11"                 VARCHAR2 (128),
	"q12"                 VARCHAR2 (128),
	"q13"                 VARCHAR2 (128),
	"q14"                 VARCHAR2 (128),
	"q15"                 VARCHAR2 (128),
	"q16"                 VARCHAR2 (128),
	"q17"                 VARCHAR2 (128),
	"q18"                 VARCHAR2 (128),
	"q19"                 VARCHAR2 (128),
	"q20"                 VARCHAR2 (128),
	"q21"                 VARCHAR2 (128),
	"q22"                 VARCHAR2 (128),
	"q23"                 VARCHAR2 (128),
	"q24"                 VARCHAR2 (128),
	"q25"                 VARCHAR2 (128),
	"q26"                 VARCHAR2 (128),
	"q27"                 VARCHAR2 (128),
	"q28"                 VARCHAR2 (128),
	"q29"                 VARCHAR2 (128),
	"q30"                 VARCHAR2 (128),
	"StatusHistory"       VARCHAR2 (2048),
	CONSTRAINT PK_CUSTOMERS_ID PRIMARY KEY (Id)
	);

CREATE TABLE APP.APP_IFRS_QUESTIONS
	(
	"Id"   INTEGER NOT NULL,
	"Status" VARCHAR2 (128),
	"Text" VARCHAR2 (1024),
	"Answer" VARCHAR2 (128),
	CONSTRAINT PK_QUESTIONS_ID PRIMARY KEY (Id)
	);

CREATE TABLE APP.APP_IFRS_USERS
	(
	"Id"             VARCHAR2 (128) NOT NULL,
	"Name"           VARCHAR2 (128),
	"NameWithInitials" VARCHAR2 (128),
	"DepartmentId"   VARCHAR2 (128),
	"DepartmentName" VARCHAR2 (128),
	"Email"          VARCHAR2 (128),
	"ReportingId"    VARCHAR2 (128),
	CONSTRAINT PK_USERS_ID PRIMARY KEY (Id)
	);







