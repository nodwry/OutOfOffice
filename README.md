## Application functionality

### List of employees
<img width="1434" alt="Знімок екрана 2024-06-20 о 01 29 08" src="https://github.com/nodwry/OutOfOffice/assets/166646734/edc47463-e868-427c-aa42-de3c9b8a4733">

### Form to add a new employee
<img width="1137" alt="Знімок екрана 2024-06-20 о 01 32 37" src="https://github.com/nodwry/OutOfOffice/assets/166646734/25dba900-1eed-4d4c-94eb-37de264e3c4b">

### Form to update employee details
<img width="1154" alt="Знімок екрана 2024-06-20 о 01 35 31" src="https://github.com/nodwry/OutOfOffice/assets/166646734/642df130-ff72-4315-8371-6e91f541fa56">

### Form to show employee details
<img width="925" alt="Знімок екрана 2024-06-20 о 01 36 49" src="https://github.com/nodwry/OutOfOffice/assets/166646734/cfa40d3d-f3f1-4ee7-888e-e3b522821f17">

### Form to assign employee to a project
<img width="686" alt="Знімок екрана 2024-06-20 о 01 37 38" src="https://github.com/nodwry/OutOfOffice/assets/166646734/339f38b8-cb32-406d-9ad7-51dfce2f0375">

### List of projects
<img width="1435" alt="Знімок екрана 2024-06-20 о 01 38 39" src="https://github.com/nodwry/OutOfOffice/assets/166646734/5c27e7a3-4f1b-4d21-a63c-bdcedb079a54">

### Form to add a new project
<img width="1405" alt="Знімок екрана 2024-06-20 о 01 39 38" src="https://github.com/nodwry/OutOfOffice/assets/166646734/d575b82e-9231-415b-93c6-d44eb2b35400">

### Form to update project details
<img width="1399" alt="Знімок екрана 2024-06-20 о 01 40 48" src="https://github.com/nodwry/OutOfOffice/assets/166646734/3328f5c3-9728-4fd8-a041-280ecd931335">

### Form to show project details
<img width="638" alt="Знімок екрана 2024-06-20 о 01 42 13" src="https://github.com/nodwry/OutOfOffice/assets/166646734/3137153c-1aea-4d24-a41c-d0ff59e2fda9">

### List of leave requests
<img width="1439" alt="Знімок екрана 2024-06-20 о 01 43 20" src="https://github.com/nodwry/OutOfOffice/assets/166646734/bf8828a2-2d94-4258-b6f3-f1684cbc9f10">

### Form to submit leave request
<img width="1358" alt="Знімок екрана 2024-06-20 о 01 45 21" src="https://github.com/nodwry/OutOfOffice/assets/166646734/a49ddcad-afb8-44e1-866a-c7b1e3d1bc06">

### Form to update leave request
<img width="1358" alt="Знімок екрана 2024-06-20 о 01 50 52" src="https://github.com/nodwry/OutOfOffice/assets/166646734/ca6acf2f-0839-4e38-8c80-b5e602f8ccea">

### Form to show leave request details
<img width="731" alt="Знімок екрана 2024-06-20 о 01 52 13" src="https://github.com/nodwry/OutOfOffice/assets/166646734/04471cfb-0d70-403d-874f-346fa05c4b10">

### List of approval requests
<img width="1426" alt="Знімок екрана 2024-06-20 о 01 53 31" src="https://github.com/nodwry/OutOfOffice/assets/166646734/cb8a0d21-d0e2-44ee-8ff2-44c2461fcc2e">

### Form to enter reject comment
<img width="883" alt="Знімок екрана 2024-06-20 о 01 54 15" src="https://github.com/nodwry/OutOfOffice/assets/166646734/a5b51674-0f4c-416f-b9bd-7b003cede0fb">

### Form to show approval request details
<img width="686" alt="Знімок екрана 2024-06-20 о 01 55 44" src="https://github.com/nodwry/OutOfOffice/assets/166646734/1b6745b5-e70a-4ed0-a736-a90daf61eec7">

## Database schema of table relations
![schema](https://github.com/nodwry/OutOfOffice/assets/166646734/f61292f3-e202-499f-96fd-5b508bd87a94)

## SQL script to create the database
```

CREATE TABLE "Employees" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Employees" PRIMARY KEY AUTOINCREMENT,
    "Balance" INTEGER NOT NULL,
    "EmployeeStatus" INTEGER NOT NULL,
    "FullName" TEXT NOT NULL,
    "PeoplePartnerId" INTEGER NOT NULL,
    "Position" INTEGER NOT NULL,
    "Subdivision" INTEGER NOT NULL,
    CONSTRAINT "FK_Employees_Employees_PeoplePartnerId" FOREIGN KEY ("PeoplePartnerId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Projects" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Projects" PRIMARY KEY AUTOINCREMENT,
    "ProjectType" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "EndDate" TEXT NOT NULL,
    "ProjectManagerID" INTEGER NOT NULL,
    "Comment" TEXT NULL,
    "Status" INTEGER NOT NULL,
    "Name" TEXT NULL,
    CONSTRAINT "FK_Projects_Employees_ProjectManagerID" FOREIGN KEY ("ProjectManagerID") REFERENCES "Employees" ("Id") ON DELETE CASCADE
);

CREATE TABLE "LeaveRequests" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_LeaveRequests" PRIMARY KEY AUTOINCREMENT,
    "AbsenseReason" INTEGER NOT NULL,
    "Comment" TEXT NULL,
    "EmployeeId" INTEGER NOT NULL,
    "EndDate" TEXT NOT NULL,
    "LastStatusChange" TEXT NOT NULL,
    "LeaveRequestStatus" INTEGER NOT NULL,
    "StartDate" TEXT NOT NULL,
    "SubmittedTime" TEXT NOT NULL,
    CONSTRAINT "FK_LeaveRequests_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ApprovalRequests" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ApprovalRequests" PRIMARY KEY AUTOINCREMENT,
    "ApprovalRequestStatus" INTEGER NOT NULL,
    "Comment" TEXT NULL,
    "EmployeeId" INTEGER NOT NULL,
    "LastStatusChange" TEXT NOT NULL,
    "LeaveRequestId" INTEGER NOT NULL,
    CONSTRAINT "FK_ApprovalRequests_Employees_EmployeeId" FOREIGN KEY ("EmployeeId") REFERENCES "Employees" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ApprovalRequests_LeaveRequests_LeaveRequestId" FOREIGN KEY ("LeaveRequestId") REFERENCES "LeaveRequests" ("Id") ON DELETE CASCADE
);

CREATE TABLE "EmployeeProject" (
    "AssignedEmployeesId" INTEGER NOT NULL,
    "ProjectsId" INTEGER NOT NULL,
    CONSTRAINT "PK_EmployeeProject" PRIMARY KEY ("AssignedEmployeesId", "ProjectsId"),
    CONSTRAINT "FK_EmployeeProject_Employees_AssignedEmployeesId" FOREIGN KEY ("AssignedEmployeesId") REFERENCES "Employees" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_EmployeeProject_Projects_ProjectsId" FOREIGN KEY ("ProjectsId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
);

```






















