// module "transfer.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { default as fetch } from 'node-fetch' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { checkToken } from '../Auth/check-token.js';


const register = ( app, conn, marketURL ) => {
  app.put( "/inventory/:id/:uid/:count/:target/:token",
    param('id').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    param('target').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    param('token').notEmpty().isString().withMessage("invalid token (must be string)"),
    param('uid').notEmpty().isInt().toInt( { min:0 } ).withMessage("invalid uid (must be zero or positive integer)"),
    param('count').notEmpty().isInt( { min:0 } ).toInt().optional().withMessage("invalid count (must be zero or positive integer)"),
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
          "uid": -1,
          "errors": tokenStatus.errors
        } ) ;
        return ;
      }
      /** End */


      /** Forward Request to Market */
      await fetch(
        `${marketURL}/inventory/${request.params.id}/${request.params.uid}/${request.params.count}/${request.params.target}`,
        {
          method: 'PUT',
          body: JSON.stringify( request.body ),
          headers: { 'Content-type': 'application/json' }
        },
      ).then(
        (marketResponse) => marketResponse.json()
      ).then(
        (json) => response.status(200).json( json ) 
      ) ;
      /** End */
  } ) ) ;
}

export { register as registerTransfer } ;