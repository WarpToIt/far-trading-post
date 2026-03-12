// module "salt.js"
"use strict" ;
import { body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app ) => {
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

      let resBody = {
        "id": "123456",
        "salt": "ghsdvl04e9tcuw34sfdsgd",
        "errors": [ ]
      } ;

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerSalt } ;