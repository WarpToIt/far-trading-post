// module "login.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { v6 as uuidv6 } from 'uuid' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';
import { app_settings } from '../../Util/app_settings.js';


const register = ( app, conn ) => {
  app.get( "/auth/:id",
    param('id').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    body('passkey').notEmpty().isString().withMessage("invalid passkey (must be string)"),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */


      /** Retrieve Username and Passkey */
      let [ok, responseBody] = await getToken( request.params.id, request.body.passkey, conn, app_settings.TOKEN_LIFETIME ) ;
      if( !ok )
      {
        console.log( "Login attempt encountered the following errors: " + responseBody.errors ) ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( responseBody ) ;
      /** End */
  } ) ) ;
}

const validateIdentity = async function( id, reqPasskey, conn ) {
  let [username, passkey, errors] = await getNameAndKey( id, conn ) ;
  
  if( errors.length == 0 && passkey != reqPasskey )
  {
    errors.push( error_codes.INVALID_PASSKEY ) ;
  }

  return [ (errors.length == 0), { "username": username, "token": null, "expires_at": null, "errors": errors } ] ;
}

const getNameAndKey = async function( id, conn ) {
  let [username, passkey, errors] = [null,null,[]] ;
  let query = 'SELECT `name`,`passkey` FROM `users` WHERE `users`.`id` = ?';
  let [results, _] = await conn.execute( query,  [ id ] );
  if( results.length == 0 )
  {
    errors.push( error_codes.USER_NOT_FOUND ) ;
  }
  else
  {
    username = results[0].name ;
    passkey  = results[0].passkey ;
  }
  return [username, passkey, errors] ;
}

const getToken = async function( id, reqPasskey, conn, token_lifetime ) {
  let [ ok, body ] = await validateIdentity( id, reqPasskey, conn ) ;
  if( ok )
  {
    body.token = uuidv6() ; // THIS IS NOT A SECURE TOKEN GENERATOR AND IS PURELY FOR DEMO PURPOSES
    body.expires_at = new Date( Date.now() + token_lifetime ); 

    let query = 'INSERT INTO `sessions`(`user_id`, `expires_at`, `token`) VALUES (?,?,?)' ;
    let [results, _] = await conn.execute( query,  [ id, body.expires_at, body.token ] );

    if( results.affectedRows > 0 )
    {
      body.expires_at = body.expires_at.toJSON() ;
    } else {
      body.errors.push( error_codes.SWR_ISSUE_TOKEN ) ;
    }
  }
  else
  {
    body.username = '' ;
  }

  return [ (body.errors.length == 0), body ] ;
} ;


export { register as registerLogin } ;