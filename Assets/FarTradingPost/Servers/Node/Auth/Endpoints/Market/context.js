// module "context.js"
"use strict" ;
import { param, validationResult } from 'express-validator' ;
import { default as fetch } from 'node-fetch' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { checkToken } from '../Auth/check-token.js';

const register = ( app, conn, marketURL ) => {
  app.get( "/inventory/context/:id/:token",
    param('id').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    param('token').notEmpty().isString().withMessage("invalid token (must be string)"),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */


      /** Check Token Validity */
      let tokenStatus = await checkToken( request.params.id, request.params.token, conn ) ;

      if( !tokenStatus.ok )
      {
        response.status(200).json( {
          "actors": [ ],
          "companies": [ ],
          "itemPrototypes": [ ],
          "categories": [ ], 
          "timestamps": [ ], 
          "valueTrends": [ ], 
          "errors": tokenStatus.errors
        } ) ;
        return ;
      }
      /** End */


      /** Forward Request to Market */
      await fetch(
        `${marketURL}/inventory/context`,
        { method: 'GET' }
      ).then(
        (marketResponse) => marketResponse.json()
      ).then(
        (json) => response.status(200).json( json ) 
      ) ;
      /** End */
  } ) ) ;
}

export { register as registerContext } ;