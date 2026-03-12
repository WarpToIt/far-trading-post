// module "register-auth.js"
"use strict" ;

import { registerLogin  } from './login.js' ; 
import { registerExtend } from './extend.js' ;
import { registerLogout } from './logout.js' ;

import { registerRegister   } from './register.js' ;
import { registerUnRegister } from './unregister.js' ;

const register = ( app ) => {
  registerLogin(  app ) ;
  registerExtend( app ) ;
  registerLogout( app ) ;

  registerRegister(  app ) ;
  registerUnRegister( app ) ;
}

export { register as registerAuth } ;