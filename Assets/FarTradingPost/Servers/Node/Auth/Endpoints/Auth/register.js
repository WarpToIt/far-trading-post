// module "register.js"
"use strict" ;
import { body, validationResult } from 'express-validator' ;
import { asyncMiddleware } from '../../Util/asyncMiddleware.js';
import { error_codes } from '../../Util/error_codes.js';


const register = ( app, conn, marketURL ) => {
  app.post( "/user",
    body('username').notEmpty().isString().withMessage("invalid username (must be string)"),
    body('email').notEmpty().isEmail().withMessage("invalid email (must be e-mail string)"),
    body('passkey').notEmpty().isString().withMessage("invalid passkey (must be string)"),
    body('salt').notEmpty().isString().withMessage("invalid salt (must be string)"),
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


      /** Forward Request to Market */
      let actor_id = -1 ;
      await fetch(
        `${marketURL}/actor`,
        {
          method: 'POST',
          body: JSON.stringify( { username:request.body.username, company:0, human:true } ),
          headers: { 'Content-type': 'application/json' }
        },
      ).then(
        (marketResponse) => marketResponse.json()
      ).then(
        (json) => actor_id = json.actor_id 
      ) ;
      /** End */


      /** SQL */
      let query = 'INSERT INTO `users`(`id`, `email`, `passkey`, `salt`) VALUES (?, ?, ?, ?)' ;
      let [results, _] = await conn.execute( query,  [ actor_id, request.body.email, request.body.passkey, request.body.salt ] );
      if( results.affectedRows > 0 )
      {
        // ok
      } else {
        resBody.errors.push( error_codes.SWR_REGISTER_USER ) ;
      }
      /** End */


      /** Dispatch Response */
      response.status(200).json( resBody ) ;
      /** End */
  } ) ) ;
}

export { register as registerRegister } ;