// module "salt.js"
"use strict" ;
import { body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';


const register = ( app, conn ) => {
  app.get( "/salt",
    body('email').notEmpty().isEmail().withMessage("invalid email (must be e-mail string)"),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */

      
      /** Response Body */
      let resBody = { "id": '', "salt": '', "errors": [ ] } ;
      /** End */


      /** SQL */
      let query = 'SELECT `id`, `salt` FROM `users` WHERE `email` = ?' ;
      let [results, _] = await conn.execute( query,  [ request.body.email ] );
      if( results.length > 0 )
      {
        resBody = {
          "id": results[0].id,
          "salt": results[0].salt,
          "errors": [ ]
        } ;
      } else {
        resBody.errors.push( error_codes.USER_NOT_FOUND ) ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerSalt } ;