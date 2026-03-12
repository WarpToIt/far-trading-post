// module "unregister.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app ) => {
  app.delete( "/user/:id",
    param('id').notEmpty().isInt().toInt(),
    body('passkey').notEmpty().isString(),
    body('token').notEmpty().isString(),
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

export { register as registerUnRegister } ;