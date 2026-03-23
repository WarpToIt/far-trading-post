// module "edit.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';


const register = ( app, conn ) => {
  app.put( "/inventory/:id",
    param('id').notEmpty().isInt().toInt().withMessage("invalid id (must be integer)"),
    body('uid').notEmpty().isInt().toInt( { min:0 } ).withMessage("invalid uid (must be zero or positive integer)"),
    body('count').notEmpty().isInt( { min:0 } ).toInt().optional().withMessage("invalid count (must be zero or positive integer)"),
    body('want').notEmpty().isFloat().optional().withMessage("invalid want (must be float)"),
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

export { register as registerEdit } ;