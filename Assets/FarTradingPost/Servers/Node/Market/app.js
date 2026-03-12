#!/usr/bin/node
"use strict" ;
const LOCALHOST = "127.0.0.1" ;
const LOCALPORT = 4343 ;


/*************************/
/****** BASIC SETUP ******/
/*************************/

import express from 'express' ;
import http from 'http' ;

var app = express() ;

var server = http.createServer(app) ;

app.use( express.json() ) ;

/*************************/
/******* ENDPOINTS *******/
/*************************/

import { registerMarket } from './Endpoints/Market/register-market.js' ;  registerMarket( app ) ;

/*************************/
/********* START *********/
/*************************/

server.listen(LOCALPORT,LOCALHOST) ;

console.log(`Server listening on: http://${LOCALHOST}:${LOCALPORT}`) ;