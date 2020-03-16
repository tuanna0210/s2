-- SQL Manager for PostgreSQL 5.9.4.51539
-- ---------------------------------------
-- Host      : 172.16.0.203
-- Database  : dvg_sso_global
-- Version   : PostgreSQL 10.5 on x86_64-pc-linux-gnu, compiled by gcc (Ubuntu 7.4.0-1ubuntu1~18.04.1) 7.4.0, 64-bit



SET check_function_bodies = false;
--
-- Definition for function userclient_getbyusername (OID = 91850) : 
--
SET search_path = public, pg_catalog;
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
-- Definition for function userclientid_getbyusernameanddomain (OID = 91862) : 
--
CREATE FUNCTION public.userclientid_getbyusernameanddomain (
  _username character varying,
  _domain character varying
)
RETURNS TABLE (
  userid integer,
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
-- Definition for function userclient_getbyusernameandclientid (OID = 91864) : 
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
-- Definition for function membership_getbyid (OID = 91890) : 
--
CREATE FUNCTION public.membership_getbyid (
  _userid integer
)
RETURNS TABLE (
  id integer,
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
-- Definition for function membership_getlistbysecurity (OID = 91891) : 
--
CREATE FUNCTION public.membership_getlistbysecurity (
  _username character varying,
  _password character varying
)
RETURNS TABLE (
  id integer,
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
  WHERE m.username = _username and m.password = _password; 
END;
$body$
LANGUAGE plpgsql;
--
-- Definition for function membership_updateotprrivatekey (OID = 92468) : 
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
-- Definition for function membership_getlistbyusername (OID = 92469) : 
--
CREATE FUNCTION public.membership_getlistbyusername (
  _username character varying
)
RETURNS TABLE (
  id integer,
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
-- Definition for function client_getbydomain (OID = 92500) : 
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
-- Structure for table userclient (OID = 91396) : 
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
-- Structure for table client (OID = 92478) : 
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
-- Structure for table membership (OID = 92522) : 
--
CREATE TABLE public.membership (
    id integer NOT NULL,
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
-- Definition for view userclientid (OID = 92530) : 
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
-- Data for table public.userclient (OID = 91396) (LIMIT 0,7)
--
INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (1, 'admin', 'admin', 1, true);

INSERT INTO userclient (id, username, useridclient, clientid, islogin)
VALUES (3, 'vund', 'VuND', 5, true);

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

--
-- Data for table public.client (OID = 92478) (LIMIT 0,6)
--
INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (44, 'BCRM CĐT BĐS', 'BCRM CĐT BĐS', NULL, 'cdt.daivietgroup.net:8668', 'tnLjwPNzfF', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (15, 'Candidate Management - Phần mềm quản lý ứng viên', 'Candidate Management - Phần mềm quản lý ứng viên', NULL, '172.16.0.68:1368', '332NJFDS!@#65', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (8, 'BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn', 'BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn', NULL, 'bcrm.daivietgroup.net', 'xA34vpoLmc', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (5, 'Email', 'Email', NULL, 'localhost:65071', '3foaAn85w5', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (3, 'Information Checking System', 'Information Checking System', NULL, NULL, 'x7FOrpASlR', true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

INSERT INTO client (id, name, title, type, domain, token, active, "order", parentid, createduser, lastupdateduser, createddate, lastupdateddate)
VALUES (1, 'HRM - Phần mềm quản lý nhân sự', 'HRM - Phần mềm quản lý nhân sự', NULL, NULL, NULL, true, 0, 0, 1, 1, '2019-03-30 11:35:10', '2019-03-30 11:35:10');

--
-- Data for table public.membership (OID = 92522) (LIMIT 0,2)
--
INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (2, 'vund', 'anhnq', NULL, NULL, NULL, NULL, NULL, NULL, 'e10adc3949ba59abbe56e057f20f883e', '5f64b96e2559496fbc06db3bdcb90cbf', 'anhnq@gmail.com', NULL, NULL, true, false, NULL, NULL, '2020-03-11', NULL, NULL, NULL, NULL, 1, 'RFFYK');

INSERT INTO membership (id, username, displayname, gender, link, provider, loweredusername, mobilealias, isanonymous, password, hashcode, email, passwordquestion, passwordanswer, isapproved, islockedout, createddate, lastlogindate, lastpasswordchangeddate, lastlockoutdate, failedpasswordattemptcount, activatecode, comment, location, otpprivatekey)
VALUES (1, 'admin', 'admin', NULL, NULL, NULL, NULL, NULL, NULL, '14e1b600b1fd579f47433b88e8d85291', 'e3e14cd9a386d7461a5209073a5b7811', 'quantri@mvc.com', NULL, NULL, true, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 'QFQVQ');

--
-- Definition for index userclient_pkey (OID = 91402) : 
--
ALTER TABLE ONLY userclient
    ADD CONSTRAINT userclient_pkey
    PRIMARY KEY (id);
--
-- Definition for index client_pkey (OID = 92484) : 
--
ALTER TABLE ONLY client
    ADD CONSTRAINT client_pkey
    PRIMARY KEY (id);
--
-- Definition for index membership_pkey (OID = 92528) : 
--
ALTER TABLE ONLY membership
    ADD CONSTRAINT membership_pkey
    PRIMARY KEY (id);
--
-- Comments
--
COMMENT ON SCHEMA public IS 'standard public schema';
