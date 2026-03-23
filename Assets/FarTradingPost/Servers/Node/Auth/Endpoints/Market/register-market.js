// module "register-market.js"
"use strict" ;

import { registerList     } from './list.js' ; 
import { registerContext  } from './context.js' ; 
import { registerGive     } from './give.js' ;
import { registerEdit     } from './edit.js' ;
import { registerTransfer } from './transfer.js' ;
import { registerRemove   } from './remove.js' ;

const register = ( app, conn, marketURL ) => {
  registerList( app, conn, marketURL ) ;
  registerContext( app, conn, marketURL ) ;
  registerGive( app, conn, marketURL ) ;
  registerEdit( app, conn, marketURL ) ;
  registerTransfer( app, conn, marketURL ) ;
  registerRemove( app, conn, marketURL ) ;
}

export { register as registerMarket } ;