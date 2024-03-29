-- SQL Manager for PostgreSQL 5.9.4.51539
-- ---------------------------------------
-- Host      : localhost
-- Database  : test
-- Version   : PostgreSQL 11.7, compiled by Visual C++ build 1914, 64-bit



--
-- Definition for function userclient_getbyusername (OID = 16505) : 
--
SET search_path = public, pg_catalog;
SET check_function_bodies = false;
CREATE FUNCTION public.userclient_getbyusername (
  _username character varying
)
RETURNS TABLE (
  id integer,
  username character varying,
  useridclient character varying,
  clientid integer,
  islogin boolean
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT uc.id,
  		 uc.username,
         uc.useridclient,
         uc.clientid,
         uc.islogin
  FROM  public.userclient uc
  WHERE uc.username = _username;       
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function userclient_getbyusernameandclientid (OID = 16506) : 
--
CREATE FUNCTION public.userclient_getbyusernameandclientid (
  _username character varying,
  _clientid integer
)
RETURNS TABLE (
  id integer,
  username character varying,
  useridclient character varying,
  clientid integer,
  islogin boolean
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT uc.id,
  		 uc.username,
         uc.useridclient,
         uc.clientid,
         uc.islogin
  FROM  public.userclient uc
  WHERE uc.username = _username AND uc.clientid = _clientid;       
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_updateotprrivatekey (OID = 16507) : 
--
CREATE FUNCTION public.membership_updateotprrivatekey (
  _userid integer,
  _otpprivatekey character varying
)
RETURNS void
AS 
$body$
BEGIN
  UPDATE public.membership
  SET otpprivatekey = _otpprivatekey 
  WHERE id = _userid; 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function client_getbydomain (OID = 16508) : 
--
CREATE FUNCTION public.client_getbydomain (
  _domain character varying
)
RETURNS TABLE (
  id integer,
  name character varying,
  title character varying,
  type character varying,
  domain character varying,
  token character varying,
  active boolean,
  "order" integer,
  parentid integer,
  createduser integer,
  lastupdateduser integer,
  createddate timestamp without time zone,
  lastupdateddate timestamp without time zone
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT c.id,
  		 c.name,
         c.title,
         c.type,
         c.domain,
         c.token,
         c.active,
         c.order,
         c.parentid,
         c.createduser,       
         c.lastupdateduser,
         c.createddate,
         c.lastupdateddate      
  FROM  public.client c
  WHERE c.domain = _domain;     
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function userrole_getbyuserid (OID = 16509) : 
--
CREATE FUNCTION public.userrole_getbyuserid (
  _userid integer
)
RETURNS TABLE (
  id integer,
  membershipid integer,
  role integer
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT ur.id,
  		 ur.membershipid,
         ur.role     
  FROM  public.userrole ur
  WHERE ur.membershipid = _userid;     
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_getbyid (OID = 16510) : 
--
CREATE FUNCTION public.membership_getbyid (
  _userid bigint
)
RETURNS TABLE (
  id bigint,
  username character varying,
  displayname character varying,
  loweredusername character varying,
  password character varying,
  hashcode character varying,
  email character varying,
  location integer,
  isapproved boolean,
  islockedout boolean,
  lastpasswordchangeddate date
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT m.id,
  		 m.username,
         m.displayname,
         m.loweredusername,
         m.password,
         m.hashcode,
         m.email,
         m.location,
         m.isapproved,
         m.islockedout,
         m.lastpasswordchangeddate
  FROM  public.membership m
  WHERE m.id = _userid; 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_getlistbysecurity (OID = 16511) : 
--
CREATE FUNCTION public.membership_getlistbysecurity (
  _username character varying,
  _password character varying
)
RETURNS TABLE (
  id bigint,
  username character varying,
  displayname character varying,
  loweredusername character varying,
  password character varying,
  hashcode character varying,
  email character varying,
  location integer,
  isapproved boolean,
  islockedout boolean,
  lastpasswordchangeddate date
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT m.id,
  		 m.username,
         m.displayname,
         m.loweredusername,
         m.password,
         m.hashcode,
         m.email,
         m.location,
         m.isapproved,
         m.islockedout,
         m.lastpasswordchangeddate
  FROM  public.membership m
  WHERE lower(m.username) = _username and lower(m.password) = _password; 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_getlistbyusername (OID = 16512) : 
--
CREATE FUNCTION public.membership_getlistbyusername (
  _username character varying
)
RETURNS TABLE (
  id bigint,
  username character varying,
  displayname character varying,
  loweredusername character varying,
  password character varying,
  hashcode character varying,
  email character varying,
  location integer,
  isapproved boolean,
  islockedout boolean,
  lastpasswordchangeddate date,
  otpprivatekey character varying
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT m.id,
  		 m.username,
         m.displayname,
         m.loweredusername,
         m.password,
         m.hashcode,
         m.email,
         m.location,
         m.isapproved,
         m.islockedout,
         m.lastpasswordchangeddate,
         m.otpprivatekey
  FROM  public.membership m
  WHERE m.username = _username; 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function userrole_getbyusername (OID = 16513) : 
--
CREATE FUNCTION public.userrole_getbyusername (
  _username character varying
)
RETURNS TABLE (
  id integer,
  membershipid integer,
  role integer
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT ur.id,
  		 ur.membershipid,
         ur.role     
  FROM  public.userrole ur INNER JOIN public.membership m on m.id = ur.membershipid
  WHERE m.username = _username;     
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function userclientid_getbyusernameanddomain (OID = 16514) : 
--
CREATE FUNCTION public.userclientid_getbyusernameanddomain (
  _username character varying,
  _domain character varying
)
RETURNS TABLE (
  userid bigint,
  username character varying,
  useridclient character varying,
  domain character varying,
  token character varying
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT uc.userid,
  		 uc.username,
         uc.useridclient,
         uc.domain,
         uc.token
  FROM  public.userclientid uc
  WHERE uc.username = _username AND uc.domain = _domain;       
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_search (OID = 16515) : 
--
CREATE FUNCTION public.membership_search (
  _pageindex integer,
  _pagesize integer,
  _filterkeyword character varying
)
RETURNS TABLE (
  id bigint,
  username character varying,
  displayname character varying,
  loweredusername character varying,
  password character varying,
  hashcode character varying,
  email character varying,
  location integer,
  isapproved boolean,
  islockedout boolean,
  lastpasswordchangeddate date,
  otpprivatekey character varying
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT m.id,
  		 m.username,
         m.displayname,
         m.loweredusername,
         m.password,
         m.hashcode,
         m.email,
         m.location,
         m.isapproved,
         m.islockedout,
         m.lastpasswordchangeddate,
         m.otpprivatekey
  FROM  public.membership m
  order by m.username
  LIMIT _pagesize
  OFFSET _pagesize*(_pageindex -1); 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_search_getcount (OID = 16516) : 
--
CREATE FUNCTION public.membership_search_getcount (
  _filterkeyword character varying
)
RETURNS integer
AS 
$body$
DECLARE _count INTEGER;
BEGIN
  SELECT  COUNT(1) INTO _count
  FROM	  public.membership usr;
  RETURN _count;
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_getlistbyusernameoremail (OID = 16556) : 
--
CREATE FUNCTION public.membership_getlistbyusernameoremail (
  _username character varying,
  _email character varying
)
RETURNS TABLE (
  id bigint,
  username character varying,
  displayname character varying,
  loweredusername character varying,
  password character varying,
  hashcode character varying,
  email character varying,
  location integer,
  isapproved boolean,
  islockedout boolean,
  lastpasswordchangeddate date,
  otpprivatekey character varying
)
AS 
$body$
BEGIN
  RETURN QUERY
  SELECT m.id,
  		 m.username,
         m.displayname,
         m.loweredusername,
         m.password,
         m.hashcode,
         m.email,
         m.location,
         m.isapproved,
         m.islockedout,
         m.lastpasswordchangeddate,
         m.otpprivatekey
  FROM  public.membership m
  WHERE lower(m.email) = _email 
  OR lower(m.username) = _username;
END;
$body$
LANGUAGE plpgsql;
--
-- Structure for table userclient (OID = 16517) : 
--
CREATE TABLE public.userclient (
    id integer NOT NULL,
    username varchar(256) NOT NULL,
    useridclient varchar(256) NOT NULL,
    clientid integer NOT NULL,
    islogin boolean
)
WITH (oids = false);
--
-- Structure for table client (OID = 16523) : 
--
CREATE TABLE public.client (
    id integer NOT NULL,
    name varchar(256) NOT NULL,
    title varchar(256) NOT NULL,
    type varchar(10),
    domain varchar,
    token varchar(50),
    active boolean NOT NULL,
    "order" integer NOT NULL,
    parentid integer NOT NULL,
    createduser integer NOT NULL,
    lastupdateduser integer NOT NULL,
    createddate timestamp without time zone NOT NULL,
    lastupdateddate timestamp without time zone NOT NULL
)
WITH (oids = false);
--
-- Structure for table membership (OID = 16529) : 
--
CREATE TABLE public.membership (
    id bigint DEFAULT nextval(('public.membership_id_seq'::text)::regclass) NOT NULL,
    username varchar(256) NOT NULL,
    displayname varchar(256),
    gender varchar(10),
    link varchar(100),
    provider varchar(20),
    loweredusername varchar(256),
    mobilealias varchar(16),
    isanonymous boolean,
    password varchar(256) NOT NULL,
    hashcode varchar(256) NOT NULL,
    email varchar(256) NOT NULL,
    passwordquestion varchar(256),
    passwordanswer varchar(128),
    isapproved boolean,
    islockedout boolean,
    createddate timestamp without time zone,
    lastlogindate timestamp without time zone,
    lastpasswordchangeddate date,
    lastlockoutdate timestamp without time zone,
    failedpasswordattemptcount integer,
    activatecode varchar(128),
    comment varchar,
    location integer NOT NULL,
    otpprivatekey varchar(50)
)
WITH (oids = false);
--
-- Structure for table userrole (OID = 16536) : 
--
CREATE TABLE public.userrole (
    id integer NOT NULL,
    membershipid integer NOT NULL,
    role integer NOT NULL
)
WITH (oids = false);
ALTER TABLE ONLY public.userrole ALTER COLUMN id SET STATISTICS 0;
ALTER TABLE ONLY public.userrole ALTER COLUMN membershipid SET STATISTICS 0;
ALTER TABLE ONLY public.userrole ALTER COLUMN role SET STATISTICS 0;
--
-- Definition for sequence membership_id_seq (OID = 16539) : 
--
CREATE SEQUENCE public.membership_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MAXVALUE
    NO MINVALUE
    CACHE 1;
--
-- Definition for view userclientid (OID = 16541) : 
--
CREATE VIEW public.userclientid
AS
SELECT b.id AS userid,
    a.username,
    a.useridclient,
    c.domain,
    c.token
FROM ((userclient a
     LEFT JOIN membership b ON (((a.username)::text = (b.username)::text)))
     LEFT JOIN client c ON ((a.clientid = c.id)));

--
-- Data for table public.userclient (OID = 16517) (LIMIT 0,8)
--
INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (7, 'anhnq', 'AnhNQ', 44, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (6, 'anhnq', 'AnhNQ', 8, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (5, 'anhnq', 'AnhNQ', 1, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (2, 'anhnq', 'AnhNQ', 3, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (4, 'anhnq', 'AnhNQ', 15, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (8, 'tuansale', 'tuansale', 7, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (3, 'vund', 'VuND', 5, false);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (1, 'admin', 'admin', 1, true);

--
-- Data for table public.client (OID = 16523) (LIMIT 0,6)
--
INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (44, 'BCRM CĐT BĐS', 'BCRM CĐT BĐS', NULL, 'cdt.daivietgroup.net:8668', 'tnLjwPNzfF', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (15, 'Candidate Management - Phần mềm quản lý ứng viên', 'Candidate Management - Phần mềm quản lý ứng viên', NULL, '172.16.0.68:1368', '332NJFDS!@#65', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (8, 'BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn', 'BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn', NULL, 'bcrm.daivietgroup.net', 'xA34vpoLmc', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (3, 'Information Checking System', 'Information Checking System', NULL, NULL, 'x7FOrpASlR', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (1, 'HRM - Phần mềm quản lý nhân sự', 'HRM - Phần mềm quản lý nhân sự', NULL, NULL, NULL, true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (5, 'Email', 'Email', NULL, 'localhost:4201', '3foaAn85w5', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

--
-- Data for table public.membership (OID = 16529) (LIMIT 0,7)
--
INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (1, 'admin', 'admin', NULL, NULL, NULL, NULL, NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '7917f2596f8bb70c954893f200ba6274', 'quantri@mvc.com', NULL, NULL, true, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 'PVIRS');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (2, 'vund', 'anhnq', NULL, NULL, NULL, NULL, NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '5f64b96e2559496fbc06db3bdcb90cbf', 'anhnq@gmail.com', NULL, NULL, true, false, NULL, NULL, '2020-03-11', NULL, NULL, NULL, NULL, 1, 'VEEYM');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (7, 'tuansale', 'tuansale', NULL, NULL, NULL, '', NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '1471e3e3212434b9441de748eae9e86e', 'tuansale@gmail.com', NULL, NULL, true, false, '2020-03-17 16:32:20.255406', '2020-03-17 16:32:20.255406', '2020-03-17', '2020-03-17 16:32:20.255406', 0, NULL, NULL, 0, 'WJBZY');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (5, 'test', 'test', NULL, NULL, NULL, '', NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '9b8c343a439678df948e526aea5d5ad6', 'test@gmail.com', NULL, NULL, true, false, '2020-03-17 11:24:37.385055', '2020-03-17 11:24:37.385055', '2020-03-17', '2020-03-17 11:24:37.385055', 0, NULL, NULL, 0, 'UBOZE');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (6, 'ddđ', 'fff', NULL, NULL, NULL, '', NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '6ee3bc114f968dfe898795267a2699b7', 'dđ', NULL, NULL, false, false, '2020-03-17 13:43:33.003716', '2020-03-17 13:43:33.003716', '2020-03-17', '2020-03-17 13:43:33.003716', 0, NULL, NULL, 0, 'IVGWC');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (8, 'test1', 'test1', NULL, NULL, NULL, '', NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', 'fdaad81663daa65f65f959495a6413e9', 'test1', NULL, NULL, true, false, '2020-03-17 22:31:16.154417', '2020-03-17 22:31:16.154417', '2020-03-17', '2020-03-17 22:31:16.154417', 0, NULL, NULL, 0, NULL);

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (9, 'abcd', 'ddd', NULL, NULL, NULL, '', NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '5de8d67910733ce611776d9603436eb7', 'ddd', NULL, NULL, true, false, '2020-03-17 22:54:08.18154', '2020-03-17 22:54:08.18154', '2020-03-17', '2020-03-17 22:54:08.18154', 0, NULL, NULL, 0, NULL);

--
-- Data for table public.userrole (OID = 16536) (LIMIT 0,2)
--
INSERT INTO userrole (id, membershipid, role)
VALUES (1, 1, 1);

INSERT INTO userrole (id, membershipid, role)
VALUES (2, 2, 2);

--
-- Definition for index userclient_pkey (OID = 16546) : 
--
ALTER TABLE ONLY userclient
    ADD CONSTRAINT userclient_pkey
    PRIMARY KEY (id);
--
-- Definition for index client_pkey (OID = 16548) : 
--
ALTER TABLE ONLY client
    ADD CONSTRAINT client_pkey
    PRIMARY KEY (id);
--
-- Definition for index userrole_pkey (OID = 16550) : 
--
ALTER TABLE ONLY userrole
    ADD CONSTRAINT userrole_pkey
    PRIMARY KEY (id);
--
-- Definition for index membership_pkey (OID = 16552) : 
--
ALTER TABLE ONLY membership
    ADD CONSTRAINT membership_pkey
    PRIMARY KEY (id);
--
-- Data for sequence public.membership_id_seq (OID = 16539)
--
SELECT pg_catalog.setval('membership_id_seq', 9, true);
--
-- Comments
--
COMMENT ON SCHEMA public IS 'standard public schema';
