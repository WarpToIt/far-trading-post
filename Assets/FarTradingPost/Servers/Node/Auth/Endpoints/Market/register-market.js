// module "register-market.js"
"use strict" ;

import { registerList     } from './list.js' ; 
import { registerGive     } from './give.js' ;
import { registerEdit     } from './edit.js' ;
import { registerTransfer } from './transfer.js' ;
import { registerRemove   } from './remove.js' ;

const register = ( app ) => {
  registerList( app ) ;
  registerGive( app ) ;
  registerEdit( app ) ;
  registerTransfer( app ) ;
  registerRemove( app ) ;
}

export { register as registerMarket } ;