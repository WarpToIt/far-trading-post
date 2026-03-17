// module "error_codes.js"
"use strict" ;

const error_codes = {
  "USER_NOT_FOUND"                : '0001 user not found',
  "USER_DUPLICATE_NAME"           : '0002 username already in use',
  "USER_DUPLICATE_EMAIL"          : '0003 email already in use',
  "USER_DUPLICATE_NAME_AND_EMAIL" : '0004 username and email already in use',

  "INVALID_PASSKEY"               : '0101 invalid passkey',
  "INVALID_TOKEN"                 : '0102 invalid token',
  "EXPIRED_TOKEN"                 : '0103 expired token',
  "TOKEN_NOT_FOUND"               : '0103 no tokens found',

  "SWR_ISSUE_TOKEN"               : 'X001 something went wrong when attempting to issue a token',
  "SWR_EXTEND_TOKEN"              : 'X002 something went wrong when attempting to extend a token',
  "SWR_DELETE_TOKEN"              : 'X002 something went wrong when attempting to delete a token',
} ;

export { error_codes } ;