#!/usr/bin/node
"use strict" ;
const LOCALHOST = "127.0.0.1" ;
const LOCALPORT = 4141 ;

const MARKET_HOST = LOCALHOST ;
const MARKET_PORT = 4343 ;

const DB_AUTH_NAME = 'far-trader-auth' ;
const DB_AUTH_USER = 'root' ;

/*************************/
/***** EXPRESS SETUP *****/
/*************************/

import express from 'express' ;
import http from 'http' ;

const app = express() ;

const server = http.createServer(app) ;

app.use( express.json() ) ;

/*************************/
/******* SQL SETUP *******/
/*************************/

import mysql from 'mysql2/promise';

const conn = await mysql.createConnection({
  host: LOCALHOST,
  user: DB_AUTH_USER,
  database: DB_AUTH_NAME,
});

/*************************/
/******* ENDPOINTS *******/
/*************************/

import { registerAuth  } from './Endpoints/Auth/register-auth.js' ;       registerAuth( app, conn ) ;
import { registerMarket } from './Endpoints/Market/register-market.js' ;  registerMarket( app, conn ) ;

/*************************/
/********* START *********/
/*************************/

server.listen(LOCALPORT,LOCALHOST) ;

console.log(`Server listening on: http://${LOCALHOST}:${LOCALPORT}`) ;