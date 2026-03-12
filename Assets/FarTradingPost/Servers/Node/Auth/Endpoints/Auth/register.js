// module "register.js"
"use strict" ;
import { body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app ) => {
  app.post( "/user",
    body('username').notEmpty().isString(),
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
        "errors": [ ]
      } ;

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerRegister } ;