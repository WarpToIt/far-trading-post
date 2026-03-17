// module "extend.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';
import { app_settings } from '../../Util/app_settings.js';


const register = ( app ) => {
  app.put( "/auth/:id/:token",
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

      let resBody = {
        "expires_at": "",
        "errors": [ ]
      } ;

      /** Check Token */
      let tokenCheck = await checkToken( request.params.id, request.params.token, conn ) ;
      if( tokenCheck.ok )
      {
        [_, resBody] = extendToken( id, token, app_settings.TOKEN_EXTENSION_TIME, conn ) ;
      }
      else
      {
        resBody.errors = tokenCheck.errors ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

const extendToken = async function( id, token, token_extension_time, conn ) {
  let tokenCheck = await checkToken( request.params.id, request.params.token, conn ) ;

  let [ ok, body ] = [ tokenCheck.ok, { "expires_at": "", "errors": tokenCheck.errors } ] ;

  if( ok )
  {
    let expires_at = new Date( Date.now() + token_extension_time ) ;

    let query = 'UPDATE `sessions` SET `expires_at`=? WHERE `id` = ? AND `token` = ?' ;
    let [results, _] = await conn.execute( query,  [ expires_at, id, token ] );

    if( results.affectedRows > 0 )
    {
      body.expires_at = expires_at.toJSON() ;
    }
    else
    {
      body.errors.push( error_codes.SWR_EXTEND_TOKEN ) ;
    }
  }

  return [ (body.errors.length == 0), body ] ;
}

export { register as registerExtend } ;