// module "register-auth.js"
"use strict" ;

import { registerSalt   } from './salt.js' ; 
import { registerLogin  } from './login.js' ; 
import { registerExtend } from './extend.js' ;
import { registerLogout } from './logout.js' ;

import { registerRegister   } from './register.js' ;
import { registerUnRegister } from './unregister.js' ;

const register = ( app, conn, marketURL ) => {
  registerSalt( app, conn ) ;
  registerLogin( app, conn ) ;
  registerExtend( app, conn ) ;
  registerLogout( app, conn ) ;

  registerRegister( app, conn, marketURL ) ;
  registerUnRegister( app, conn, marketURL) ;
}

export { register as registerAuth } ;