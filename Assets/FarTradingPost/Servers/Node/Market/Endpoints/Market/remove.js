// module "remove.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';


const register = ( app, conn ) => {
  app.delete( "/inventory/:uid/:count",
    param('uid').notEmpty().isInt().toInt( { min:0 } ).withMessage("invalid uid (must be zero or positive integer)"),
    param('count').notEmpty().isInt( { min:0 } ).toInt().optional().withMessage("invalid count (must be zero or positive integer)"),
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


      /** SQL :: Item Prototypes */
      let errors = [] ;
      let sql = conn.execute( 'UPDATE `inventory` SET `count`=`count`-? WHERE `item_uid`=?', [ request.params.count, request.params.uid ] )
      .then( (sqlResponse) => sqlResponse[0] )
      .then( (results) => {
        if( results.affectedRows > 0 )
        {
          // 
        } else {
          errors.push( error_codes.MISSING_INVENTORY_ITEM ) ;
        }
      } ) ;
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerRemove } ;