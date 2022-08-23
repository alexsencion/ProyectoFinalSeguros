CREATE DATABASE insurancedb;
USE insurancedb;

CREATE TABLE address_details
    (
        address_id int primary key,
        h_no varchar(6),
        city varchar(50),
        addressline1 varchar(50),
        state varchar(50),
        pin varchar(50)
     );

	 CREATE TABLE user_details
    (    
        user_id int primary key,
        firstname varchar(50),
        lastname varchar(50),
        email varchar(50),
        mobileno varchar(50),
        address_id int references address_details(address_id),
        dob date
     );

	 
		CREATE TABLE ref_policy_types
    (
        policy_type_code varchar(10) primary key,
        policy_type_name varchar(50)
    );

       CREATE TABLE policy_sub_types
    (
        policy_type_id varchar(10) primary key,
        policy_type_code varchar(10) references         
        ref_policy_types(policy_type_code),
        description varchar(50),
        yearsofpayements int,
        amount int,
        maturityperiod int,
        maturityamount int,
        validity int
     );

	 CREATE TABLE user_policies
    (
        policy_no varchar(20) primary key,
        user_id int references user_details(user_id),
        date_registered date,
        policy_type_id varchar(10) references 
        policy_sub_types(policy_type_id)         
    );

	CREATE TABLE policy_payments
    (
        receipno int primary key,
        user_id int references user_details(user_id),
        policy_no varchar(20) references user_policies(policy_no),
        dateofpayment date,
        amount int,
        fine int
    );

/*address_details table:*/

INSERT INTO address_details values(1, '6-21', 'hyderabad', 'kphb', 'andhra pradesh', 1254);
INSERT INTO address_details values(2, '7-81', 'chennai', 'seruseri', 'tamilnadu', 16354);
INSERT INTO address_details values(3, '3-71', 'lucknow', 'street', 'uttarpradesh', 86451);
INSERT INTO address_details values(4, '4-81', 'mumbai', 'iroli', 'maharashtra', 51246);
INSERT INTO address_details values(5, '5-81', 'bangalore', 'mgroad', 'karnataka', 125465);
INSERT INTO address_details values(6, '6-81', 'ahamadabad', 'street2', 'gujarat', 125423);
INSERT INTO address_details values(7, '9-21', 'chennai', 'sholinganur', 'tamilnadu', 654286);

/* user_details  table:*/

INSERT INTO user_details values(1111,'raju','reddy','raju@gmail.com','9854261456',4,'1986-4-   11');
INSERT INTO user_details values(2222,'vamsi','krishna','vamsi@gmail.com','9854261463',1,'1990-4-11');
INSERT INTO user_details values(3333,'naveen','reddy','naveen@gmail.com','9854261496',4,'1985-3-14');
INSERT INTO user_details values(4444,'raghava','rao','raghava@gmail.com','9854261412',4,'1985-9-21');
INSERT INTO user_details values(5555,'harsha','vardhan','harsha@gmail.com','9854261445',4,'1992-10-11');

/*ref_policy_types*/

INSERT INTO ref_policy_types values('58934', 'car');
INSERT INTO ref_policy_types values('58539', 'home');
INSERT INTO ref_policy_types values('58683', 'life');

/*policy_sub_types*/

INSERT INTO policy_sub_types values('6893','58934','theft',1,5000,null,200000,1);
INSERT INTO policy_sub_types values('6894','58934','accident',1,20000,null,200000,3);
INSERT INTO policy_sub_types values('6895','58539','fire',1,50000,null,500000,3);
INSERT INTO policy_sub_types values('6896','58683','anandhlife',7,50000,15,1500000,null);
INSERT INTO policy_sub_types values('6897','58683','sukhlife',10,5000,13,300000,null);

/*user_policies*/

INSERT INTO user_policies values('689314',1111,'1994-4-18','6896');
INSERT INTO user_policies values('689316',1111,'2012-5-18','6895');
INSERT INTO user_policies values('689317',1111,'2012-6-20','6894');
INSERT INTO user_policies values('689318',2222,'2012-6-21','6894');
INSERT INTO user_policies values('689320',3333,'2012-6-18','6894');
INSERT INTO user_policies values('689420',4444,'2012-4-09','6896');

/*policy_payments*/

INSERT INTO policy_payments values(121,4444,'689420','2012-4-09',50000,null);
INSERT INTO policy_payments values(345,4444,'689420','2013-4-09',50000,null);
INSERT INTO policy_payments values(300,1111,'689317','2012-6-20',20000,null);
INSERT INTO policy_payments values(225,1111,'689316','2012-5-18',20000,null);
INSERT INTO policy_payments values(227,1111,'689314','1994-4-18',50000,null);
INSERT INTO policy_payments values(100,1111,'689314','1995-4-10',50000,null);
INSERT INTO policy_payments values(128,1111,'689314','1996-4-11',50000,null);
INSERT INTO policy_payments values(96,1111,'689314','1997-4-18',50000,200);
INSERT INTO policy_payments values(101,1111,'689314','1998-4-09',50000,null);
INSERT INTO policy_payments values(105,1111,'689314','1999-4-08',50000,null);
INSERT INTO policy_payments values(120,1111,'689314','2000-4-05',50000,null);
INSERT INTO policy_payments values(367,2222,'689318','2012-6-21',20000,null);
INSERT INTO policy_payments values(298,3333,'689320','2012-6-18',20000,null);

