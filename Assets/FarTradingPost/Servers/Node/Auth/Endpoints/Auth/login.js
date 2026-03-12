// module "login.js"
"use strict" ;
import { body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app ) => {
  app.get( "/auth",
    body('email').notEmpty().isEmail(),
    body('passkey').notEmpty().isString(),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */

      let resBody = {
        "id": "123456",
        "username": "Amy Sally",
        "token": "gfd8923hf0dsjf2ßjdfsa934tfg9tzdh8iseruwgo",
        "expires_at": "2026-03-05T00:00:00Z",
        "errors": [ ]
      } ;

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerLogin } ;