// module "check-token.js"
"use strict" ;

import { error_codes } from '../../Util/error_codes.js';

const checkToken = async function( id, token, conn ) {
  let [ remainder, errors ] = [ -1, [] ] ;

  let query = 'SELECT `expires_at` FROM `sessions` WHERE `user_id` = ? AND `token` = ?' ;
  let [results, _] = await conn.execute( query,  [ id, token ] );

  if( results.length > 0 )
  {
    remainder = Date.parse( results[0].expires_at ) - Date.now() ;
    if( remainder < 0 ) { deleteToken( id, token, conn ) ; }
  } else {
    errors.push( error_codes.TOKEN_NOT_FOUND ) ;
  }

  return { "ok": remainder > 0, "safe": remainder > 15000, "remainder": remainder, "errors": errors } ;
} ;

const deleteToken = async function( id, token, conn ) {
  let errors = [] ;
  let query = 'DELETE FROM `sessions` WHERE `user_id` = ? AND `token` = ?' ;
  let [results, _] = await conn.execute( query,  [ id, token ] );

  if( results.affectedRows == 0 )
  {
    errors.push( error_codes.SWR_DELETE_TOKEN ) ;
  }

  return [ errors.length == 0, errors ] ;
}

export { checkToken, deleteToken } ;