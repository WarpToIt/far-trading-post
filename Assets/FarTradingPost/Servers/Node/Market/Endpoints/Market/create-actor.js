// module "create-actor.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';


const register = ( app, conn ) => {
  app.post( "/actor",
    body('username').notEmpty().isString().withMessage("invalid username (must be string)"),
    body('company').notEmpty().isInt( { min:0 } ).toInt().withMessage("invalid company (must be zero or positive integer)"),
    body('human').notEmpty().isBoolean().withMessage("invalid human (must be boolean)"),
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */


      let resBody = {
        "actor_id": -1,
        "errors": [ ]
      } ;


      /** SQL */
      let query = 'INSERT INTO `actors`(`name`, `company`, `human`) VALUES (?, ?, ?)' ;
      let [results, _] = await conn.execute( query,  [ request.body.username, request.body.company, request.body.human ] );
      if( results.affectedRows > 0 )
      {
        resBody.actor_id = results.insertId ;
      } else {
        resBody.errors.push( error_codes.SWR_REGISTER_ACTOR ) ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerCreateActor } ;