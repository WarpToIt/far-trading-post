// module "list.js"
"use strict" ;
import { param, body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../../Auth/Util/error_codes.js';

/*
  Maximalist query:
  'SELECT `actor_id`, `item_uid`, `prototype_id`, `item-prototypes`.`category` as `category`, `item-prototypes`.`value` as `value`, `count`, `want` FROM `inventory` JOIN `item-prototypes` ON `item-prototypes`.`id` = `inventory`.`prototype_id` WHERE `actor_id` = ?'
*/


const register = ( app, conn ) => {
  app.get( "/inventory",
    asyncMiddleware( async (request,response,next) => {
      /** Query Validation */
      const result = validationResult(request) ;
      if ( ! result.isEmpty() )
      {
        return response.send( { errors: result.array() } ) ;
      }
      /** End */


      let resBody = {
        "data": [ ], 
        "errors": [ ]
      } ;


      /** SQL */
      let query = 'SELECT `actor_id`, `item_uid`, `prototype_id`, `count`, `want` FROM `inventory`' ;
      let [results, _] = await conn.execute( query );
      if( results.length > 0 )
      {
        results.forEach( result => {
          resBody.data.push( {
            "actor_id": result.actor_id,
            "proto_id": result.prototype_id,
            "uid": result.item_uid,
            "count": result.count,
            "want": result.want
          } ) ;
        } ) ;
      } else {
        resBody.errors.push( error_codes.INVENTORY_EMPTY ) ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerList } ;