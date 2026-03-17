// module "logout.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { checkToken, deleteToken } from './check-token.js';


const register = ( app, conn ) => {
  app.delete( "/auth/:id/:token",
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


      /** Check Token */
      let tokenCheck = await checkToken( request.params.id, request.params.token, conn ) ;

      let resBody = {
        "errors": tokenCheck.errors
      } ;

      if( tokenCheck.ok )
      {
        let [deleteOk, deleteErrors] = await deleteToken( request.params.id, request.params.token, conn ) ;

        if( !deleteOk ) { deleteErrors.forEach(error => { resBody.errors.push( error ) ; }) ; }
      }
      /** End */

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerLogout } ;