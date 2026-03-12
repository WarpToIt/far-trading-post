// module "list.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app ) => {
  app.get( "/inventory/:id",
    param('id').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    body('token').notEmpty().isString().withMessage("invalid token (must be string)"),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */

      let resBody = {
        "data": [ {"proto_id": 234567, "uid": 345678, "count": 17, "want": 0.85} ], 
        "errors": [ ]
      } ;

      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerList } ;