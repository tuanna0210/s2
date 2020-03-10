--
-- PostgreSQL database dump
--

-- Dumped from database version 10.5
-- Dumped by pg_dump version 11beta2

-- Started on 2020-03-10 18:38:58

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 215 (class 1255 OID 91885)
-- Name: membership_getlistbysecurity(character varying, character varying); Type: FUNCTION; Schema: public; Owner: tungbt
--

CREATE FUNCTION public.membership_getlistbysecurity(_username character varying, _password character varying) RETURNS TABLE(id integer, username character varying, displayname character varying, loweredusername character varying, password character varying, hashcode character varying, email character varying, location integer, isapproved boolean, islockedout boolean)
    LANGUAGE plpgsql
    AS $$
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
         m.islockedout
  FROM  public.membership m
  WHERE m.username = _username and m.password = _password; 
END;
$$;


ALTER FUNCTION public.membership_getlistbysecurity(_username character varying, _password character varying) OWNER TO tungbt;

--
-- TOC entry 216 (class 1255 OID 91887)
-- Name: membership_getlistbyusername(character varying); Type: FUNCTION; Schema: public; Owner: tungbt
--

CREATE FUNCTION public.membership_getlistbyusername(_username character varying) RETURNS TABLE(id integer, username character varying, displayname character varying, loweredusername character varying, password character varying, hashcode character varying, email character varying, location integer, isapproved boolean, islockedout boolean)
    LANGUAGE plpgsql
    AS $$
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
         m.islockedout
  FROM  public.membership m
  WHERE m.username = _username; 
END;
$$;


ALTER FUNCTION public.membership_getlistbyusername(_username character varying) OWNER TO tungbt;

--
-- TOC entry 200 (class 1255 OID 91850)
-- Name: userclient_getbyusername(character varying); Type: FUNCTION; Schema: public; Owner: tungbt
--

CREATE FUNCTION public.userclient_getbyusername(_username character varying) RETURNS TABLE(id integer, username character varying, useridclient character varying, clientid integer, islogin boolean)
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.userclient_getbyusername(_username character varying) OWNER TO tungbt;

--
-- TOC entry 214 (class 1255 OID 91864)
-- Name: userclient_getbyusernameandclientid(character varying, integer); Type: FUNCTION; Schema: public; Owner: tungbt
--

CREATE FUNCTION public.userclient_getbyusernameandclientid(_username character varying, _clientid integer) RETURNS TABLE(id integer, username character varying, useridclient character varying, clientid integer, islogin boolean)
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.userclient_getbyusernameandclientid(_username character varying, _clientid integer) OWNER TO tungbt;

--
-- TOC entry 201 (class 1255 OID 91862)
-- Name: userclientid_getbyusernameanddomain(character varying, character varying); Type: FUNCTION; Schema: public; Owner: tungbt
--

CREATE FUNCTION public.userclientid_getbyusernameanddomain(_username character varying, _domain character varying) RETURNS TABLE(userid integer, username character varying, useridclient character varying, domain character varying, token character varying)
    LANGUAGE plpgsql
    AS $$
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
$$;


ALTER FUNCTION public.userclientid_getbyusernameanddomain(_username character varying, _domain character varying) OWNER TO tungbt;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 196 (class 1259 OID 91380)
-- Name: client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.client (
    id integer NOT NULL,
    name character varying(256) NOT NULL,
    title character varying(256) NOT NULL,
    domain character varying,
    active boolean,
    type character varying(10),
    token character varying(50)
);


ALTER TABLE public.client OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 91388)
-- Name: membership; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.membership (
    id integer NOT NULL,
    username character varying(256) NOT NULL,
    displayname character varying(256),
    password character varying(256) NOT NULL,
    hashcode character varying(256) NOT NULL,
    email character varying(256) NOT NULL,
    location integer NOT NULL,
    isapproved boolean,
    islockedout boolean,
    loweredusername character varying(256)
);


ALTER TABLE public.membership OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 91396)
-- Name: userclient; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.userclient (
    id integer NOT NULL,
    username character varying(256) NOT NULL,
    useridclient character varying(256) NOT NULL,
    clientid integer NOT NULL,
    islogin boolean
);


ALTER TABLE public.userclient OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 91857)
-- Name: userclientid; Type: VIEW; Schema: public; Owner: tungbt
--

CREATE VIEW public.userclientid AS
 SELECT b.id AS userid,
    a.username,
    a.useridclient,
    c.domain,
    c.token
   FROM ((public.userclient a
     LEFT JOIN public.membership b ON (((a.username)::text = (b.username)::text)))
     LEFT JOIN public.client c ON ((a.clientid = c.id)));


ALTER TABLE public.userclientid OWNER TO tungbt;

--
-- TOC entry 2173 (class 0 OID 91380)
-- Dependencies: 196
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.client (id, name, title, domain, active, type, token) FROM stdin;
44	BCRM CĐT BĐS	BCRM CĐT BĐS	cdt.daivietgroup.net:8668	t	\N	tnLjwPNzfF
8	BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn	BCRM BĐS - Phần mềm BCRM của batdongsan.com.vn	bcrm.daivietgroup.net	t	\N	xA34vpoLmc
1	HRM - Phần mềm quản lý nhân sự	HRM - Phần mềm quản lý nhân sự	\N	t	\N	\N
15	Candidate Management - Phần mềm quản lý ứng viên	Candidate Management - Phần mềm quản lý ứng viên	172.16.0.68:1368	t	\N	332NJFDS!@#65
5	Email	Email	\N	t	\N	\N
3	Information Checking System	Information Checking System	\N	t	\N	x7FOrpASlR
\.


--
-- TOC entry 2174 (class 0 OID 91388)
-- Dependencies: 197
-- Data for Name: membership; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.membership (id, username, displayname, password, hashcode, email, location, isapproved, islockedout, loweredusername) FROM stdin;
1	admin	admin	14e1b600b1fd579f47433b88e8d85291	e3e14cd9a386d7461a5209073a5b7811	quantri@mvc.com	1	t	\N	\N
2	anhnq	anhnq	14e1b600b1fd579f47433b88e8d85291	6ac0a2ff13b99201d9b75c1e7b98a0ed	anhnq@gmail.com	1	t	\N	\N
\.


--
-- TOC entry 2175 (class 0 OID 91396)
-- Dependencies: 198
-- Data for Name: userclient; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.userclient (id, username, useridclient, clientid, islogin) FROM stdin;
1	admin	admin	1	t
7	anhnq	AnhNQ	44	t
6	anhnq	AnhNQ	8	t
5	anhnq	AnhNQ	1	t
4	anhnq	AnhNQ	15	t
3	anhnq	AnhNQ@daivietgroup.net.vn	5	t
2	anhnq	AnhNQ	3	t
\.


--
-- TOC entry 2046 (class 2606 OID 91387)
-- Name: client client_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pkey PRIMARY KEY (id);


--
-- TOC entry 2048 (class 2606 OID 91395)
-- Name: membership membership_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.membership
    ADD CONSTRAINT membership_pkey PRIMARY KEY (id);


--
-- TOC entry 2050 (class 2606 OID 91403)
-- Name: userclient userclient_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.userclient
    ADD CONSTRAINT userclient_pkey PRIMARY KEY (id);


-- Completed on 2020-03-10 18:38:58

--
-- PostgreSQL database dump complete
--

