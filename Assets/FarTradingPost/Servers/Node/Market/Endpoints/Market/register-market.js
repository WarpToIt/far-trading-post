// module "register-market.js"
"use strict" ;

import { registerList     } from './list.js' ; 
import { registerContext  } from './context.js' ; 
import { registerGive     } from './give.js' ;
import { registerEdit     } from './edit.js' ;
import { registerTransfer } from './transfer.js' ;
import { registerRemove   } from './remove.js' ;
import { registerCreateActor } from './create-actor.js';

const register = ( app, conn ) => {
  registerList( app, conn ) ;
  registerContext( app, conn ) ;
  registerGive( app, conn ) ;
  registerEdit( app, conn ) ;
  registerTransfer( app, conn ) ;
  registerRemove( app, conn ) ;
  registerCreateActor( app, conn ) ;
}

export { register as registerMarket } ;