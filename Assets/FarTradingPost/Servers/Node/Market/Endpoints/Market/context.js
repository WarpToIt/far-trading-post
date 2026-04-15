// module "context.js"
"use strict" ;
import { param, validationResult } from 'express-validator' ;
import { default as fetch } from 'node-fetch' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';

const register = ( app, conn ) => {
  app.get( "/inventory/context",
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */


      let resBody = {
        "actors": [ ],
        "companies": [ ],
        "itemPrototypes": [ ],
        "categories": [ ], 
        "timestamps": [ ], 
        "valueTrends": [ ], 
        "errors": [ ]
      } ;


      /** SQL :: Actors */
      let errorsActors = [] ;
      let sqlActors = conn.execute( 'SELECT `id`, `name`, `company`, `human` FROM `actors`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.actors.push( {
              "id": result.id,
              "name": result.name,
              "company": result.company,
              "human": result.human
            } ) ;
          } ) ;
        } else {
          errorsActors.push( error_codes.EMPTY_ACTORS ) ;
        }
      } ) ;
      /** End */


      /** SQL :: Companies */
      let errorsCompanies = [] ;
      let sqlCompanies = conn.execute( 'SELECT `id`, `name` FROM `companies`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.companies.push( {
              "id": result.id,
              "name": result.name
            } ) ;
          } ) ;
        } else {
          errorsActors.push( error_codes.EMPTY_ACTORS ) ;
        }
      } ) ;
      /** End */


      /** SQL :: Item Prototypes */
      let errorsItemPrototypes = [] ;
      let sqlItemPrototypes = conn.execute( 'SELECT `id`, `name`, `category`, `value` FROM `item-prototypes`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.itemPrototypes.push( {
              "id": result.id,
              "name": result.name,
              "category": result.category,
              "value": result.value,
            } ) ;
          } ) ;
        } else {
          errorsItemPrototypes.push( error_codes.EMPTY_ITEM_PROTOTYPES ) ;
        }
      } ) ;
      /** End */


      /** SQL :: Categories */
      let errorsCategories = [] ;
      let sqlCategories = conn.execute( 'SELECT `id`, `name` FROM `categories`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.categories.push( {
              "id": result.id,
              "name": result.name,
            } ) ;
          } ) ;
        } else {
          errorsCategories.push( error_codes.EMPTY_CATEGORIES ) ;
        }
      } ) ;
      /** End */


      /** SQL :: Timestamps */
      let errorsTimestamps = [] ;
      let sqlTimestamps = conn.execute( 'SELECT `id`, `timestamp` FROM `timestamps`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.timestamps.push( {
              "id": result.id,
              "timestamp": result.timestamp
            } ) ;
          } ) ;
        } else {
          errorsTimestamps.push( error_codes.EMPTY_TIMESTAMPS ) ;
        }
      } ) ;
      /** End */


      /** SQL :: Value Trends */
      let errorsValueTrends = [] ;
      let sqlValueTrends = conn.execute( 'SELECT `category_id`, `timestamp_id`, `trend` FROM `value-trends`' )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.length > 0 )
        {
          results.forEach( result => {
            resBody.valueTrends.push( {
              "category_id": result.category_id,
              "timestamp_id": result.timestamp_id,
              "trend": result.trend
            } ) ;
          } ) ;
        } else {
          errorsValueTrends.push( error_codes.EMPTY_VALUE_TRENDS ) ;
        }
      } ) ;
      /** End */


      await Promise.all( [ sqlActors, sqlCompanies, sqlCategories, sqlItemPrototypes, sqlTimestamps, sqlValueTrends ] ) ;

      resBody.errors = errorsActors
        .concat(errorsCompanies)
        .concat(errorsCategories)
        .concat(errorsItemPrototypes)
        .concat(errorsTimestamps)
        .concat(errorsValueTrends) ;

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerContext } ;