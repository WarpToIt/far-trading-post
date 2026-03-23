#!/usr/bin/node
"use strict" ;
const LOCALHOST = "127.0.0.1" ;
const LOCALPORT = 4343 ;

const DB_MARKET_NAME = 'far-trader' ;
const DB_MARKET_USER = 'root' ;


/*************************/
/****** BASIC SETUP ******/
/*************************/

import express from 'express' ;
import http from 'http' ;

var app = express() ;

var server = http.createServer(app) ;

app.use( express.json() ) ;

/*************************/
/******* SQL SETUP *******/
/*************************/

import mysql from 'mysql2/promise';

const conn = await mysql.createConnection({
  host: LOCALHOST,
  user: DB_MARKET_USER,
  database: DB_MARKET_NAME,
});

/*************************/
/******* ENDPOINTS *******/
/*************************/

import { registerMarket } from './Endpoints/Market/register-market.js' ;  registerMarket( app, conn ) ;

/*************************/
/********* START *********/
/*************************/

server.listen(LOCALPORT,LOCALHOST) ;

console.log(`Server listening on: http://${LOCALHOST}:${LOCALPORT}`) ;