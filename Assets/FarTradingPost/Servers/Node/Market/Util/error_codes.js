// module "error_codes.js"
"use strict" ;

const error_codes = {
  "EMPTY_ACTORS"            : 'M0101 no actors found',
  "EMPTY_COMPANIES"         : 'M0101 no companies found',
  "EMPTY_ITEM_PROTOTYPES"   : 'M0102 no item prototypes found',
  "EMPTY_CATEGORIES"        : 'M0103 no categories found',
  "EMPTY_TIMESTAMPS"        : 'M0104 no timestamps found',
  "EMPTY_VALUE_TRENDS"      : 'M0105 no value trends found',

  "EMPTY_INVENTORY"         : 'M0201 no items found in this user\'s inventory',
} ;

export { error_codes } ;